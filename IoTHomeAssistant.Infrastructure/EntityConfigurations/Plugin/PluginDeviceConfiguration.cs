using IoTHomeAssistant.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IoTHomeAssistant.Infrastructure.EntityConfigurations
{
    public class PluginDeviceConfiguration : IEntityTypeConfiguration<PluginDevice>
    {
        public void Configure(EntityTypeBuilder<PluginDevice> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.DeviceId).IsRequired();
            builder.Property(x => x.ExtDeviceRef).IsRequired();
            builder.Property(x => x.ExtDeviceRef).IsRequired();

            builder.HasOne(x => x.Device);
            builder.HasOne(x => x.Plugin);
            builder.HasMany(x => x.Configurations);
        }
    }
}
