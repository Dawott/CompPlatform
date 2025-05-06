using Compliance_Platform.Interfaces;
using Compliance_Platform.Model;
using Microsoft.EntityFrameworkCore;

namespace Compliance_Platform.Repositories
{
    public class QuestionnaireRepository : IQuestionnaireRepository
    {
        private readonly CompPlatformDbContext _context;

        public QuestionnaireRepository(CompPlatformDbContext context)
        {
            _context = context;
        }

        public async Task<CompPlatformQuestionnaires> GetQuestionnaireAsync(int questionnaireId)
        {
            return await _context.Questionnaires
                .Include(q => q.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(q => q.Id == questionnaireId);
        }

        public async Task<List<CompPlatformQuestionnaires>> GetActiveQuestionnairesAsync()
        {
            return await _context.Questionnaires
                .Where(q => q.Aktywny)
                .ToListAsync();
        }

        public async Task<CompPlatformTool> GetToolAsync(int toolId)
        {
            return await _context.Tools
                .FirstOrDefaultAsync(t => t.Id == toolId);
        }

        public async Task<List<CompPlatformTool>> GetToolsByOwnerAsync(string ownerId)
        {
            return await _context.Tools
                .Where(t => t.OwnerId == ownerId)
                .ToListAsync();
        }

        public async Task<List<CompPlatformTool>> GetAllToolsAsync()
        {
            return await _context.Tools
                .Include(t => t.Wlasciciel)
                .ToListAsync();
        }

        public async Task<CompPlatformInstance> GetInstanceAsync(int instanceId)
        {
            return await _context.InstancesTool
                .Include(i => i.Answers)
                .FirstOrDefaultAsync(i => i.Id == instanceId);
        }

        public async Task<CompPlatformInstance> GetInstanceDraftAsync(int toolId, int questionnaireId)
        {
            return await _context.InstancesTool
                .Include(i => i.Answers)
                .FirstOrDefaultAsync(i =>
                    i.ToolId == toolId &&
                    i.QuestionnaireId == questionnaireId &&
                    i.Status == "Draft");
        }

        public async Task<List<CompPlatformInstance>> GetInstancesByStatusAsync(string status)
        {
            return await _context.InstancesTool
                .Include(i => i.CompPlatformTool)
                .Where(i => i.Status == status)
                .ToListAsync();
        }

        public async Task<List<CompPlatformInstance>> GetInstancesByToolAsync(int toolId)
        {
            return await _context.InstancesTool
                .Where(i => i.ToolId == toolId)
                .ToListAsync();
        }

        public async Task<List<CompPlatformQuestions>> GetQuestionsByQuestionnaireAsync(int questionnaireId)
        {
            return await _context.Questions
                .Include(q => q.Answers)
                .Where(q => q.QuestionnaireId == questionnaireId)
                .OrderBy(q => q.OrderIndex)
                .ToListAsync();
        }

        public async Task<List<CompPlatformAnswers>> GetAnswersByQuestionAsync(int questionId)
        {
            return await _context.Answers
                .Where(a => a.QuestionTemplateId == questionId)
                .OrderBy(a => a.OrderIndex)
                .ToListAsync();
        }

        public async Task<List<CompPlatformInstanceAnswer>> GetInstanceAnswersAsync(int instanceId)
        {
            return await _context.InstanceAnswers
                .Where(a => a.InstanceId == instanceId)
                .ToListAsync();
        }

        public async Task<int> SaveDraftAsync(CompPlatformInstance instance, List<CompPlatformInstanceAnswer> answers)
        {
            // !!! Sprawdzanie draftu
            var existingInstance = await _context.InstancesTool
                .FirstOrDefaultAsync(i => i.Id == instance.Id);

            if (existingInstance == null)
            {
                // Nowy
                instance.Status = "Draft";
                instance.DataUtworzenia = DateTime.Now;
                _context.InstancesTool.Add(instance);
                await _context.SaveChangesAsync();

                // Update id instancji
                foreach (var answer in answers)
                {
                    answer.InstanceId = instance.Id;
                    answer.DataOdpowiedzi = DateTime.Now;
                }

                _context.InstanceAnswers.AddRange(answers);
            }
            else
            {
                // Update draftu
                existingInstance.KalkulacjaRyzyka = instance.KalkulacjaRyzyka;
                existingInstance.PoziomRyzyka = instance.PoziomRyzyka;

                // Update odpowiedzi
                foreach (var answer in answers)
                {
                    var existingAnswer = await _context.InstanceAnswers
                        .FirstOrDefaultAsync(a =>
                            a.InstanceId == instance.Id &&
                            a.QuestionTemplateId == answer.QuestionTemplateId);

                    if (existingAnswer != null)
                    {
                        existingAnswer.OdpowiedzTekst = answer.OdpowiedzTekst;
                        existingAnswer.OpcjaOdpowiedziId = answer.OpcjaOdpowiedziId;
                        existingAnswer.WagaRyzykaPojedyncza = answer.WagaRyzykaPojedyncza;
                        existingAnswer.DataOdpowiedzi = DateTime.Now;
                    }
                    else
                    {
                        answer.InstanceId = instance.Id;
                        answer.DataOdpowiedzi = DateTime.Now;
                        _context.InstanceAnswers.Add(answer);
                    }
                }
            }

            return await _context.SaveChangesAsync();
        }

        //Niżej submit dla nowych formularzy
        public async Task<int> SubmitInstanceAsync(CompPlatformInstance instance, List<CompPlatformInstanceAnswer> answers)
        {
            instance.Status = "Do weryfikacji";
            instance.DataZlozenia = DateTime.Now;

            // Zapisuje draft
            await SaveDraftAsync(instance, answers);

            // Nowy status
            var existingInstance = await _context.InstancesTool
                .FirstOrDefaultAsync(i => i.Id == instance.Id);

            if (existingInstance != null)
            {
                existingInstance.Status = instance.Status;
                existingInstance.DataZlozenia = instance.DataZlozenia;
                existingInstance.KalkulacjaRyzyka = instance.KalkulacjaRyzyka;
                existingInstance.PoziomRyzyka = instance.PoziomRyzyka;
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateInstanceStatusAsync(int instanceId, string newStatus, string auditorId, string comment = null)
        {
            var instance = await _context.InstancesTool
                .FirstOrDefaultAsync(i => i.Id == instanceId);

            if (instance == null)
                return false;

            instance.Status = newStatus;
            instance.AudytorId = auditorId;
            instance.DataSprawdzenia = DateTime.Now;

            // TBD

            await _context.SaveChangesAsync();
            return true;
        }

        // Implementacja nowych metod dla historii weryfikacji
        public async Task<int> AddVerificationHistoryAsync(
            int instanceId,
            string auditorId,
            string newStatus,
            string comment,
            string oldStatus = null)
        {
            // Jeśli nie podano starego statusu, pobierz go z bazy
            if (string.IsNullOrEmpty(oldStatus))
            {
                var instance = await _context.InstancesTool
                    .FirstOrDefaultAsync(i => i.Id == instanceId);

                if (instance != null)
                {
                    oldStatus = instance.Status;
                }
            }

            var history = new CompPlatformVerificationHistory
            {
                InstanceId = instanceId,
                AuditorId = auditorId,
                OldStatus = oldStatus,
                NewStatus = newStatus,
                Comment = comment,
                VerificationDate = DateTime.Now
            };

            _context.VerificationHistory.Add(history);
            await _context.SaveChangesAsync();

            return history.Id;
        }

        public async Task<List<CompPlatformVerificationHistory>> GetVerificationHistoryAsync(int instanceId)
        {
            return await _context.VerificationHistory
                .Include(h => h.Auditor)
                .Where(h => h.InstanceId == instanceId)
                .OrderByDescending(h => h.VerificationDate)
                .ToListAsync();
        }

        public async Task<int> AddToolAsync(CompPlatformTool tool)
        {
            _context.Tools.Add(tool);
            await _context.SaveChangesAsync();
            return tool.Id;
        }
    }
}
