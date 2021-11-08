using Microsoft.EntityFrameworkCore.Migrations;

namespace IoTHomeAssistant.Infrastructure.Migrations
{
    public partial class ChangeDeviceEventsAndCommands : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Command_CommandCollection_CommandCollectionId",
                table: "Command");

            migrationBuilder.DropForeignKey(
                name: "FK_Device_CommandCollection_CommandCollectionId",
                table: "Device");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_EventCollection_EventCollectionId",
                table: "Event");

            migrationBuilder.DropTable(
                name: "Color");

            migrationBuilder.DropTable(
                name: "CommandCollection");

            migrationBuilder.DropTable(
                name: "DeviceEventCollection");

            migrationBuilder.DropTable(
                name: "EventCollection");

            migrationBuilder.DropIndex(
                name: "IX_Event_EventCollectionId",
                table: "Event");

            migrationBuilder.DropIndex(
                name: "IX_Device_CommandCollectionId",
                table: "Device");

            migrationBuilder.DropIndex(
                name: "IX_Command_CommandCollectionId",
                table: "Command");

            migrationBuilder.DropColumn(
                name: "EventCollectionId",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "CommandCollectionId",
                table: "Device");

            migrationBuilder.DropColumn(
                name: "CommandCollectionId",
                table: "Command");

            migrationBuilder.CreateTable(
                name: "DeviceCommand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DeviceId = table.Column<int>(type: "INTEGER", nullable: false),
                    CommandId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceCommand", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceCommand_Command_CommandId",
                        column: x => x.CommandId,
                        principalTable: "Command",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceCommand_Device_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Device",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeviceEvent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DeviceId = table.Column<int>(type: "INTEGER", nullable: false),
                    EventId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceEvent_Device_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Device",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceEvent_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeviceTypeCommand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<byte>(type: "INTEGER", nullable: false),
                    CommandId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceTypeCommand", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceTypeCommand_Command_CommandId",
                        column: x => x.CommandId,
                        principalTable: "Command",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeviceTypeEvent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<byte>(type: "INTEGER", nullable: false),
                    EventId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceTypeEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceTypeEvent_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCommand_CommandId",
                table: "DeviceCommand",
                column: "CommandId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCommand_DeviceId",
                table: "DeviceCommand",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceEvent_DeviceId",
                table: "DeviceEvent",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceEvent_EventId",
                table: "DeviceEvent",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceTypeCommand_CommandId",
                table: "DeviceTypeCommand",
                column: "CommandId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceTypeEvent_EventId",
                table: "DeviceTypeEvent",
                column: "EventId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceCommand");

            migrationBuilder.DropTable(
                name: "DeviceEvent");

            migrationBuilder.DropTable(
                name: "DeviceTypeCommand");

            migrationBuilder.DropTable(
                name: "DeviceTypeEvent");

            migrationBuilder.AddColumn<int>(
                name: "EventCollectionId",
                table: "Event",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CommandCollectionId",
                table: "Device",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CommandCollectionId",
                table: "Command",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Color",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Value = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Color", x => x.Id);
                });

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
                name: "DeviceEventCollection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DeviceId = table.Column<int>(type: "INTEGER", nullable: false),
                    EventCollectionId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceEventCollection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceEventCollection_Device_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Device",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceEventCollection_EventCollection_EventCollectionId",
                        column: x => x.EventCollectionId,
                        principalTable: "EventCollection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Event_EventCollectionId",
                table: "Event",
                column: "EventCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Device_CommandCollectionId",
                table: "Device",
                column: "CommandCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Command_CommandCollectionId",
                table: "Command",
                column: "CommandCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_CommandCollection_PluginId",
                table: "CommandCollection",
                column: "PluginId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceEventCollection_DeviceId",
                table: "DeviceEventCollection",
                column: "DeviceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceEventCollection_EventCollectionId",
                table: "DeviceEventCollection",
                column: "EventCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_EventCollection_PluginId",
                table: "EventCollection",
                column: "PluginId");

            migrationBuilder.AddForeignKey(
                name: "FK_Command_CommandCollection_CommandCollectionId",
                table: "Command",
                column: "CommandCollectionId",
                principalTable: "CommandCollection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Device_CommandCollection_CommandCollectionId",
                table: "Device",
                column: "CommandCollectionId",
                principalTable: "CommandCollection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_EventCollection_EventCollectionId",
                table: "Event",
                column: "EventCollectionId",
                principalTable: "EventCollection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
