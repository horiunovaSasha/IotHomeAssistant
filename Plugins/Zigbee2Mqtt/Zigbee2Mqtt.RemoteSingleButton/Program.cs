using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Zigbee2Mqtt.RemoteSingleButton
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            await new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<MqttBackgroundService>();
                })
            .RunConsoleAsync();
        }

    }
}