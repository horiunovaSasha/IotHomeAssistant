using IoTHomeAssistant.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IoTHomeAssistant.Infrastructure.EntityConfigurations
{
    public class DeviceMqttTopicConfiguration : IEntityTypeConfiguration<DeviceMqttTopic>
    {
        public void Configure(EntityTypeBuilder<DeviceMqttTopic> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Title).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Topic).HasMaxLength(255).IsRequired();
            builder.Property(x => x.TopicType).IsRequired();

            builder.HasOne(x => x.Device);
            builder.HasOne(x => x.MqttBroker);
        }
    }
}
