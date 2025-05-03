using Compliance_Platform.Model;
using Compliance_Platform.Interfaces;

namespace Compliance_Platform.Classes
{
    public class QuestionnaireService
    {
        private readonly IQuestionRepository _repository;

        public QuestionnaireService(IQuestionRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CompPlatformQuestions>> GetQuestionsForToolCategoryAsync(string category)
        {
            // Pobieranie pytań
            var allQuestions = await _repository.GetAllQuestionsAsync();

            // Filtrowanie po kategorii (bierz wszystko, gdy brak kategorii)
            return string.IsNullOrEmpty(category)
                ? allQuestions.OrderBy(q => q.OrderIndex).ToList()
                : allQuestions.Where(q => q.Kategoria == category).OrderBy(q => q.OrderIndex).ToList();
        }
    }
}
