using IoTHomeAssistant.Domain.Enums;
using System.Threading.Tasks;

namespace IoTHomeAssistant.Domain.Services
{
    public interface IEventPublisher
    {
        Task PublishEvent(string eventName, object payload);
        Task PublishNotification(NotificationTypeEnum type, string message);
    }
}
