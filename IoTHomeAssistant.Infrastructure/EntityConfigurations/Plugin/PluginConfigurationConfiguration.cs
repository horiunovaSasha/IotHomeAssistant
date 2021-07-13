using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IoTHomeAssistant.Infrastructure.EntityConfigurations
{
    public class PluginConfigurationConfiguration : IEntityTypeConfiguration<Domain.Entities.PluginConfiguration>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.PluginConfiguration> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.PluginId).IsRequired();
            builder.Property(x => x.Key).IsRequired();
            builder.Property(x => x.Title).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Description);

            builder.HasOne(x => x.Plugin);
        }
    }
}
