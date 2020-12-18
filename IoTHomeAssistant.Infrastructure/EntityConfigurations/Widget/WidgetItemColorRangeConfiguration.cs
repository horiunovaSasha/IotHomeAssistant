using IoTHomeAssistant.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IoTHomeAssistant.Infrastructure.EntityConfigurations
{
    public class WidgetItemColorRangeConfiguration : IEntityTypeConfiguration<WidgetItemColorRange>
    {
        public void Configure(EntityTypeBuilder<WidgetItemColorRange> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.ValueFrom).IsRequired();
            builder.Property(x => x.ValueTo).IsRequired();

            builder.HasOne(x => x.WidgetItem);
            builder.HasOne(x => x.Color);
        }
    }
}
