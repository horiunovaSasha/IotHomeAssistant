using IoTHomeAssistant.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IoTHomeAssistant.Infrastructure.EntityConfigurations.Device
{
    public class DeviceTypeCommandConfiguration : IEntityTypeConfiguration<DeviceTypeCommand>
    {
        public void Configure(EntityTypeBuilder<DeviceTypeCommand> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Type).IsRequired();
            builder.Property(x => x.CommandId).IsRequired();

            builder.HasOne(x => x.Command);
        }
    }
}