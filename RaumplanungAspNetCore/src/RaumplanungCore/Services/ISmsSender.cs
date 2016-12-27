using System.Threading.Tasks;

namespace RaumplanungCore.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
