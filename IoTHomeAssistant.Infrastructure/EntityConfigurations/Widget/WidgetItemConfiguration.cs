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

            builder.HasOne(x => x.Widget);
            builder.HasOne(x => x.Icon);
            builder.HasOne(x => x.IconColor);
            builder.HasOne(x => x.DeviceTopic);

            builder.HasMany(x => x.ColorRange);
        }
    }
}
