using OAOPS.Shared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAOPS.Shared.Services
{
    public class EmailService : IEmailService
    {
        public EmailSender EmailSender { get; set; }
        public ApplicationDbContext Db { get; }

        public EmailService(EmailSender emailSender, ApplicationDbContext db)
        {
            EmailSender = emailSender;
            Db = db;
        }

        private async Task<List<string>> GetAllNotificationEmails()
        {
            var emailUsers = await Db.User_Has_Notifications.Include(x => x.User).Include(x => x.Notification).Select(x => x.User.Email).ToListAsync();
            return emailUsers;
        }

        public async Task SendArticleAlmostEmptyMail(string storageName, string slotName, int remainingAmount, string ArticleName)
        {
            // TODO: change Path to relative
            var fileData = await File.ReadAllTextAsync("C:\\Users\\Haunschmied.Bastian\\Documents\\GitHub\\Bezahlsystem\\Bezahlsystem\\OAOPS\\Server\\Pages\\articleAlmostEmpty.html");
            fileData = fileData.Replace("{{ArticleName}}", ArticleName)
                                .Replace("{{storageName}}",storageName)
                                .Replace("{{remainingAmount}}", remainingAmount.ToString())
                                .Replace("{{slotName}}", slotName);

            foreach (var email in await GetAllNotificationEmails())
            {
                await EmailSender.SendEmailAsync(email, "Artikel fast leer", fileData);
            }
        }

        public async Task SendArticleEmptyMail(string storageName, string slotName, string ArticleName)
        {
            var fileData = await File.ReadAllTextAsync("C:\\Users\\Haunschmied.Bastian\\Documents\\GitHub\\Bezahlsystem\\Bezahlsystem\\OAOPS\\Server\\Pages\\articleEmpty.html");
            fileData = fileData.Replace("{{ArticleName}}", ArticleName)
                                .Replace("{{storageName}}", storageName)
                                .Replace("{{slotName}}", slotName);

            foreach (var email in await GetAllNotificationEmails())
            {
                await EmailSender.SendEmailAsync(email, "Artikel leer", fileData);
            }
        }
    }
}
