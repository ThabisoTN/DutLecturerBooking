using System.Net.Mail;
using System.Net;
using System.Text.Encodings.Web;

namespace DutLecturerBooking.Services
{
    public interface IEmailService
    {
        Task<bool> SendConfirmationEmailAsync(string email, string callbackUrl); 
        Task<bool> SendEmailAsync(string email, string subject, string message);
    }

    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<bool> SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                MailMessage message = new MailMessage
                {
                    From = new MailAddress("Go4toro@faniehome.com"),
                    Subject = subject,
                    IsBodyHtml = true,
                    Body = htmlMessage
                };

                message.To.Add(email);

                using (SmtpClient smtpClient = new SmtpClient())
                {
                    smtpClient.Port = 587;  // SMTP port
                    smtpClient.Host = "smtp.office365.com";  // SMTP server
                    smtpClient.EnableSsl = true;  // Enable SSL
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential("Go4toro@faniehome.com", "Mossgert@2018");
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                    // Send the email
                    await smtpClient.SendMailAsync(message);
                }

                return true;
            }
            catch (SmtpException smtpEx)
            {
                _logger.LogError($"SMTP error sending email: {smtpEx.Message}");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error sending email: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> SendConfirmationEmailAsync(string email, string callbackUrl)
        {
            string subject = "Confirm your email";
            string body = $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.";

            return await SendEmailAsync(email, subject, body);
        }
    }
}
