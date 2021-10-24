using System.Threading.Tasks;

namespace IoTHomeAssistant.Domain.Services
{
    public interface ICommandService
    {
        Task Exec(int deviceId, string command, object value = null);
        Task GetStatus(int deviceId);
    }
}
