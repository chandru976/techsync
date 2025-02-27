using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

public class EmailService
{
    private readonly string _apiKey;

    public EmailService(string apiKey)
    {
        _apiKey = apiKey;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        var client = new SendGridClient(_apiKey);
        var from = new EmailAddress("no-reply@techsync.com", "TechSync");
        var to = new EmailAddress(toEmail);
        var msg = MailHelper.CreateSingleEmail(from, to, subject, message, message);
        await client.SendEmailAsync(msg);
    }
}