using Gree.AirConditioner.Device.Protocol;
using Gree.AirConditioner.Logging;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Gree.AirConditioner.Device
{
     internal class Controller
    {
        private readonly ILogger _logger;

        public Controller(string id, string ipAddress, string privateKey)
        {
            this.Parameters = new Dictionary<string, int>();
            
            DeviceId = id;
            PrivateKey = privateKey;
            IpAddress = ipAddress;
            _logger = Logger.CreateDefaultLogger();
        }

        public delegate void DeviceStatusChangedEventHandler(object sender, DeviceStatusChangedEventArgs e);

        public event DeviceStatusChangedEventHandler DeviceStatusChanged;

        public string DeviceId { get; private set; }
        public string IpAddress { get; private set; }
        public string PrivateKey { get; private set; }

        public Dictionary<string, int> Parameters { get; private set; }

        public async Task UpdateDeviceStatus()
        {
            var columns = typeof(DeviceParameterKeys).GetFields()
                .Where((f) => f.FieldType == typeof(string))
                .Select((f) => f.GetValue(null) as string)
                .ToList();

            var pack = DeviceStatusRequestPack.Create(DeviceId, columns);
            var json = JsonConvert.SerializeObject(pack);

            var encrypted = Crypto.EncryptData(json, PrivateKey);
            if (encrypted == null)
            {
                this._logger.LogWarning("Failed to encrypt DeviceStatusRequestPack");
                return;
            }

            var request = Request.Create(DeviceId, encrypted);

            ResponsePackInfo response;

            try
            {
                response = await this.SendDeviceRequest(request);
            }
            catch (Exception e)
            {
                this._logger.LogWarning($"Failed to send DeviceStatusRequestPack. Error: {e.Message}");
                return;
            }

            json = Crypto.DecryptData(response.Pack, PrivateKey);
            if (json == null)
            {
                this._logger.LogWarning("Failed to decrypt DeviceStatusResponsePack");
                return;
            }

            var responsePack = JsonConvert.DeserializeObject<DeviceStatusResponsePack>(json);
            if (responsePack == null)
            {
                this._logger.LogWarning("Failed to deserialize DeviceStatusReponsePack");
                return;
            }

            this.Parameters = responsePack.Columns
                .Zip(responsePack.Values, (k, v) => new { k, v })
                .ToDictionary(x => x.k, x => x.v);
            
            this.DeviceStatusChanged?.Invoke(
                this, 
                new DeviceStatusChangedEventArgs()
                {
                    Parameters = this.Parameters
                });
        }

        public async Task SetDeviceParameter(string name, int value)
        {
            this._logger.LogDebug($"Setting parameter: {name}={value}");

            var pack = CommandRequestPack.Create(
                this.DeviceId,
                new List<string>() { name },
                new List<int>() { value });

            var json = JsonConvert.SerializeObject(pack);
            var request = Request.Create(this.DeviceId, Crypto.EncryptData(json, PrivateKey));

            ResponsePackInfo response;
            try
            {
                response = await this.SendDeviceRequest(request);
            }
            catch (System.IO.IOException e)
            {
                this._logger.LogWarning($"Failed to send CommandRequestPack: {e.Message}");
                return;
            }

            json = Crypto.DecryptData(response.Pack, PrivateKey);
            var responsePack = JsonConvert.DeserializeObject<CommandResponsePack>(json);

            if (!responsePack.Columns.Contains(name))
            {
                this._logger.LogWarning("Parameter cannot be changed.");
            }
        }

        private async Task<ResponsePackInfo> SendDeviceRequest(Request request)
        {
            this._logger.LogDebug($"Sending device request");

            var datagram = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(request));
            this._logger.LogDebug($"{datagram.Length} bytes will be sent");

            using (var udp = new UdpClient())
            {
                var sent = await udp.SendAsync(datagram, datagram.Length, IpAddress, 7000);
                this._logger.LogDebug($"{sent} bytes sent to {IpAddress}");

                for (int i = 0; i < 20; ++i)
                {
                    if (udp.Available > 0)
                    {
                        var results = await udp.ReceiveAsync();
                        this._logger.LogDebug($"Got response, {results.Buffer.Length} bytes");

                        var json = Encoding.ASCII.GetString(results.Buffer);
                        var response = JsonConvert.DeserializeObject<ResponsePackInfo>(json);

                        return response;
                    }

                    await Task.Delay(100);
                }

                this._logger.LogWarning("Request timed out");

                throw new System.IO.IOException("Device request timed out");
            }
        }
    }
}