using IoTHomeAssistant.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IoTHomeAssistant.Infrastructure.EntityConfigurations
{
    public class CommandValueTypeConfiguration : IEntityTypeConfiguration<CommandValueType>
    {
        public void Configure(EntityTypeBuilder<CommandValueType> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.CommandId).IsRequired();
            builder.Property(x => x.Type).IsRequired();
            builder.Property(x => x.Min);
            builder.Property(x => x.Max);

            builder.HasOne(x => x.Command).WithOne(x => x.ValueType);
            builder.HasMany(x => x.Items).WithOne(x => x.CommandValueType).HasForeignKey(x => x.CommandValueTypeId);
        }
    }
}
