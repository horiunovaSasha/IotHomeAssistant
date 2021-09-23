using IoTHomeAssistant.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IoTHomeAssistant.Infrastructure.EntityConfigurations
{
    public class EventCollectionConfiguration : IEntityTypeConfiguration<EventCollection>
    {
        public void Configure(EntityTypeBuilder<EventCollection> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.DeviceType).IsRequired();
            builder.Property(x => x.PluginId);

            builder.HasOne(x => x.Plugin);
            builder.HasMany(x => x.Events);
        }
    }
}
