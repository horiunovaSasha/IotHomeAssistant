using Microsoft.EntityFrameworkCore;

namespace IoTHomeAssistant.Infrastructure.EntityConfigurations
{
    public class IoTDbContext : DbContext
    {
        public IoTDbContext(DbContextOptions<IoTDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite(@"DataSource=..\Data\app.db;Cache=Shared");
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ColorConfiguration());
            builder.ApplyConfiguration(new IconConfiguration());
            builder.ApplyConfiguration(new AreaConfiguration());
            builder.ApplyConfiguration(new DeviceConfiguration());
            builder.ApplyConfiguration(new DeviceGroupConfiguration());
            builder.ApplyConfiguration(new DeviceMqttTopicConfiguration());
            builder.ApplyConfiguration(new DeviceVendorConfiguration());
            builder.ApplyConfiguration(new MqttBrokerConfiguration());
            builder.ApplyConfiguration(new WidgetConfiguration());
            builder.ApplyConfiguration(new WidgetItemConfiguration());
            builder.ApplyConfiguration(new WidgetItemColorRangeConfiguration());
        }
    }
}
