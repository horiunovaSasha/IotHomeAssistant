using IoTHomeAssistant.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IoTHomeAssistant.Infrastructure.EntityConfigurations.Device
{
    public class DeviceTypeEventConfiguration : IEntityTypeConfiguration<DeviceTypeEvent>
    {
        public void Configure(EntityTypeBuilder<DeviceTypeEvent> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Type).IsRequired();
            builder.Property(x => x.EventId).IsRequired();

            builder.HasOne(x => x.Event);
        }
    }
}