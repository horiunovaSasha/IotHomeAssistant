using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace IoTHomeAssistant.Web.Hubs
{
    public class NotificationHub
    {
        private readonly ConnectionHubManager _connectionHub;
        private readonly IHubContext<DeviceHub> _hubContext;

        public NotificationHub(
            ConnectionHubManager connectionHub,
            IHubContext<DeviceHub> hubContext)
        {
            _connectionHub = connectionHub;
            _hubContext = hubContext;
        }

        public async Task NotifyDevice(int deviceTopicId, string payload)
        {
            foreach (var connectionId in _connectionHub.GetConnections(deviceTopicId))
            {
                await _hubContext.Clients.Client(connectionId).SendAsync("ReceiveData", payload);
            }
        }
    }
}
