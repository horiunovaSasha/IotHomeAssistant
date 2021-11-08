using IoTHomeAssistant.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IoTHomeAssistant.Infrastructure.EntityConfigurations.Device
{
    public class DeviceEventConfiguration : IEntityTypeConfiguration<DeviceEvent>
    {
        public void Configure(EntityTypeBuilder<DeviceEvent> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.DeviceId).IsRequired();
            builder.Property(x => x.EventId).IsRequired();

            builder.HasOne(x => x.Device); 
            builder.HasOne(x => x.Event); 
        }
    }
}