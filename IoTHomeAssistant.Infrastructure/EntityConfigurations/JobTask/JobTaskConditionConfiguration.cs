using IoTHomeAssistant.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IoTHomeAssistant.Infrastructure.EntityConfigurations
{
    public class JobTaskConditionConfiguration : IEntityTypeConfiguration<JobTaskCondition>
    {
        public void Configure(EntityTypeBuilder<JobTaskCondition> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.JobTaskId).IsRequired();
            builder.Property(x => x.Type).IsRequired();
            builder.Property(x => x.Operation);
            builder.Property(x => x.SensorId);
            builder.Property(x => x.DateTime);
            builder.Property(x => x.TriggeredEventId);
            builder.Property(x => x.TriggeredTaskId);
            builder.Property(x => x.Value);

            builder.HasOne(x => x.JobTask);
            builder.HasOne(x => x.TriggeredTask);
        }
    }
}
