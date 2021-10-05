﻿// <auto-generated />
using System;
using IoTHomeAssistant.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IoTHomeAssistant.Infrastructure.Migrations
{
    [DbContext(typeof(IoTDbContext))]
    [Migration("20211005170931_AddWidgetItemForeginKeys")]
    partial class AddWidgetItemForeginKeys
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.8");

            modelBuilder.Entity("IoTHomeAssistant.Domain.Entities.Area", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Area");
                });

            modelBuilder.Entity("IoTHomeAssistant.Domain.Entities.Color", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Color");
                });

            modelBuilder.Entity("IoTHomeAssistant.Domain.Entities.Command.Command", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CommandCollectionId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CommandCollectionId");

                    b.ToTable("Command");
                });

            modelBuilder.Entity("IoTHomeAssistant.Domain.Entities.Command.CommandCollection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<byte>("DeviceType")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PluginId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PluginId");

                    b.ToTable("CommandCollection");
                });

            modelBuilder.Entity("IoTHomeAssistant.Domain.Entities.Device", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AreaId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CommandCollectionId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<byte>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AreaId");

                    b.HasIndex("CommandCollectionId");

                    b.ToTable("Device");
                });

            modelBuilder.Entity("IoTHomeAssistant.Domain.Entities.DeviceEventCollection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("DeviceId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EventCollectionId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId")
                        .IsUnique();

                    b.HasIndex("EventCollectionId");

                    b.ToTable("DeviceEventCollection");
                });

            modelBuilder.Entity("IoTHomeAssistant.Domain.Entities.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("EventCollectionId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("HasValue")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EventCollectionId");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("IoTHomeAssistant.Domain.Entities.EventCollection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<byte>("DeviceType")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PluginId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PluginId");

                    b.ToTable("EventCollection");
                });

            modelBuilder.Entity("IoTHomeAssistant.Domain.Entities.Icon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Icon");
                });

            modelBuilder.Entity("IoTHomeAssistant.Domain.Entities.Job", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ErrorMessage")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("FinishTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("JobTaskId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Succeed")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("JobTaskId");

                    b.ToTable("Job");
                });

            modelBuilder.Entity("IoTHomeAssistant.Domain.Entities.JobTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("JobTask");
                });

            modelBuilder.Entity("IoTHomeAssistant.Domain.Entities.JobTaskCondition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("JobTaskId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Operation")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("SensorId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("TriggeredEventId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("TriggeredTaskId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<float?>("Value")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("JobTaskId");

                    b.HasIndex("TriggeredTaskId");

                    b.ToTable("JobTaskCondition");
                });

            modelBuilder.Entity("IoTHomeAssistant.Domain.Entities.JobTaskExecution", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CommandId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("DeviceId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("JobTaskId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Order")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("TriggeredTaskId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.Property<int?>("WaitSeconds")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("JobTaskId");

                    b.HasIndex("TriggeredTaskId");

                    b.ToTable("JobTaskExecution");
                });

            modelBuilder.Entity("IoTHomeAssistant.Domain.Entities.MqttBroker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<bool>("UseCredentials")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("MqttBroker");
                });

            modelBuilder.Entity("IoTHomeAssistant.Domain.Entities.Plugin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<byte>("DeviceType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DockerImageId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DockerImageSource")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Plugin");
                });

            modelBuilder.Entity("IoTHomeAssistant.Domain.Entities.PluginConfiguration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Key")
                        .HasColumnType("TEXT");

                    b.Property<int>("PluginId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.Property<byte>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PluginId");

                    b.ToTable("PluginConfiguration");
                });

            modelBuilder.Entity("IoTHomeAssistant.Domain.Entities.PluginDevice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("DeviceId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ExtDeviceRef")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PluginId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId")
                        .IsUnique();

                    b.HasIndex("PluginId");

                    b.ToTable("PluginDevice");
                });

            modelBuilder.Entity("IoTHomeAssistant.Domain.Entities.PluginDeviceConfiguration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("PluginConfigurationId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PluginDeviceId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PluginConfigurationId");

                    b.HasIndex("PluginDeviceId");

                    b.ToTable("PluginDeviceConfiguration");
                });

            modelBuilder.Entity("IoTHomeAssistant.Domain.Entities.WidgetItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AreaId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("DeviceId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("EventId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("IconId")
                        .HasColumnType("INTEGER");

                    b.Property<byte>("Order")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SymbolAfter")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<byte>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AreaId");

                    b.HasIndex("DeviceId");

                    b.HasIndex("EventId");

                    b.HasIndex("IconId");

                    b.ToTable("WidgetItem");
                });

            modelBuilder.Entity("IoTHomeAssistant.Domain.Entities.Command.Command", b =>
                {
                    b.HasOne("IoTHomeAssistant.Domain.Entities.Command.CommandCollection", null)
                        .WithMany("Commands")
                        .HasForeignKey("CommandCollectionId");
                });

            modelBuilder.Entity("IoTHomeAssistant.Domain.Entities.Command.CommandCollection", b =>
                {
                    b.HasOne("IoTHomeAssistant.Domain.Entities.Plugin", "Plugin")
                        .WithMany()
                        .HasForeignKey("PluginId");

                    b.Navigation("Plugin");
                });

            modelBuilder.Entity("IoTHomeAssistant.Domain.Entities.Device", b =>
                {
                    b.HasOne("IoTHomeAssistant.Domain.Entities.Area", null)
                        .WithMany("Devices")
                        .HasForeignKey("AreaId");

                    b.HasOne("IoTHomeAssistant.Domain.Entities.Command.CommandCollection", "CommandCollection")
                        .WithMany()
                        .HasForeignKey("CommandCollectionId");

                    b.Navigation("CommandCollection");
                });

            modelBuilder.Entity("IoTHomeAssistant.Domain.Entities.DeviceEventCollection", b =>
                {
                    b.HasOne("IoTHomeAssistant.Domain.Entities.Device", "Device")
                        .WithOne("DeviceEvents")
                        .HasForeignKey("IoTHomeAssistant.Domain.Entities.DeviceEventCollection", "DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IoTHomeAssistant.Domain.Entities.EventCollection", "EventCollection")
                        .WithMany()
                        .HasForeignKey("EventCollectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");

                    b.Navigation("EventCollection");
                });

            modelBuilder.Entity("IoTHomeAssistant.Domain.Entities.Event", b =>
                {
                    b.HasOne("IoTHomeAssistant.Domain.Entities.EventCollection", null)
                        .WithMany("Events")
                        .HasForeignKey("EventCollectionId");
                });

            modelBuilder.Entity("IoTHomeAssistant.Domain.Entities.EventCollection", b =>
                {
                    b.HasOne("IoTHomeAssistant.Domain.Entities.Plugin", "Plugin")
                        .WithMany()
                        .HasForeignKey("PluginId");

                    b.Navigation("Plugin");
                });

            modelBuilder.Entity("IoTHomeAssistant.Domain.Entities.Job", b =>
                {
                    b.HasOne("IoTHomeAssistant.Domain.Entities.JobTask", "JobTask")
                        .WithMany()
                        .HasForeignKey("JobTaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("JobTask");
                });

            modelBuilder.Entity("IoTHomeAssistant.Domain.Entities.JobTaskCondition", b =>
                {
                    b.HasOne("IoTHomeAssistant.Domain.Entities.JobTask", "JobTask")
                        .WithMany("Conditions")
                        .HasForeignKey("JobTaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IoTHomeAssistant.Domain.Entities.JobTask", "TriggeredTask")
                        .WithMany()
                        .HasForeignKey("TriggeredTaskId");

                    b.Navigation("JobTask");

                    b.Navigation("TriggeredTask");
                });

            modelBuilder.Entity("IoTHomeAssistant.Domain.Entities.JobTaskExecution", b =>
                {
                    b.HasOne("IoTHomeAssistant.Domain.Entities.JobTask", "JobTask")
                        .WithMany("Executions")
                        .HasForeignKey("JobTaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IoTHomeAssistant.Domain.Entities.JobTask", "TriggeredTask")
                        .WithMany()
                        .HasForeignKey("TriggeredTaskId");

                    b.Navigation("JobTask");

                    b.Navigation("TriggeredTask");
                });

            modelBuilder.Entity("IoTHomeAssistant.Domain.Entities.PluginConfiguration", b =>
                {
                    b.HasOne("IoTHomeAssistant.Domain.Entities.Plugin", "Plugin")
                        .WithMany("Configurations")
                        .HasForeignKey("PluginId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Plugin");
                });

            modelBuilder.Entity("IoTHomeAssistant.Domain.Entities.PluginDevice", b =>
                {
                    b.HasOne("IoTHomeAssistant.Domain.Entities.Device", "Device")
                        .WithOne("PluginDevice")
                        .HasForeignKey("IoTHomeAssistant.Domain.Entities.PluginDevice", "DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IoTHomeAssistant.Domain.Entities.Plugin", "Plugin")
                        .WithMany()
                        .HasForeignKey("PluginId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");

                    b.Navigation("Plugin");
                });

            modelBuilder.Entity("IoTHomeAssistant.Domain.Entities.PluginDeviceConfiguration", b =>
                {
                    b.HasOne("IoTHomeAssistant.Domain.Entities.PluginConfiguration", "PluginConfiguration")
                        .WithMany()
                        .HasForeignKey("PluginConfigurationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IoTHomeAssistant.Domain.Entities.PluginDevice", "PluginDevice")
                        .WithMany("Configurations")
                        .HasForeignKey("PluginDeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PluginConfiguration");

                    b.Navigation("PluginDevice");
                });

            modelBuilder.Entity("IoTHomeAssistant.Domain.Entities.WidgetItem", b =>
                {
                    b.HasOne("IoTHomeAssistant.Domain.Entities.Area", "Area")
                        .WithMany("Widgets")
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IoTHomeAssistant.Domain.Entities.Device", "Device")
                        .WithMany()
                        .HasForeignKey("DeviceId");

                    b.HasOne("IoTHomeAssistant.Domain.Entities.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId");

                    b.HasOne("IoTHomeAssistant.Domain.Entities.Icon", "Icon")
                        .WithMany()
                        .HasForeignKey("IconId");

                    b.Navigation("Area");

                    b.Navigation("Device");

                    b.Navigation("Event");

                    b.Navigation("Icon");
                });

            modelBuilder.Entity("IoTHomeAssistant.Domain.Entities.Area", b =>
                {
                    b.Navigation("Devices");

                    b.Navigation("Widgets");
                });

            modelBuilder.Entity("IoTHomeAssistant.Domain.Entities.Command.CommandCollection", b =>
                {
                    b.Navigation("Commands");
                });

            modelBuilder.Entity("IoTHomeAssistant.Domain.Entities.Device", b =>
                {
                    b.Navigation("DeviceEvents");

                    b.Navigation("PluginDevice");
                });

            modelBuilder.Entity("IoTHomeAssistant.Domain.Entities.EventCollection", b =>
                {
                    b.Navigation("Events");
                });

            modelBuilder.Entity("IoTHomeAssistant.Domain.Entities.JobTask", b =>
                {
                    b.Navigation("Conditions");

                    b.Navigation("Executions");
                });

            modelBuilder.Entity("IoTHomeAssistant.Domain.Entities.Plugin", b =>
                {
                    b.Navigation("Configurations");
                });

            modelBuilder.Entity("IoTHomeAssistant.Domain.Entities.PluginDevice", b =>
                {
                    b.Navigation("Configurations");
                });
#pragma warning restore 612, 618
        }
    }
}
