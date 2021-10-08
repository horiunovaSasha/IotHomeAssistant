using Microsoft.EntityFrameworkCore.Migrations;

namespace IoTHomeAssistant.Infrastructure.Migrations
{
    public partial class ExtendWidgetItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JobTaskId",
                table: "WidgetItem",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "WidgetItem",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "WidgetItem",
                type: "REAL",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobTaskId",
                table: "WidgetItem");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "WidgetItem");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "WidgetItem");
        }
    }
}
