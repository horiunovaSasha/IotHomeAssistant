using Microsoft.EntityFrameworkCore.Migrations;

namespace IoTHomeAssistant.Infrastructure.Migrations
{
    public partial class AddedDeviceEventColelction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Device_EventCollection_EventCollectionId",
                table: "Device");

            migrationBuilder.DropIndex(
                name: "IX_Device_EventCollectionId",
                table: "Device");

            migrationBuilder.DropColumn(
                name: "EventCollectionId",
                table: "Device");

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
                name: "IX_DeviceEventCollection_DeviceId",
                table: "DeviceEventCollection",
                column: "DeviceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceEventCollection_EventCollectionId",
                table: "DeviceEventCollection",
                column: "EventCollectionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceEventCollection");

            migrationBuilder.AddColumn<int>(
                name: "EventCollectionId",
                table: "Device",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Device_EventCollectionId",
                table: "Device",
                column: "EventCollectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Device_EventCollection_EventCollectionId",
                table: "Device",
                column: "EventCollectionId",
                principalTable: "EventCollection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
