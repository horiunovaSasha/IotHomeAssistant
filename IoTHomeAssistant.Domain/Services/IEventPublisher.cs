using System.Threading.Tasks;

namespace IoTHomeAssistant.Domain.Services
{
    public interface IEventPublisher
    {
        Task PublishEvent(string eventName, object payload);
    }
}
