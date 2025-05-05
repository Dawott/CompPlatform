namespace Compliance_Platform.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendStatusChangeNotificationAsync(
            string recipientEmail,
            int instanceId,
            string toolName,
            string newStatus,
            string comment);
    }
}
