using Microsoft.EntityFrameworkCore.Migrations;

namespace IoTHomeAssistant.Infrastructure.Migrations
{
    public partial class RemovedDeviceMqttTopicsAndAddedCommandsAndEnventsInstead : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WidgetItem_DeviceMqttTopic_DeviceTopicId",
                table: "WidgetItem");

            migrationBuilder.DropTable(
                name: "DeviceMqttTopic");

            migrationBuilder.DropIndex(
                name: "IX_WidgetItem_DeviceTopicId",
                table: "WidgetItem");

            migrationBuilder.DropColumn(
                name: "DeviceTopicId",
                table: "WidgetItem");

            migrationBuilder.AddColumn<int>(
                name: "CommandCollectionId",
                table: "Device",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EventCollectionId",
                table: "Device",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CommandCollection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DeviceType = table.Column<byte>(type: "INTEGER", nullable: false),
                    PluginId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandCollection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommandCollection_Plugin_PluginId",
                        column: x => x.PluginId,
                        principalTable: "Plugin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EventCollection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DeviceType = table.Column<byte>(type: "INTEGER", nullable: false),
                    PluginId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventCollection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventCollection_Plugin_PluginId",
                        column: x => x.PluginId,
                        principalTable: "Plugin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Command",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Key = table.Column<string>(type: "TEXT", nullable: false),
                    CommandCollectionId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Command", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Command_CommandCollection_CommandCollectionId",
                        column: x => x.CommandCollectionId,
                        principalTable: "CommandCollection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Key = table.Column<string>(type: "TEXT", nullable: false),
                    EventCollectionId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Event_EventCollection_EventCollectionId",
                        column: x => x.EventCollectionId,
                        principalTable: "EventCollection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Device_CommandCollectionId",
                table: "Device",
                column: "CommandCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Device_EventCollectionId",
                table: "Device",
                column: "EventCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Command_CommandCollectionId",
                table: "Command",
                column: "CommandCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_CommandCollection_PluginId",
                table: "CommandCollection",
                column: "PluginId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_EventCollectionId",
                table: "Event",
                column: "EventCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_EventCollection_PluginId",
                table: "EventCollection",
                column: "PluginId");

            migrationBuilder.AddForeignKey(
                name: "FK_Device_CommandCollection_CommandCollectionId",
                table: "Device",
                column: "CommandCollectionId",
                principalTable: "CommandCollection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Device_EventCollection_EventCollectionId",
                table: "Device",
                column: "EventCollectionId",
                principalTable: "EventCollection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Device_CommandCollection_CommandCollectionId",
                table: "Device");

            migrationBuilder.DropForeignKey(
                name: "FK_Device_EventCollection_EventCollectionId",
                table: "Device");

            migrationBuilder.DropTable(
                name: "Command");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "CommandCollection");

            migrationBuilder.DropTable(
                name: "EventCollection");

            migrationBuilder.DropIndex(
                name: "IX_Device_CommandCollectionId",
                table: "Device");

            migrationBuilder.DropIndex(
                name: "IX_Device_EventCollectionId",
                table: "Device");

            migrationBuilder.DropColumn(
                name: "CommandCollectionId",
                table: "Device");

            migrationBuilder.DropColumn(
                name: "EventCollectionId",
                table: "Device");

            migrationBuilder.AddColumn<int>(
                name: "DeviceTopicId",
                table: "WidgetItem",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DeviceMqttTopic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DeviceId = table.Column<int>(type: "INTEGER", nullable: true),
                    MqttBrokerId = table.Column<int>(type: "INTEGER", nullable: true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Topic = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    TopicType = table.Column<byte>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceMqttTopic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceMqttTopic_Device_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Device",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeviceMqttTopic_MqttBroker_MqttBrokerId",
                        column: x => x.MqttBrokerId,
                        principalTable: "MqttBroker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WidgetItem_DeviceTopicId",
                table: "WidgetItem",
                column: "DeviceTopicId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceMqttTopic_DeviceId",
                table: "DeviceMqttTopic",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceMqttTopic_MqttBrokerId",
                table: "DeviceMqttTopic",
                column: "MqttBrokerId");

            migrationBuilder.AddForeignKey(
                name: "FK_WidgetItem_DeviceMqttTopic_DeviceTopicId",
                table: "WidgetItem",
                column: "DeviceTopicId",
                principalTable: "DeviceMqttTopic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
