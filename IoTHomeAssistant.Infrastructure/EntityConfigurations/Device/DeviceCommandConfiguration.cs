using IoTHomeAssistant.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IoTHomeAssistant.Infrastructure.EntityConfigurations.Device
{
    public class DeviceCommandConfiguration : IEntityTypeConfiguration<DeviceCommand>
    {
        public void Configure(EntityTypeBuilder<DeviceCommand> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.DeviceId).IsRequired();
            builder.Property(x => x.CommandId).IsRequired();

            builder.HasOne(x => x.Device); 
            builder.HasOne(x => x.Command); 
        }
    }
}