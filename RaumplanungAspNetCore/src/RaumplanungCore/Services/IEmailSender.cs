using System.Threading.Tasks;

namespace RaumplanungCore.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string username, string email, string subject, string message);
    }
}
