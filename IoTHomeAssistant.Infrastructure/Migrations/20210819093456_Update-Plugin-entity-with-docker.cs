using Microsoft.EntityFrameworkCore.Migrations;

namespace IoTHomeAssistant.Infrastructure.Migrations
{
    public partial class UpdatePluginentitywithdocker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DockerConfiguration",
                table: "Plugin",
                newName: "DockerImageId");

            migrationBuilder.AlterColumn<string>(
                name: "DockerImageSource",
                table: "Plugin",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DockerImageId",
                table: "Plugin",
                newName: "DockerConfiguration");

            migrationBuilder.AlterColumn<string>(
                name: "DockerImageSource",
                table: "Plugin",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }
    }
}
