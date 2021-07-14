using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IoTHomeAssistant.Infrastructure.EntityConfigurations
{
    public class PluginDeviceConfigurationConfiguration : IEntityTypeConfiguration<Domain.Entities.PluginDeviceConfiguration>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.PluginDeviceConfiguration> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.PluginDeviceId).IsRequired();
            builder.Property(x => x.PluginConfigurationId).IsRequired();
            builder.Property(x => x.Value).IsRequired();

            builder.HasOne(x => x.PluginDevice);
            builder.HasOne(x => x.PluginConfiguration);
        }
    }
}
