using Microsoft.EntityFrameworkCore.Migrations;

namespace IoTHomeAssistant.Infrastructure.Migrations
{
    public partial class ChangePluginEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "PluginDeviceConfiguration");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "PluginDeviceConfiguration");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "PluginDeviceConfiguration");

            migrationBuilder.AddColumn<int>(
                name: "PluginConfigurationId",
                table: "PluginDeviceConfiguration",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PluginConfiguration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PluginId = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Key = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PluginConfiguration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PluginConfiguration_Plugin_PluginId",
                        column: x => x.PluginId,
                        principalTable: "Plugin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PluginDeviceConfiguration_PluginConfigurationId",
                table: "PluginDeviceConfiguration",
                column: "PluginConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_PluginConfiguration_PluginId",
                table: "PluginConfiguration",
                column: "PluginId");

            migrationBuilder.AddForeignKey(
                name: "FK_PluginDeviceConfiguration_PluginConfiguration_PluginConfigurationId",
                table: "PluginDeviceConfiguration",
                column: "PluginConfigurationId",
                principalTable: "PluginConfiguration",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PluginDeviceConfiguration_PluginConfiguration_PluginConfigurationId",
                table: "PluginDeviceConfiguration");

            migrationBuilder.DropTable(
                name: "PluginConfiguration");

            migrationBuilder.DropIndex(
                name: "IX_PluginDeviceConfiguration_PluginConfigurationId",
                table: "PluginDeviceConfiguration");

            migrationBuilder.DropColumn(
                name: "PluginConfigurationId",
                table: "PluginDeviceConfiguration");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "PluginDeviceConfiguration",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "PluginDeviceConfiguration",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "PluginDeviceConfiguration",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
