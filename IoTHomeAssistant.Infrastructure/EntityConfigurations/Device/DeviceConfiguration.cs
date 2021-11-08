using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IoTHomeAssistant.Infrastructure.EntityConfigurations
{
    public class DeviceConfiguration : IEntityTypeConfiguration<Domain.Entities.Device>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Device> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Title).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Type).IsRequired();

            builder.HasOne(x => x.PluginDevice);
            builder.HasMany(x => x.DeviceEvents).WithOne(x => x.Device).HasForeignKey(x => x.DeviceId);
        }
    }
}
