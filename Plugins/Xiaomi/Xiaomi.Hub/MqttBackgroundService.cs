using Microsoft.Extensions.Hosting;
using MiHomeLib;
using MiHomeLib.Devices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Xiaomi.Hub
{
    public class MqttBackgroundService : IHostedService, IDisposable
    {
        private const string MQTT_CLIENT_ID = "XiaomiHub";

        private const string XIAOMI_GET_HUMIDITY = "xiaomi_get_humidity";
        private const string XIAOMI_GET_TEMPERATURE = "xiaomi_get_temperature";
        private const string XIAOMI_GET_DOORWINDOW = "xiaomi_get_doorwindow";
        private const string XIAOMI_GET_MOTION = "xiaomi_get_motion";
        private const string XIAOMI_GET_WATERLEAK = "xiaomi_get_waterleak";

        private const string XIAOMI_HUMIDITY_CHANGED = "xiaomi_humidity_changed";
        private const string XIAOMI_TEMPERATURE_CHANGED = "xiaomi_temperature_changed";
        private const string XIAOMI_DOORWINDOW_CLOSED = "xiaomi_doorwindow_closed";
        private const string XIAOMI_DOORWINDOW_OPENED = "xiaomi_doorwindow_opened";
        private const string XIAOMI_MOTION_DETECTED = "xiaomi_motion_detected";
        private const string XIAOMI_MOTION_STOPPED = "xiaomi_motion_stopped";
        private const string XIAOMI_WATERLEAK_DETECTED = "xiaomi_waterleak_detected";
        private const string XIAOMI_WATERLEAK_STOPPED = "xiaomi_waterleak_stopped";

        private readonly MqttClient _client;
        private readonly MiHome _miHome;

        private Dictionary<string, ThSensor> _thSensors;
        private Dictionary<string, DoorWindowSensor> _doorWidowSensors;
        private Dictionary<string, MotionSensor> _motionSensors;
        private Dictionary<string, AqaraMotionSensor> _aqaraMotionSensors;
        private Dictionary<string, WaterLeakSensor> _waterLeakSensors;

        public MqttBackgroundService()
        {
            _client = new MqttClient(VariableExtension.MQTT_ADDR);
            _miHome = new MiHome(VariableExtension.HUB_ID);

            _thSensors = new Dictionary<string, ThSensor>();
            _doorWidowSensors = new Dictionary<string, DoorWindowSensor>();
            _motionSensors = new Dictionary<string, MotionSensor>();
            _aqaraMotionSensors = new Dictionary<string, AqaraMotionSensor>();
            _waterLeakSensors = new Dictionary<string, WaterLeakSensor>();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _client.Connect(MQTT_CLIENT_ID);

            _client.MqttMsgPublishReceived += (object sender, MqttMsgPublishEventArgs e) =>
            {
                var id = Encoding.UTF8.GetString(e.Message);

                switch(e.Topic)
                {
                    case XIAOMI_GET_HUMIDITY:
                        SendHumidity(id);
                        break;
                    case XIAOMI_GET_TEMPERATURE:
                        SendTemperature(id);
                        break;
                    case XIAOMI_GET_DOORWINDOW:
                        SendDoorWindowState(id);
                        break;
                    case XIAOMI_GET_MOTION:
                        SendMotionState(id);
                        SendAqaraMotionState(id);
                        break;
                    case XIAOMI_GET_WATERLEAK:
                        SendWaterLeakState(id);
                        break;
                }
            };

            _client.Subscribe(new string[] { XIAOMI_GET_HUMIDITY }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            _client.Subscribe(new string[] { XIAOMI_GET_TEMPERATURE }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            _client.Subscribe(new string[] { XIAOMI_GET_DOORWINDOW }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            _client.Subscribe(new string[] { XIAOMI_GET_MOTION }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            _client.Subscribe(new string[] { XIAOMI_GET_WATERLEAK }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });

            _miHome.OnThSensor += (_, thSensor) =>
            {
                if (!_thSensors.ContainsKey(thSensor.Sid))
                {
                    Console.WriteLine($"Discovered TH: {thSensor.Name} {thSensor.Sid}");
                    _thSensors.Add(thSensor.Sid, thSensor);
                }

                thSensor.OnHumidityChange += (_, e) =>
                {
                    Console.WriteLine($"Humidity Change: {e.Humidity}");
                    _client.Publish($"{XIAOMI_HUMIDITY_CHANGED}_{thSensor.Sid}", Encoding.UTF8.GetBytes(e.Humidity.ToString()));
                };

                thSensor.OnTemperatureChange += (_, e) =>
                {
                    Console.WriteLine($"Temperature Change: {e.Temperature}");
                    _client.Publish($"{XIAOMI_TEMPERATURE_CHANGED}_{thSensor.Sid}", Encoding.UTF8.GetBytes(e.Temperature.ToString()));
                };
            };

            _miHome.OnDoorWindowSensor += (_, sensor) =>
            {
                Console.WriteLine($"Discovered DoorWindowSensor: {sensor.Name} {sensor.Sid}");

                if (!_doorWidowSensors.ContainsKey(sensor.Sid))
                {
                    _doorWidowSensors.Add(sensor.Sid, sensor);
                }

                sensor.OnClose += (_, e) =>
                {
                    Console.WriteLine($"OnClose: {sensor.Sid}");
                    _client.Publish($"{XIAOMI_DOORWINDOW_CLOSED}_{sensor.Sid}", new byte[] { });
                };

                sensor.OnOpen += (_, e) =>
                {
                    Console.WriteLine($"OnOpen: {sensor.Sid}");
                    _client.Publish($"{XIAOMI_DOORWINDOW_OPENED}_{sensor.Sid}", new byte[] { });
                };
            };

            _miHome.OnMotionSensor += (_, sensor) =>
            {
                Console.WriteLine($"Discovered MotionSensor: {sensor.Name} {sensor.Sid}");
                if (!_motionSensors.ContainsKey(sensor.Sid))
                {
                    _motionSensors.Add(sensor.Sid, sensor);
                }

                sensor.OnMotion += (_, e) => {
                    Console.WriteLine($"OnMotion: {sensor.Sid}");
                    _client.Publish($"{XIAOMI_MOTION_DETECTED}_{sensor.Sid}", new byte[] { });
                };
                
                sensor.OnNoMotion += (_, e) => {
                    Console.WriteLine($"OnNoMotion: {sensor.Sid}");
                    _client.Publish($"{XIAOMI_MOTION_STOPPED}_{sensor.Sid}", new byte[] { });
                };
            };

            _miHome.OnAqaraMotionSensor += (_, sensor) =>
            {
                Console.WriteLine($"Discovered AqaraMotionSensor: {sensor.Name} {sensor.Sid}");
                if (!_motionSensors.ContainsKey(sensor.Sid))
                {
                    _aqaraMotionSensors.Add(sensor.Sid, sensor);
                }

                sensor.OnMotion += (_, e) => {
                    Console.WriteLine($"OnAqaraMotion: {sensor.Sid}");
                    _client.Publish($"{XIAOMI_MOTION_DETECTED}_{sensor.Sid}", new byte[] { });
                };

                sensor.OnNoMotion += (_, e) => {
                    Console.WriteLine($"OnAqaraNoMotion: {sensor.Sid}");
                    _client.Publish($"{XIAOMI_MOTION_STOPPED}_{sensor.Sid}", new byte[] { });
                };
            };

            _miHome.OnWaterLeakSensor += (_, sensor) => {
                Console.WriteLine($"Discovered WaterLeak: {sensor.Name} {sensor.Sid}");
                
                if (!_waterLeakSensors.ContainsKey(sensor.Sid))
                {
                    _waterLeakSensors.Add(sensor.Sid, sensor);
                }

                sensor.OnLeak += (_, e) => {
                    Console.WriteLine($"OnLeak: {sensor.Sid}");
                    _client.Publish($"{XIAOMI_WATERLEAK_DETECTED}_{sensor.Sid}", new byte[] { });
                };

                sensor.OnNoLeak += (_, e) => {
                    Console.WriteLine($"OnNoLeak: {sensor.Sid}");
                    _client.Publish($"{XIAOMI_WATERLEAK_STOPPED}_{sensor.Sid}", new byte[] { });
                };
            };

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _client.Disconnect();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _client.Disconnect();
        }

        private void SendTemperature(string id)
        {
            if (_thSensors.ContainsKey(id))
            {
                var sensor = _thSensors[id];
                _client.Publish($"{XIAOMI_TEMPERATURE_CHANGED}_{sensor.Sid}", 
                    Encoding.UTF8.GetBytes(sensor.Temperature.ToString()));
            }
        }

        private void SendHumidity(string id)
        {
            if (_thSensors.ContainsKey(id))
            {
                var sensor = _thSensors[id];
                _client.Publish($"{XIAOMI_HUMIDITY_CHANGED}_{sensor.Sid}",
                    Encoding.UTF8.GetBytes(sensor.Humidity.ToString()));
            }
        }

        private void SendDoorWindowState(string id)
        {
            if (_doorWidowSensors.ContainsKey(id))
            {
                var sensor = _doorWidowSensors[id];

                if (sensor.Status == "Open")
                {
                    _client.Publish($"{XIAOMI_DOORWINDOW_OPENED}_{sensor.Sid}", new byte[] { });
                }
                else
                {
                    _client.Publish($"{XIAOMI_DOORWINDOW_CLOSED}_{sensor.Sid}", new byte[] { });
                }
            }
        }

        private void SendMotionState(string id)
        {
            if (_motionSensors.ContainsKey(id))
            {
                var sensor = _motionSensors[id];

                if (sensor.Status == "Move")
                {
                    _client.Publish($"{XIAOMI_MOTION_DETECTED}_{sensor.Sid}", new byte[] { });
                }
                else
                {
                    _client.Publish($"{XIAOMI_MOTION_STOPPED}_{sensor.Sid}", new byte[] { });
                }
            }
        }

        private void SendAqaraMotionState(string id)
        {
            if (_aqaraMotionSensors.ContainsKey(id))
            {
                var sensor = _aqaraMotionSensors[id];

                if (sensor.Status == "Move")
                {
                    _client.Publish($"{XIAOMI_MOTION_DETECTED}_{sensor.Sid}", new byte[] { });
                }
                else
                {
                    _client.Publish($"{XIAOMI_MOTION_STOPPED}_{sensor.Sid}", new byte[] { });
                }
            }
        }

        private void SendWaterLeakState(string id)
        {
            if (_waterLeakSensors.ContainsKey(id))
            {
                var sensor = _waterLeakSensors[id];

                if (sensor.Status == "Leak")
                {
                    _client.Publish($"{XIAOMI_WATERLEAK_DETECTED}_{sensor.Sid}", new byte[] { });
                }
                else
                {
                    _client.Publish($"{XIAOMI_WATERLEAK_STOPPED}_{sensor.Sid}", new byte[] { });
                }
            }
        }
    }
}