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
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ColorConfiguration());
            builder.ApplyConfiguration(new IconConfiguration());
            builder.ApplyConfiguration(new AreaConfiguration());
            builder.ApplyConfiguration(new DeviceConfiguration());
            builder.ApplyConfiguration(new DeviceGroupConfiguration());
            builder.ApplyConfiguration(new MqttBrokerConfiguration());
            builder.ApplyConfiguration(new WidgetConfiguration());
            builder.ApplyConfiguration(new WidgetItemConfiguration());
            builder.ApplyConfiguration(new WidgetItemColorRangeConfiguration());
            builder.ApplyConfiguration(new PluginConfiguration());
            builder.ApplyConfiguration(new PluginDeviceConfiguration());
            builder.ApplyConfiguration(new PluginDeviceConfigurationConfiguration());
            builder.ApplyConfiguration(new JobTaskConfiguration());
            builder.ApplyConfiguration(new JobTaskConditionConfiguration());
            builder.ApplyConfiguration(new JobTaskExecutionConfiguration());
            builder.ApplyConfiguration(new JobConfiguration());
            builder.ApplyConfiguration(new EventConfiguration());
            builder.ApplyConfiguration(new EventCollectionConfiguration());
            builder.ApplyConfiguration(new CommandConfiguration());
            builder.ApplyConfiguration(new CommandCollectionConfiguration());
        }
    }
}
