using IotHomeAssistant.Blazor.Areas.Identity;
using IotHomeAssistant.Blazor.Data;
using IoTHomeAssistant.Domain.Options;
using IoTHomeAssistant.Domain.Repositories;
using IoTHomeAssistant.Domain.Services;
using IoTHomeAssistant.Infrastructure.EntityConfigurations;
using IoTHomeAssistant.Infrastructure.Repositories;
using IoTHomeAssistant.Blazor.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Syncfusion.Blazor;
using Syncfusion.Licensing;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace IotHomeAssistant.Blazor
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                 .AddRoles<IdentityRole>()
                 .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.Configure<MqttOption>(Configuration.GetSection("MqttConfiguration"));

            services.AddDbContext<IoTDbContext>(options =>
                options.UseSqlite(
                    Configuration.GetConnectionString("IoTDefaultConnection")));

            services.AddTransient<IIconRepository, IconRepository>();
            services.AddTransient<IPluginRepository, PluginRepository>();
            services.AddTransient<IDeviceRepository, DeviceRepository>();
            services.AddTransient<IJobTaskRepository, JobTaskRepository>();
            services.AddTransient<IAreaRepository, AreaRepository>();
            services.AddTransient<IWidgetItemRepository, WidgetItemRepository>();
            services.AddTransient<IDeviceTypeEventRepository, DeviceTypeEventRepository>();
            services.AddTransient<IDeviceTypeCommandRepository, DeviceTypeCommandRepository>();

            services.AddTransient<IDeviceService, DeviceService>();
            services.AddTransient<IPluginService, PluginService>();
            services.AddTransient<IJobTaskService, JobTaskService>();            
            services.AddTransient<IAreaService, AreaService>();
            services.AddTransient<IWidgetService, WidgetService>();
            services.AddTransient<ICommandService, CommandService>();

            services.AddTransient<IEventPublisher, EventPublisher>();
            
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IActionContextAccessor, ActionContextAccessor>();
            services.AddTransient<UserManager<IdentityUser>>();

            services.AddSingleton<JobTaskBackgroundService>();

            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });

            services.AddMvc(options =>
            {
                options.Filters.Add(new AuthorizeFilter());
            });


            services.AddSyncfusionBlazor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           SyncfusionLicenseProvider.RegisterLicense("NTE1MDAwQDMxMzkyZTMzMmUzMEcxa2xzOVZjN3I3OHNqVWR1K3BROGlSR3JtNlkwMlpMS2tweWdPbDhPeXM9");
           app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapHub<EventPublisher>("/event-publisher");
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
