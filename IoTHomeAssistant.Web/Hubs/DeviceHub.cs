using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace IoTHomeAssistant.Web.Hubs
{
    public class DeviceHub : Hub
    {
        private readonly ConnectionHubManager _connectionHub;        

        public DeviceHub(ConnectionHubManager connectionHub)
        {
            _connectionHub = connectionHub;
        }

        public async Task NotifyDevice(string connectionId, string payload)
        {
            await Clients.Client(connectionId).SendAsync("ReceiveData", payload);
        }

        public string GetConnectionId(int deviceTopicId)
        {
            var connectionId = Context.ConnectionId;

            _connectionHub.Add(deviceTopicId, connectionId);

            return connectionId;
        }

    }
}
