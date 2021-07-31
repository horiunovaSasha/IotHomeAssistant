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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "WidgetItem");
        }
    }
}
