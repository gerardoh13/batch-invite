using Data.Providers;
using Models;
using Models.Domain;
using Models.Requests;
using Models.Requests.Emails;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Services
{
    public class EmailService : IEmailService
    {
        private IDataProvider _dataProvider = null;

        public EmailService(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        private async Task SendEmail(SendGridMessage msg)
        {
            var apiKey = REDACTED;
            var client = new SendGridClient(apiKey);
            await client.SendEmailAsync(msg);
        }

        public async Task InviteContributor(InviteContributorRequest model)

        {
            string directory = Directory.GetCurrentDirectory();
            var path = Path.Combine(directory, "EmailTemplates\\InviteContributors.html");
            var htmlContent = System.IO.File.ReadAllText(path);
            htmlContent = htmlContent.Replace("{&To}", model.To);
            htmlContent = htmlContent.Replace("{&FirstName}", model.FirstName);
            htmlContent = htmlContent.Replace("{&LastName}", model.LastName);
            htmlContent = htmlContent.Replace("{&ContributorType}", model.ContributionType);
            htmlContent = htmlContent.Replace("{&Url}", " https://localhost:3000/confirmeventinvite?:" + model.Token);

            var msg = new SendGridMessage()
            {

                From = new EmailAddress("ADDI@addi.com"),
                Subject = "You've been invited!",
                HtmlContent = htmlContent,
            };
            msg.AddTo(model.To, model.FirstName);
            await SendEmail(msg);
        }

    }

}
