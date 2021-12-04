using IoTHomeAssistant.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IoTHomeAssistant.Infrastructure.EntityConfigurations
{
    public class WidgetItemConfiguration : IEntityTypeConfiguration<WidgetItem>
    {
        public void Configure(EntityTypeBuilder<WidgetItem> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Title).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Type).IsRequired();
            builder.Property(x => x.Order).IsRequired();
            builder.Property(x => x.SymbolAfter);

            builder.Property(x => x.DeviceId);
            builder.Property(x => x.IconId);
            builder.Property(x => x.AreaId);
            builder.Property(x => x.DeviceEventId);
            builder.Property(x => x.JobTaskId);
            builder.Property(x => x.Latitude);
            builder.Property(x => x.Longitude);

            builder.HasOne(x => x.Area);
            builder.HasOne(x => x.Icon);
            builder.HasOne(x => x.Device);
            builder.HasOne(x => x.DeviceEvent);

        }
    }
}
