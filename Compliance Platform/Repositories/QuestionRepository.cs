using Compliance_Platform.Interfaces;
using Compliance_Platform.Model;
using Microsoft.EntityFrameworkCore;

namespace Compliance_Platform.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly CompPlatformDbContext _context;

        public QuestionRepository(CompPlatformDbContext context)
        {
            _context = context;
        }

        public async Task<List<CompPlatformQuestions>> GetAllQuestionsAsync()
        {
            return await _context.Questions
                .Include(q => q.Answers)
                .OrderBy(q => q.OrderIndex)
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

        public async Task<List<CompPlatformQuestions>> GetQuestionsByCategoryAsync(string category)
        {
            // If category is null or empty, return all questions
            if (string.IsNullOrEmpty(category))
            {
                return await GetAllQuestionsAsync();
            }

            return await _context.Questions
                .Include(q => q.Answers)
                .Where(q => q.Kategoria == category)
                .OrderBy(q => q.OrderIndex)
                .ToListAsync();
        }

        public async Task<CompPlatformQuestions> GetQuestionByIdAsync(int questionId)
        {
            return await _context.Questions
                .Include(q => q.Answers)
                .FirstOrDefaultAsync(q => q.Id == questionId);
        }

        public async Task<int> AddQuestionAsync(CompPlatformQuestions question)
        {
            _context.Questions.Add(question);
            await _context.SaveChangesAsync();
            return question.Id;
        }

        public async Task<bool> UpdateQuestionAsync(CompPlatformQuestions question)
        {
            var existingQuestion = await _context.Questions
                .FirstOrDefaultAsync(q => q.Id == question.Id);

            if (existingQuestion == null)
                return false;

            // Properties
            existingQuestion.Tresc = question.Tresc;
            existingQuestion.TypPytania = question.TypPytania;
            existingQuestion.OrderIndex = question.OrderIndex;
            existingQuestion.Kategoria = question.Kategoria;
            existingQuestion.Wymagane = question.Wymagane;
            existingQuestion.WagaRyzyka = question.WagaRyzyka;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteQuestionAsync(int questionId)
        {
            var question = await _context.Questions
                .FirstOrDefaultAsync(q => q.Id == questionId);

            if (question == null)
                return false;

            // Opcjonalnie: Do sprawdzania odpowiedzi
            var hasAnswers = await _context.InstanceAnswers
                .AnyAsync(a => a.QuestionTemplateId == questionId);

            if (hasAnswers)
                return false; // Nie można skasować odpowiedzi

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
            return true;
        }

        //Odwołanie do pobierania pytań
        public async Task<List<CompPlatformAnswers>> GetAnswersByQuestionIdAsync(int questionId)
        {
            return await _context.Answers
                .Where(a => a.QuestionTemplateId == questionId)
                .OrderBy(a => a.OrderIndex)
                .ToListAsync();
        }

        public async Task<CompPlatformAnswers> GetAnswerByIdAsync(int answerId)
        {
            return await _context.Answers
                .FirstOrDefaultAsync(a => a.Id == answerId);
        }

        //Zarządzanie pytaniami
        public async Task<int> AddAnswerAsync(CompPlatformAnswers answer)
        {
            _context.Answers.Add(answer);
            await _context.SaveChangesAsync();
            return answer.Id;
        }

        public async Task<bool> UpdateAnswerAsync(CompPlatformAnswers answer)
        {
            var existingAnswer = await _context.Answers
                .FirstOrDefaultAsync(a => a.Id == answer.Id);

            if (existingAnswer == null)
                return false;

            existingAnswer.Treść = answer.Treść;
            existingAnswer.WagaRyzyka = answer.WagaRyzyka;
            existingAnswer.OrderIndex = answer.OrderIndex;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAnswerAsync(int answerId)
        {
            var answer = await _context.Answers
                .FirstOrDefaultAsync(a => a.Id == answerId);

            if (answer == null)
                return false;

            // Sprawdzanie czy instancja odnosi się do odpowiedzi
            var hasInstanceAnswers = await _context.InstanceAnswers
                .AnyAsync(a => a.OpcjaOdpowiedziId == answerId);

            if (hasInstanceAnswers)
                return false; // Cannot delete an answer that is used

            _context.Answers.Remove(answer);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
