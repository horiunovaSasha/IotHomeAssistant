using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace IoTHomeAssistant.Domain.Services
{
    public class EventPublisher : Hub, IEventPublisher
    {
        public async Task PublishEvent(string eventName, string payload)
        {
            await Clients.All.SendAsync(eventName, payload);
        }
    }
}