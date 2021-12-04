using Microsoft.EntityFrameworkCore.Migrations;

namespace IoTHomeAssistant.Infrastructure.Migrations
{
    public partial class Changewidgetanddeviceeventrelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WidgetItem_Event_EventId",
                table: "WidgetItem");

            migrationBuilder.DropIndex(
                name: "IX_WidgetItem_EventId",
                table: "WidgetItem");

            migrationBuilder.AddColumn<int>(
                name: "DeviceEventId",
                table: "WidgetItem",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WidgetItem_DeviceEventId",
                table: "WidgetItem",
                column: "DeviceEventId");

            migrationBuilder.CreateIndex(
                name: "IX_JobTaskExecution_CommandId",
                table: "JobTaskExecution",
                column: "CommandId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobTaskExecution_Command_CommandId",
                table: "JobTaskExecution",
                column: "CommandId",
                principalTable: "Command",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WidgetItem_DeviceEvent_DeviceEventId",
                table: "WidgetItem",
                column: "DeviceEventId",
                principalTable: "DeviceEvent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobTaskExecution_Command_CommandId",
                table: "JobTaskExecution");

            migrationBuilder.DropForeignKey(
                name: "FK_WidgetItem_DeviceEvent_DeviceEventId",
                table: "WidgetItem");

            migrationBuilder.DropIndex(
                name: "IX_WidgetItem_DeviceEventId",
                table: "WidgetItem");

            migrationBuilder.DropIndex(
                name: "IX_JobTaskExecution_CommandId",
                table: "JobTaskExecution");

            migrationBuilder.DropColumn(
                name: "DeviceEventId",
                table: "WidgetItem");

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
    }
}
