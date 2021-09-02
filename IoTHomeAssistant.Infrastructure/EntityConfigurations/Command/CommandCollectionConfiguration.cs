using IoTHomeAssistant.Domain.Entities.Command;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IoTHomeAssistant.Infrastructure.EntityConfigurations
{
    public class CommandCollectionConfiguration : IEntityTypeConfiguration<CommandCollection>
    {
        public void Configure(EntityTypeBuilder<CommandCollection> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.DeviceType).IsRequired();
            builder.Property(x => x.PluginId);

            builder.HasOne(x => x.Plugin);
            builder.HasMany(x => x.Commands);

        }
    }
}
