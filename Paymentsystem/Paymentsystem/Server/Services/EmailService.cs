using Paymentsystem.Shared.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Paymentsystem.Server.Services
{
    public class EmailService
    {
        private readonly IConfiguration _config;
        private readonly string apiKey;
        private readonly string senderMail;
        private readonly string senderName;

        public EmailService(IConfiguration config)
        {
            _config = config;
            apiKey = config.GetSection("Email").GetValue<string>("ApiKey");
            senderMail = config.GetSection("Email").GetValue<string>("SenderMail");
            senderMail = config.GetSection("Email").GetValue<string>("SenderName");
        }

        public async Task<int> SendMailToUser(string adress, string recieverName, string subject, string htmlBody, string? body = null)
        {
            var client = new SendGridClient(apiKey);
            var senderEmail = new EmailAddress(senderMail, senderName);
            var recieverMail = new EmailAddress(adress, recieverName);
            var msg = MailHelper.CreateSingleEmail(senderEmail, recieverMail, subject, body, htmlBody);

            var res = await client.SendEmailAsync(msg).ConfigureAwait(false);
            if (res.IsSuccessStatusCode)
            {
                return 21;
            }

            return 20;
        }
    }
}
