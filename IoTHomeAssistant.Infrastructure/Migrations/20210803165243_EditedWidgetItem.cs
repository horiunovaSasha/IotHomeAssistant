using Microsoft.EntityFrameworkCore.Migrations;

namespace IoTHomeAssistant.Infrastructure.Migrations
{
    public partial class EditedWidgetItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeviceId",
                table: "WidgetItem",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WidgetItem_DeviceId",
                table: "WidgetItem",
                column: "DeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_WidgetItem_Device_DeviceId",
                table: "WidgetItem",
                column: "DeviceId",
                principalTable: "Device",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WidgetItem_Device_DeviceId",
                table: "WidgetItem");

            migrationBuilder.DropIndex(
                name: "IX_WidgetItem_DeviceId",
                table: "WidgetItem");

            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "WidgetItem");
        }
    }
}
