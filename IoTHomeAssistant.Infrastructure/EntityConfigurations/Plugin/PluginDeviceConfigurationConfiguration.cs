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
            builder.Property(x => x.Key).IsRequired();
            builder.Property(x => x.Title).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Value).IsRequired();
            builder.Property(x => x.Description);

            builder.HasOne(x => x.PluginDevice);
        }
    }
}
