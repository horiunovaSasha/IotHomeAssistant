using IoTHomeAssistant.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IoTHomeAssistant.Infrastructure.EntityConfigurations
{
    public class WidgetConfiguration : IEntityTypeConfiguration<Widget>
    {
        public void Configure(EntityTypeBuilder<Widget> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Title).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Order).IsRequired();

            builder.HasOne(x => x.Area);
            builder.HasMany(x => x.Items);
        }
    }
}
