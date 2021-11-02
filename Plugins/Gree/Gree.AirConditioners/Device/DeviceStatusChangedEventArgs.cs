namespace Gree.AirConditioner.Device
{
    using System.Collections.Generic;

    internal class DeviceStatusChangedEventArgs
    {
        public Dictionary<string, int> Parameters { get; set; }
    }
}
