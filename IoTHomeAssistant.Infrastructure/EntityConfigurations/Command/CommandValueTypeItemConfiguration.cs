using IoTHomeAssistant.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IoTHomeAssistant.Infrastructure.EntityConfigurations
{
    public class CommandValueTypeItemConfiguration : IEntityTypeConfiguration<CommandValueTypeItem>
    {
        public void Configure(EntityTypeBuilder<CommandValueTypeItem> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.CommandValueTypeId).IsRequired();
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.Value).IsRequired();

            builder.HasOne(x => x.CommandValueType).WithMany(x => x.Items).HasForeignKey(x => x.CommandValueTypeId);
        }
    }
}
