﻿using IoTHomeAssistant.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IoTHomeAssistant.Infrastructure.EntityConfigurations.Device
{
    public class DeviceEventCollectionConfiguration : IEntityTypeConfiguration<DeviceEventCollection>
    {
        public void Configure(EntityTypeBuilder<DeviceEventCollection> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.DeviceId).IsRequired();
            builder.Property(x => x.EventCollectionId).IsRequired();

            builder.HasOne(x => x.Device);
            builder.HasOne(x => x.EventCollection);
        }
    }
}