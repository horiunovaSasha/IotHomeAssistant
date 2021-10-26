using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace Xiaomi.Temperature
{
    class Program
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
