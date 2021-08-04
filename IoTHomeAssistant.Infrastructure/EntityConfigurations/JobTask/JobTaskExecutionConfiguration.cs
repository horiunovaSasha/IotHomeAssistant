using IoTHomeAssistant.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IoTHomeAssistant.Infrastructure.EntityConfigurations
{
    public class JobTaskExecutionConfiguration : IEntityTypeConfiguration<JobTaskExecution>
    {
        public void Configure(EntityTypeBuilder<JobTaskExecution> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.JobTaskId).IsRequired();
            builder.Property(x => x.Type).IsRequired();
            builder.Property(x => x.Order).IsRequired();
            builder.Property(x => x.CommandId);
            builder.Property(x => x.DeviceId);
            builder.Property(x => x.WaitSeconds);
            builder.Property(x => x.TriggeredTaskId);
            builder.Property(x => x.Value);

            builder.HasOne(x => x.JobTask);
            builder.HasOne(x => x.TriggeredTask);
        }
    }
}
