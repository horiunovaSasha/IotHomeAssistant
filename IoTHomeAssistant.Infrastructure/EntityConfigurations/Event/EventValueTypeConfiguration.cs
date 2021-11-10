using IoTHomeAssistant.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IoTHomeAssistant.Infrastructure.EntityConfigurations
{
    public class EventValueTypeConfiguration : IEntityTypeConfiguration<EventValueType>
    {
        public void Configure(EntityTypeBuilder<EventValueType> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.EventId).IsRequired();
            builder.Property(x => x.Type).IsRequired();
            builder.Property(x => x.Min);
            builder.Property(x => x.Max);

            builder.HasOne(x => x.Event).WithOne(x => x.ValueType);
            builder.HasMany(x => x.Items).WithOne(x => x.EventValueType).HasForeignKey(x => x.EventValueTypeId);
        }
    }
}
