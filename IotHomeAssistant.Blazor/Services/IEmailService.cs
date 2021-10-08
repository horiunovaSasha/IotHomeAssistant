using System.Threading.Tasks;

namespace IoTHomeAssistant.Blazor.Services
{
    public interface IEmailService
    {
        public Task SendEmailAsync(string email, string subject, string message);
    }
}