using Compliance_Platform.Model;

namespace Compliance_Platform.Interfaces
{
    public interface IQuestionRepository
    {
        Task<List<CompPlatformQuestions>> GetAllQuestionsAsync();
        Task<List<CompPlatformQuestions>> GetQuestionsByQuestionnaireAsync(int questionnaireId);
        Task<List<CompPlatformQuestions>> GetQuestionsByCategoryAsync(string category);
        Task<CompPlatformQuestions> GetQuestionByIdAsync(int questionId);

        //Zarządzanie pytaniami
        Task<int> AddQuestionAsync(CompPlatformQuestions question);
        Task<bool> UpdateQuestionAsync(CompPlatformQuestions question);
        Task<bool> DeleteQuestionAsync(int questionId);

        Task<List<CompPlatformAnswers>> GetAnswersByQuestionIdAsync(int questionId);
        Task<CompPlatformAnswers> GetAnswerByIdAsync(int answerId);

        Task<int> AddAnswerAsync(CompPlatformAnswers answer);
        Task<bool> UpdateAnswerAsync(CompPlatformAnswers answer);
        Task<bool> DeleteAnswerAsync(int answerId);
    }
}
