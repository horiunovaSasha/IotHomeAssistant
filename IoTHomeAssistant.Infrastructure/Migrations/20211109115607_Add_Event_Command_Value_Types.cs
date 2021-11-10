using Microsoft.EntityFrameworkCore.Migrations;

namespace IoTHomeAssistant.Infrastructure.Migrations
{
    public partial class Add_Event_Command_Value_Types : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommandValueType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CommandId = table.Column<int>(type: "INTEGER", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Min = table.Column<int>(type: "INTEGER", nullable: true),
                    Max = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandValueType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommandValueType_Command_CommandId",
                        column: x => x.CommandId,
                        principalTable: "Command",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventValueType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EventId = table.Column<int>(type: "INTEGER", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Min = table.Column<int>(type: "INTEGER", nullable: true),
                    Max = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventValueType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventValueType_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommandValueTypeItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CommandValueTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandValueTypeItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommandValueTypeItem_CommandValueType_CommandValueTypeId",
                        column: x => x.CommandValueTypeId,
                        principalTable: "CommandValueType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventValueTypeItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EventValueTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventValueTypeItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventValueTypeItem_EventValueType_EventValueTypeId",
                        column: x => x.EventValueTypeId,
                        principalTable: "EventValueType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommandValueType_CommandId",
                table: "CommandValueType",
                column: "CommandId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommandValueTypeItem_CommandValueTypeId",
                table: "CommandValueTypeItem",
                column: "CommandValueTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EventValueType_EventId",
                table: "EventValueType",
                column: "EventId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventValueTypeItem_EventValueTypeId",
                table: "EventValueTypeItem",
                column: "EventValueTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommandValueTypeItem");

            migrationBuilder.DropTable(
                name: "EventValueTypeItem");

            migrationBuilder.DropTable(
                name: "CommandValueType");

            migrationBuilder.DropTable(
                name: "EventValueType");
        }
    }
}
