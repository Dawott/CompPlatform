using Compliance_Platform.Model;

namespace Compliance_Platform.Interfaces
{
    public interface IQuestionnaireRepository
    {
        Task<CompPlatformQuestionnaires> GetQuestionnaireAsync(int questionnaireId);
        Task<List<CompPlatformQuestionnaires>> GetActiveQuestionnairesAsync();

        Task<CompPlatformTool> GetToolAsync(int toolId);
        Task<List<CompPlatformTool>> GetToolsByOwnerAsync(string ownerId);
        Task<List<CompPlatformTool>> GetAllToolsAsync();

        Task<CompPlatformInstance> GetInstanceAsync(int instanceId);
        Task<CompPlatformInstance> GetInstanceDraftAsync(int toolId, int questionnaireId);
        Task<List<CompPlatformInstance>> GetInstancesByStatusAsync(string status);
        Task<List<CompPlatformInstance>> GetInstancesByToolAsync(int toolId);

        Task<List<CompPlatformQuestions>> GetQuestionsByQuestionnaireAsync(int questionnaireId);
        Task<List<CompPlatformAnswers>> GetAnswersByQuestionAsync(int questionId);
        Task<List<CompPlatformInstanceAnswer>> GetInstanceAnswersAsync(int instanceId);

        Task<int> SaveDraftAsync(CompPlatformInstance instance, List<CompPlatformInstanceAnswer> answers);
        Task<int> SubmitInstanceAsync(CompPlatformInstance instance, List<CompPlatformInstanceAnswer> answers);
        Task<bool> UpdateInstanceStatusAsync(int instanceId, string newStatus, string auditorId, string comment = null);

        Task<int> AddVerificationHistoryAsync(int instanceId, string auditorId, string newStatus, string comment, string oldStatus);
        Task<List<CompPlatformVerificationHistory>> GetVerificationHistoryAsync(int instanceId);
    }
}
