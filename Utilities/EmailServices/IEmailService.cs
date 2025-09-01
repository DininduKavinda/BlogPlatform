namespace TaskManagement.Utilities.EmailServices
{
    public interface IEmailService
    {
        Task SendPasswordResetEmailAsync(string email, string token);
        Task SendWelcomeEmailAsync(string email);
        Task SendEmailAsync(string email, string subject, string body);
    }
}