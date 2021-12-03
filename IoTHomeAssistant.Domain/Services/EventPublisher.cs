using IoTHomeAssistant.Domain.Enums;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace IoTHomeAssistant.Domain.Services
{
    public class EventPublisher : Hub, IEventPublisher
    {
        public async Task PublishEvent(string eventName, object payload)
        {
            await Clients.All.SendAsync(eventName, payload);
        }

        public async Task PublishNotification(NotificationTypeEnum type, string message)
        {
            await Clients.All.SendAsync("notification", type, message);
        }
    }
}