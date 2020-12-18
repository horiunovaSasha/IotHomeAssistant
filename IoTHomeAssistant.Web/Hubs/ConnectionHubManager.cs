using System.Collections.Generic;

namespace IoTHomeAssistant.Web.Hubs
{
    public class ConnectionHubManager
    {
        private static Dictionary<int, List<string>> connections = new Dictionary<int, List<string>>();
        private static string locker = string.Empty;
        public void Add(int deviceTopicId, string connectionId)
        {
            lock (locker)
            {
                if (!connections.ContainsKey(deviceTopicId))
                {
                    connections.Add(deviceTopicId, new List<string>());
                }

                connections[deviceTopicId].Add(connectionId);
            }
        }
        public void Remove(string connectionId)
        {
            lock (locker)
            {
                foreach (var deviceTopicId in connections.Keys)
                {
                    if (connections.ContainsKey(deviceTopicId) && connections[deviceTopicId].Contains(connectionId)) 
                    { 
                        connections[deviceTopicId].Remove(connectionId);
                        break;
                    }
                }
            }
        }
        public List<string> GetConnections(int deviceTopicId)
        {
            if (connections.ContainsKey(deviceTopicId))
            {
                lock (locker)
                {
                    return connections[deviceTopicId];
                }
            }

            return new List<string>();
        }
    }
}
