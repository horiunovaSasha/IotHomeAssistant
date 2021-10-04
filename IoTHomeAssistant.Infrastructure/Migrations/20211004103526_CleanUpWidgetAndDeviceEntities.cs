using Microsoft.EntityFrameworkCore.Migrations;

namespace IoTHomeAssistant.Infrastructure.Migrations
{
    public partial class CleanUpWidgetAndDeviceEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WidgetItem_Color_IconColorId",
                table: "WidgetItem");

            migrationBuilder.DropForeignKey(
                name: "FK_WidgetItem_Widget_WidgetId",
                table: "WidgetItem");

            migrationBuilder.DropTable(
                name: "DeviceDeviceGroup");

            migrationBuilder.DropTable(
                name: "Widget");

            migrationBuilder.DropTable(
                name: "WidgetItemColorRange");

            migrationBuilder.DropTable(
                name: "DeviceGroup");

            migrationBuilder.DropIndex(
                name: "IX_WidgetItem_IconColorId",
                table: "WidgetItem");

            migrationBuilder.DropColumn(
                name: "IconColorId",
                table: "WidgetItem");

            migrationBuilder.RenameColumn(
                name: "WidgetId",
                table: "WidgetItem",
                newName: "AreaId");

            migrationBuilder.RenameIndex(
                name: "IX_WidgetItem_WidgetId",
                table: "WidgetItem",
                newName: "IX_WidgetItem_AreaId");

            migrationBuilder.AddColumn<byte>(
                name: "Order",
                table: "WidgetItem",
                type: "INTEGER",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddForeignKey(
                name: "FK_WidgetItem_Area_AreaId",
                table: "WidgetItem",
                column: "AreaId",
                principalTable: "Area",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WidgetItem_Area_AreaId",
                table: "WidgetItem");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "WidgetItem");

            migrationBuilder.RenameColumn(
                name: "AreaId",
                table: "WidgetItem",
                newName: "WidgetId");

            migrationBuilder.RenameIndex(
                name: "IX_WidgetItem_AreaId",
                table: "WidgetItem",
                newName: "IX_WidgetItem_WidgetId");

            migrationBuilder.AddColumn<int>(
                name: "IconColorId",
                table: "WidgetItem",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DeviceGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AreaId = table.Column<int>(type: "INTEGER", nullable: true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceGroup_Area_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Area",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Widget",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AreaId = table.Column<int>(type: "INTEGER", nullable: true),
                    Order = table.Column<byte>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Widget", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Widget_Area_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Area",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WidgetItemColorRange",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ColorId = table.Column<int>(type: "INTEGER", nullable: true),
                    ValueFrom = table.Column<float>(type: "REAL", nullable: false),
                    ValueTo = table.Column<float>(type: "REAL", nullable: false),
                    WidgetItemId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WidgetItemColorRange", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WidgetItemColorRange_Color_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Color",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WidgetItemColorRange_WidgetItem_WidgetItemId",
                        column: x => x.WidgetItemId,
                        principalTable: "WidgetItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeviceDeviceGroup",
                columns: table => new
                {
                    DevicesId = table.Column<int>(type: "INTEGER", nullable: false),
                    GroupsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceDeviceGroup", x => new { x.DevicesId, x.GroupsId });
                    table.ForeignKey(
                        name: "FK_DeviceDeviceGroup_Device_DevicesId",
                        column: x => x.DevicesId,
                        principalTable: "Device",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceDeviceGroup_DeviceGroup_GroupsId",
                        column: x => x.GroupsId,
                        principalTable: "DeviceGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WidgetItem_IconColorId",
                table: "WidgetItem",
                column: "IconColorId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceDeviceGroup_GroupsId",
                table: "DeviceDeviceGroup",
                column: "GroupsId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceGroup_AreaId",
                table: "DeviceGroup",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Widget_AreaId",
                table: "Widget",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_WidgetItemColorRange_ColorId",
                table: "WidgetItemColorRange",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_WidgetItemColorRange_WidgetItemId",
                table: "WidgetItemColorRange",
                column: "WidgetItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_WidgetItem_Color_IconColorId",
                table: "WidgetItem",
                column: "IconColorId",
                principalTable: "Color",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WidgetItem_Widget_WidgetId",
                table: "WidgetItem",
                column: "WidgetId",
                principalTable: "Widget",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
