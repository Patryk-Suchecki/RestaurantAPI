using System.Net.Mail;
using System.Net;

namespace RestaurantAPI.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string to, string subject, string body);
    }

    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;

        public EmailSender(EmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            try
            {
                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.Host = _emailSettings.Host;
                    smtpClient.Port = _emailSettings.Port;
                    smtpClient.EnableSsl = _emailSettings.EnableSsl;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password);

                    using (var mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress(_emailSettings.From);
                        mailMessage.To.Add(to);
                        mailMessage.Subject = subject;
                        mailMessage.Body = body;
                        await smtpClient.SendMailAsync(mailMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Wystąpił błąd podczas wysyłania wiadomości email.", ex);
            }
        }
    }
}
