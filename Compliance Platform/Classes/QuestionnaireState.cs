using Compliance_Platform.Interfaces;
using Compliance_Platform.Model;

namespace Compliance_Platform.Classes
{
    public class QuestionnaireState
    {
        public CompPlatformInstance CurrentInstance { get; private set; }
        //Wyłapywanie aktualnych pytan
        public Dictionary<int, CompPlatformInstanceAnswer> CurrentAnswers { get; private set;}
        public bool IsDirty { get; private set; }  //IsDirty - walidacja, czy doszły zmiany w trakcie

        //Tworzenie instancji - dla nowych formularzy
        public async Task CreateNewInstanceAsync(int toolId, int questionnaireId)
        {
            CurrentInstance = new CompPlatformInstance
            {
                ToolId = toolId,
                QuestionnaireId = questionnaireId,
                DataUtworzenia = DateTime.Now,
                Status = "Draft",
                KalkulacjaRyzyka = 0,
                PoziomRyzyka = "Nieznane"
            };

            CurrentAnswers.Clear();
            IsDirty = true;
        }

        //Ładowanie instancji
        public async Task LoadInstanceAsync(CompPlatformInstance instance)
        {
            CurrentInstance = instance;
            CurrentAnswers = instance.Answers?
                .ToDictionary(a => a.QuestionTemplateId, a => a) ?? new();
            IsDirty = false;
        }


        //Update odpowiedzi
        public void UpdateAnswer(int questionId, string textAnswer, int? optionId, decimal riskWeight) {
            if (CurrentAnswers.TryGetValue(questionId, out var existingAnswer))
            {
                existingAnswer.OdpowiedzTekst = textAnswer;
                existingAnswer.OpcjaOdpowiedziId = optionId;
                existingAnswer.WagaRyzykaPojedyncza = riskWeight;
                existingAnswer.DataOdpowiedzi = DateTime.Now;
            }
            else
            {
                CurrentAnswers[questionId] = new CompPlatformInstanceAnswer
                {
                    QuestionTemplateId = questionId,
                    InstanceId = CurrentInstance.Id,
                    OdpowiedzTekst = textAnswer,
                    OpcjaOdpowiedziId = optionId,
                    WagaRyzykaPojedyncza = riskWeight,
                    DataOdpowiedzi = DateTime.Now,
                    WnioskujacyId = "current-user-id" // Aktualny ID usera
                };
            }

            IsDirty = true;
        } 

        //Zapisanie draftu
        public async Task<bool> SaveDraftAsync(IQuestionnaireRepository repository) {

            try
            {
                await repository.SaveDraftAsync(CurrentInstance, CurrentAnswers.Values.ToList());
                IsDirty = false;
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
