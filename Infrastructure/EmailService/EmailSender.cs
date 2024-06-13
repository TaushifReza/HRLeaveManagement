using Application.Contract.Email;
using Application.Models.Email;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Infrastructure.EmailService
{
    public class EmailSender : IEmailSender
    {
        public EmailSetting _emailSettings { get; }

        public EmailSender(IOptions<EmailSetting> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }
        public async Task<bool> SendEmailAsync(EmailMessage emailMessage)
        {
            var client = new SendGridClient(_emailSettings.ApiKey);
            var to = new EmailAddress(emailMessage.To);
            var from = new EmailAddress
            {
                Email = _emailSettings.FromAddress,
                Name = _emailSettings.FromName
            };

            var message =
                MailHelper.CreateSingleEmail(from, to, emailMessage.Subject, emailMessage.Body, emailMessage.Body);
            var response = await client.SendEmailAsync(message);
            return response.IsSuccessStatusCode;
        }
    }
}
