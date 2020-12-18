using Microsoft.EntityFrameworkCore.Migrations;

namespace IoTHomeAssistant.Infrastructure.Migrations
{
    public partial class InitialIoT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Area",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Area", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Color",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Value = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Color", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceVendor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceVendor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Icon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Value = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Icon", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MqttBroker",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    UseCredentials = table.Column<bool>(type: "INTEGER", nullable: false),
                    Username = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MqttBroker", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    AreaId = table.Column<int>(type: "INTEGER", nullable: true)
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
                    Title = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Order = table.Column<byte>(type: "INTEGER", nullable: false),
                    AreaId = table.Column<int>(type: "INTEGER", nullable: true)
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
                name: "Device",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Type = table.Column<byte>(type: "INTEGER", nullable: false),
                    AreaId = table.Column<int>(type: "INTEGER", nullable: true),
                    VendorId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Device", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Device_Area_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Area",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Device_DeviceVendor_VendorId",
                        column: x => x.VendorId,
                        principalTable: "DeviceVendor",
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

            migrationBuilder.CreateTable(
                name: "DeviceMqttTopic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Topic = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    TopicType = table.Column<byte>(type: "INTEGER", nullable: false),
                    DeviceId = table.Column<int>(type: "INTEGER", nullable: true),
                    MqttBrokerId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceMqttTopic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceMqttTopic_Device_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Device",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeviceMqttTopic_MqttBroker_MqttBrokerId",
                        column: x => x.MqttBrokerId,
                        principalTable: "MqttBroker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WidgetItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<byte>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    WidgetId = table.Column<int>(type: "INTEGER", nullable: true),
                    IconId = table.Column<int>(type: "INTEGER", nullable: true),
                    IconColorId = table.Column<int>(type: "INTEGER", nullable: true),
                    DeviceTopicId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WidgetItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WidgetItem_Color_IconColorId",
                        column: x => x.IconColorId,
                        principalTable: "Color",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WidgetItem_DeviceMqttTopic_DeviceTopicId",
                        column: x => x.DeviceTopicId,
                        principalTable: "DeviceMqttTopic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WidgetItem_Icon_IconId",
                        column: x => x.IconId,
                        principalTable: "Icon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WidgetItem_Widget_WidgetId",
                        column: x => x.WidgetId,
                        principalTable: "Widget",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WidgetItemColorRange",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ValueFrom = table.Column<float>(type: "REAL", nullable: false),
                    ValueTo = table.Column<float>(type: "REAL", nullable: false),
                    ColorId = table.Column<int>(type: "INTEGER", nullable: true),
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

            migrationBuilder.CreateIndex(
                name: "IX_Device_AreaId",
                table: "Device",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Device_VendorId",
                table: "Device",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceDeviceGroup_GroupsId",
                table: "DeviceDeviceGroup",
                column: "GroupsId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceGroup_AreaId",
                table: "DeviceGroup",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceMqttTopic_DeviceId",
                table: "DeviceMqttTopic",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceMqttTopic_MqttBrokerId",
                table: "DeviceMqttTopic",
                column: "MqttBrokerId");

            migrationBuilder.CreateIndex(
                name: "IX_Widget_AreaId",
                table: "Widget",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_WidgetItem_DeviceTopicId",
                table: "WidgetItem",
                column: "DeviceTopicId");

            migrationBuilder.CreateIndex(
                name: "IX_WidgetItem_IconColorId",
                table: "WidgetItem",
                column: "IconColorId");

            migrationBuilder.CreateIndex(
                name: "IX_WidgetItem_IconId",
                table: "WidgetItem",
                column: "IconId");

            migrationBuilder.CreateIndex(
                name: "IX_WidgetItem_WidgetId",
                table: "WidgetItem",
                column: "WidgetId");

            migrationBuilder.CreateIndex(
                name: "IX_WidgetItemColorRange_ColorId",
                table: "WidgetItemColorRange",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_WidgetItemColorRange_WidgetItemId",
                table: "WidgetItemColorRange",
                column: "WidgetItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceDeviceGroup");

            migrationBuilder.DropTable(
                name: "WidgetItemColorRange");

            migrationBuilder.DropTable(
                name: "DeviceGroup");

            migrationBuilder.DropTable(
                name: "WidgetItem");

            migrationBuilder.DropTable(
                name: "Color");

            migrationBuilder.DropTable(
                name: "DeviceMqttTopic");

            migrationBuilder.DropTable(
                name: "Icon");

            migrationBuilder.DropTable(
                name: "Widget");

            migrationBuilder.DropTable(
                name: "Device");

            migrationBuilder.DropTable(
                name: "MqttBroker");

            migrationBuilder.DropTable(
                name: "Area");

            migrationBuilder.DropTable(
                name: "DeviceVendor");
        }
    }
}
