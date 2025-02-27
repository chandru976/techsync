using SendGrid;
using SendGrid.Helpers.Mail;

public class EmailService
{
    private readonly IConfiguration _config;
    public EmailService(IConfiguration config) => _config = config;

    public async Task SendVerificationEmail(string email, string otp)
    {
        var apiKey = _config["SendGrid:ApiKey"];
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress(_config["SendGrid:FromEmail"], _config["SendGrid:FromName"]);
        var to = new EmailAddress(email);
        var subject = "Verify Your TechSync Account";
        var htmlContent = $"Your OTP is <strong>{otp}</strong>. It expires in 10 minutes.";
        
        var msg = MailHelper.CreateSingleEmail(from, to, subject, null, htmlContent);
        await client.SendEmailAsync(msg);
    }
}