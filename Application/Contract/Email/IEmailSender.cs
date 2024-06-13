using Application.Models.Email;

namespace Application.Contract.Email
{
    public interface IEmailSender
    {
        Task<bool> SendEmailAsync(EmailMessage emailMessage);
    }
}
