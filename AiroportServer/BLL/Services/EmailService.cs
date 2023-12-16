using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class EmailService
    {

        private IConfiguration _configuration { get; set; }

        public EmailService(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public void SendEmailConfirmation(string token, string email)
        {
            string url = "http://localhost:4200/auth/confirm";
            string subject = "Підтвердження пошти";
            string body = $"Перейдіть за посилання для підтвердження пошти: {url}/{token}";

            SendEmail(email, subject, body);
        }

        public void SendPasswordResetEmail(string token, string email)
        {

            string subject = "Відновлення паролю";
            string body = $"Код верифікації для відновлення паролю: {token}";

            SendEmail(email, subject, body);
        }

        private void SendEmail(string recipient, string subject, string body)
        {
            try
            {
                string emailUsername = _configuration["EmailSettings:Username"];
                string emailPassword = _configuration["EmailSettings:Password"];
                string emailHost = _configuration["EmailSettings:Host"];
                int emailPort = int.Parse(_configuration["EmailSettings:Port"]);

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress(emailUsername),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(new MailAddress(recipient));

                SmtpClient smtpClient = new SmtpClient(emailHost, emailPort)
                {
                    Credentials = new NetworkCredential(emailUsername, emailPassword),
                    EnableSsl = true
                };
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка відправки електронної пошти: " + ex.Message);
            }
        }
    }
}
