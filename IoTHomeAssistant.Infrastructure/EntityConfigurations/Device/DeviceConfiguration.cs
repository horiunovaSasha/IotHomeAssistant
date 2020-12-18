using IoTHomeAssistant.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IoTHomeAssistant.Infrastructure.EntityConfigurations
{
    public class DeviceConfiguration : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Title).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Type).IsRequired();

            builder.HasOne(x => x.Area);
            builder.HasOne(x => x.Vendor);
            builder.HasMany(x => x.Groups);
            builder.HasMany(x => x.Topics);
        }
    }
}
