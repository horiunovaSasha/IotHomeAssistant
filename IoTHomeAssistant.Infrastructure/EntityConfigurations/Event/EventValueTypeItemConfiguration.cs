using IoTHomeAssistant.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IoTHomeAssistant.Infrastructure.EntityConfigurations
{
    public class EventValueTypeItemConfiguration : IEntityTypeConfiguration<EventValueTypeItem>
    {
        public void Configure(EntityTypeBuilder<EventValueTypeItem> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.EventValueTypeId).IsRequired();
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.Value).IsRequired();

            builder.HasOne(x => x.EventValueType).WithMany(x => x.Items).HasForeignKey(x => x.EventValueTypeId);
        }
    }
}
