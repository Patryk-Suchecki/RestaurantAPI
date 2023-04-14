using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace RestaurantAPI.Services
{
    public interface IEmailSender
    {
        void SendEmailAsync(string to, string subject, string body);
    }

    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;

        public EmailSender(EmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
        }

        public void SendEmailAsync(string to, string subject, string body)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_emailSettings.From, _emailSettings.From));
                message.To.Add(new MailboxAddress(to, to));
                message.Subject = subject;

                var builder = new BodyBuilder();
                builder.HtmlBody = body;
                message.Body = builder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Connect(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.StartTls);
                    client.Authenticate(_emailSettings.Username, _emailSettings.Password);
                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Wystąpił błąd podczas wysyłania wiadomości email.", ex);
            }
        }
    }
}
