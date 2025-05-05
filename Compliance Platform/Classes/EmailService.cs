using Compliance_Platform.Interfaces;
using System.Net;
using System.Net.Mail;

namespace Compliance_Platform.Classes
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> SendStatusChangeNotificationAsync(
            string recipientEmail,
            int instanceId,
            string toolName,
            string newStatus,
            string comment)
        { 
            
            try
            {
                // Pobierz ustawienia z konfiguracji
                var smtpServer = _configuration["EmailSettings:SmtpServer"];
        var smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"]);
        var smtpUsername = _configuration["EmailSettings:SmtpUsername"];
        var smtpPassword = _configuration["EmailSettings:SmtpPassword"];
        var senderEmail = _configuration["EmailSettings:SenderEmail"];
        var senderName = _configuration["EmailSettings:SenderName"];
        var baseUrl = _configuration["AppSettings:BaseUrl"];

         // Utwórz klienta SMTP
                using var client = new SmtpClient(smtpServer, smtpPort)
    {
        EnableSsl = true,
                    Credentials = new NetworkCredential(smtpUsername, smtpPassword)
                };

    // Utwórz wiadomość
                var message = new MailMessage
                {
                    From = new MailAddress(senderEmail, senderName),
                    Subject = $"Aktualizacja statusu kwestionariusza - {toolName}",
                    IsBodyHtml = true
                };

    message.To.Add(recipientEmail);

                // Przygotuj treść wiadomości
                var statusDisplayName = GetStatusDisplayName(newStatus);
    var statusInfo = GetStatusDescription(newStatus);
    var buttonUrl = $"{baseUrl}/questionnaire-view/{instanceId}";

    var body = $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <style>
                        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
                        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
                        .header {{ background-color: #f0f0f0; padding: 15px; border-radius: 5px; }}
                        .content {{ padding: 20px 0; }}
                        .status {{ font-weight: bold; padding: 5px 10px; border-radius: 3px; }}
                        .status-approved {{ background-color: #d1e7dd; color: #0f5132; }}
                        .status-rejected {{ background-color: #f8d7da; color: #842029; }}
                        .status-revision {{ background-color: #fff3cd; color: #664d03; }}
                        .comment {{ background-color: #f8f9fa; padding: 15px; border-left: 4px solid #dee2e6; margin: 15px 0; }}
                        .button {{ display: inline-block; background-color: #0d6efd; color: white; padding: 10px 15px; text-decoration: none; border-radius: 5px; }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <div class='header'>
                            <h2>Aktualizacja statusu kwestionariusza</h2>
                        </div>
                        <div class='content'>
                            <p>Witaj,</p>
                            <p>Status kwestionariusza dla narzędzia <strong>{toolName}</strong> został zmieniony na 
                            <span class='status {GetStatusClass(newStatus)}'>{statusDisplayName}</span>.</p>
                            
                            <p>{statusInfo}</p>
                            
                            {(string.IsNullOrEmpty(comment) ? "" : $@"
                            <div class='comment'>
                                <strong>Komentarz od audytora:</strong>
                                <p>{comment}</p>
                            </div>")}
                            
                            <p>Możesz przejść do podglądu kwestionariusza, klikając poniższy przycisk:</p>
                            
                            <p>
                                <a href='{buttonUrl}' class='button'>Zobacz szczegóły kwestionariusza</a>
                            </p>
                            
                            <p>
                                Pozdrawiamy,<br>
                                Zespół Compliance Platform
                            </p>
                        </div>
                    </div>
                </body>
                </html>";

    // Wyślij wiadomość
    await client.SendMailAsync(message);
                return true;
            }
            catch (Exception)
            {
    // W rzeczywistej aplikacji należy obsłużyć i zalogować błąd
    return false;
}
        }

        private string GetStatusDisplayName(string status)
{
    return status switch
    {
        "Zatwierdzony" => "Zatwierdzony",
        "Do poprawy" => "Wymaga poprawy",
        "Odrzucony" => "Odrzucony",
        _ => status
    };
}

private string GetStatusClass(string status)
{
    return status switch
    {
        "Zatwierdzony" => "status-approved",
        "Do poprawy" => "status-revision",
        "Odrzucony" => "status-rejected",
        _ => ""
    };
}

private string GetStatusDescription(string status)
{
    return status switch
    {
        "Zatwierdzony" => "Twój kwestionariusz został zatwierdzony. Narzędzie zostało zarejestrowane w systemie i jest zgodne z wymogami regulacyjnymi AI Act.",
        "Do poprawy" => "Twój kwestionariusz wymaga pewnych poprawek. Prosimy o zapoznanie się z komentarzem audytora i wprowadzenie odpowiednich zmian.",
        "Odrzucony" => "Niestety, Twój kwestionariusz został odrzucony. Sprawdź komentarz audytora, aby poznać powody tej decyzji.",
        _ => "Status Twojego kwestionariusza został zaktualizowany."
    };
}
}}