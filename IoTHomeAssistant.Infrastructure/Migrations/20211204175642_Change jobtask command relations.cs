using Microsoft.EntityFrameworkCore.Migrations;

namespace IoTHomeAssistant.Infrastructure.Migrations
{
    public partial class Changejobtaskcommandrelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobTaskExecution_Command_CommandId",
                table: "JobTaskExecution");

            migrationBuilder.RenameColumn(
                name: "CommandId",
                table: "JobTaskExecution",
                newName: "DeviceCommandId");

            migrationBuilder.RenameIndex(
                name: "IX_JobTaskExecution_CommandId",
                table: "JobTaskExecution",
                newName: "IX_JobTaskExecution_DeviceCommandId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobTaskExecution_DeviceCommand_DeviceCommandId",
                table: "JobTaskExecution",
                column: "DeviceCommandId",
                principalTable: "DeviceCommand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobTaskExecution_DeviceCommand_DeviceCommandId",
                table: "JobTaskExecution");

            migrationBuilder.RenameColumn(
                name: "DeviceCommandId",
                table: "JobTaskExecution",
                newName: "CommandId");

            migrationBuilder.RenameIndex(
                name: "IX_JobTaskExecution_DeviceCommandId",
                table: "JobTaskExecution",
                newName: "IX_JobTaskExecution_CommandId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobTaskExecution_Command_CommandId",
                table: "JobTaskExecution",
                column: "CommandId",
                principalTable: "Command",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
