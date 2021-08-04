using IoTHomeAssistant.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IoTHomeAssistant.Infrastructure.EntityConfigurations
{
    public class JobConfiguration : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.JobTaskId).IsRequired();
            builder.Property(x => x.Succeed).IsRequired();
            builder.Property(x => x.StartTime).IsRequired();
            builder.Property(x => x.FinishTime);
            builder.Property(x => x.ErrorMessage);

            builder.HasOne(x => x.JobTask);
        }
    }
}
