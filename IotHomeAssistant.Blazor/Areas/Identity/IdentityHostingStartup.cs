using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(IoTHomeAssistant.Web.Areas.Identity.IdentityHostingStartup))]
namespace IoTHomeAssistant.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}