using Microsoft.EntityFrameworkCore.Migrations;

namespace IoTHomeAssistant.Infrastructure.Migrations
{
    public partial class AddWidgetItemEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "WidgetItem",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SymbolAfter",
                table: "WidgetItem",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WidgetItem_EventId",
                table: "WidgetItem",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_WidgetItem_Event_EventId",
                table: "WidgetItem",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WidgetItem_Event_EventId",
                table: "WidgetItem");

            migrationBuilder.DropIndex(
                name: "IX_WidgetItem_EventId",
                table: "WidgetItem");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "WidgetItem");

            migrationBuilder.DropColumn(
                name: "SymbolAfter",
                table: "WidgetItem");
        }
    }
}
