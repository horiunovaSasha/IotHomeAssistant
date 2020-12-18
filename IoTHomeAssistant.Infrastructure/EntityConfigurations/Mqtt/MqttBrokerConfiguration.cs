using IoTHomeAssistant.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IoTHomeAssistant.Infrastructure.EntityConfigurations
{
    public class MqttBrokerConfiguration : IEntityTypeConfiguration<MqttBroker>
    {
        public void Configure(EntityTypeBuilder<MqttBroker> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Title).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Address).HasMaxLength(255).IsRequired();
            builder.Property(x => x.UseCredentials).IsRequired();
            builder.Property(x => x.Username).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Password).IsRequired().HasMaxLength(50);
        }
    }
}
