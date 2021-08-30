using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using IoTHomeAssistant.Web.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IoTHomeAssistant.Infrastructure.EntityConfigurations;
using IoTHomeAssistant.Domain.Repositories;
using IoTHomeAssistant.Infrastructure.Repositories;
using IoTHomeAssistant.Domain.Services;
using IoTHomeAssistant.Web.Hubs;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using IoTHomeAssistant.Domain.Options;

namespace IoTHomeAssistant.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<IoTDbContext>(options =>
                options.UseSqlite(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();
            //services.AddMvc(options =>
            //{
            //   options.Filters.Add(new AuthorizeFilter());
            //});
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<MqttOption>(Configuration.GetSection("MqttConfiguration"));

            services.AddTransient<ConnectionHubManager>();
            services.AddTransient<NotificationHub>();
            services.AddTransient<IDeviceMqttTopicRepository, DeviceMqttTopicRepository>();
            services.AddTransient<IWidgetRepository, WidgetRepository>();
            services.AddTransient<IPluginRepository, PluginRepository>();
            services.AddTransient<IDeviceRepository, DeviceRepository>();
            services.AddTransient<IJobTaskRepository, JobTaskRepository>();
            services.AddTransient<IWidgetService, WidgetService>();
            services.AddTransient<IDeviceService, DeviceService>();
            services.AddTransient<IPluginService, PluginService>();
            services.AddTransient<IJobTaskService, JobTaskService>();


            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddControllersWithViews();
            services.AddSignalR();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IoT Home Assistant", Version = "alpha" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();

                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IoT Home Assistant alpha"));
            }
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
                endpoints.MapHub<DeviceHub>("/devicehub");
            });
        }

        private static JsonSerializerSettings ConfigureNewtonsoftJson(JsonSerializerSettings settings)
        {
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            settings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
            return settings;
        }
    }
}