using IoTHomeAssistant.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IoTHomeAssistant.Infrastructure.EntityConfigurations
{
    public class JobTaskConfiguration : IEntityTypeConfiguration<JobTask>
    {
        public void Configure(EntityTypeBuilder<JobTask> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Title).HasMaxLength(50).IsRequired();

            builder.HasMany(x => x.Executions).WithOne().HasForeignKey(x => x.JobTaskId);
            builder.HasMany(x => x.Conditions).WithOne().HasForeignKey(x => x.JobTaskId);
        }
    }
}
