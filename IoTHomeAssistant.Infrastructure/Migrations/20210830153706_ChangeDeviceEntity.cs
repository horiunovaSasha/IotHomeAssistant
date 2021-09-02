using Microsoft.EntityFrameworkCore.Migrations;

namespace IoTHomeAssistant.Infrastructure.Migrations
{
    public partial class ChangeDeviceEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Device_DeviceVendor_VendorId",
                table: "Device");

            migrationBuilder.DropColumn(
               name: "VendorId",
               table: "Device");

            migrationBuilder.DropTable(
                name: "DeviceVendor");

            migrationBuilder.DropIndex(
                name: "IX_PluginDevice_DeviceId",
                table: "PluginDevice");

            migrationBuilder.DropIndex(
                name: "IX_Device_VendorId",
                table: "Device");

            migrationBuilder.DropColumn(
                name: "IPAddress",
                table: "Device");           

            migrationBuilder.CreateIndex(
                name: "IX_PluginDevice_DeviceId",
                table: "PluginDevice",
                column: "DeviceId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PluginDevice_DeviceId",
                table: "PluginDevice");

            migrationBuilder.AddColumn<string>(
                name: "IPAddress",
                table: "Device",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VendorId",
                table: "Device",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DeviceVendor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceVendor", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PluginDevice_DeviceId",
                table: "PluginDevice",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Device_VendorId",
                table: "Device",
                column: "VendorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Device_DeviceVendor_VendorId",
                table: "Device",
                column: "VendorId",
                principalTable: "DeviceVendor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
