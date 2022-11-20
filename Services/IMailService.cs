using ResolutionManagementSystem.Models;

namespace ResolutionManagementSystem.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}