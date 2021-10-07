using Microsoft.EntityFrameworkCore.Migrations;

namespace IoTHomeAssistant.Infrastructure.Migrations
{
    public partial class ExtendIconAndClearArea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Device_Area_AreaId",
                table: "Device");

            migrationBuilder.DropIndex(
                name: "IX_Device_AreaId",
                table: "Device");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "Device");

            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "Icon",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Key",
                table: "Icon");

            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "Device",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Device_AreaId",
                table: "Device",
                column: "AreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Device_Area_AreaId",
                table: "Device",
                column: "AreaId",
                principalTable: "Area",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
