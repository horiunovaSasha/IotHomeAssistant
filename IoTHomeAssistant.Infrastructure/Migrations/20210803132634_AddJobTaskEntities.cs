using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IoTHomeAssistant.Infrastructure.Migrations
{
    public partial class AddJobTaskEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobTask",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTask", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Job",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    JobTaskId = table.Column<int>(type: "INTEGER", nullable: false),
                    StartTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FinishTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Succeed = table.Column<bool>(type: "INTEGER", nullable: false),
                    ErrorMessage = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Job_JobTask_JobTaskId",
                        column: x => x.JobTaskId,
                        principalTable: "JobTask",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobTaskCondition",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    JobTaskId = table.Column<int>(type: "INTEGER", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    DateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TriggeredEventId = table.Column<int>(type: "INTEGER", nullable: true),
                    TriggeredTaskId = table.Column<int>(type: "INTEGER", nullable: true),
                    SensorId = table.Column<int>(type: "INTEGER", nullable: true),
                    Operation = table.Column<int>(type: "INTEGER", nullable: true),
                    Value = table.Column<float>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTaskCondition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobTaskCondition_JobTask_JobTaskId",
                        column: x => x.JobTaskId,
                        principalTable: "JobTask",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobTaskCondition_JobTask_TriggeredTaskId",
                        column: x => x.TriggeredTaskId,
                        principalTable: "JobTask",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JobTaskExecution",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    JobTaskId = table.Column<int>(type: "INTEGER", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    WaitSeconds = table.Column<int>(type: "INTEGER", nullable: true),
                    TriggeredTaskId = table.Column<int>(type: "INTEGER", nullable: true),
                    DeviceId = table.Column<int>(type: "INTEGER", nullable: true),
                    CommandId = table.Column<int>(type: "INTEGER", nullable: true),
                    Value = table.Column<string>(type: "TEXT", nullable: true),
                    Order = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTaskExecution", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobTaskExecution_JobTask_JobTaskId",
                        column: x => x.JobTaskId,
                        principalTable: "JobTask",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobTaskExecution_JobTask_TriggeredTaskId",
                        column: x => x.TriggeredTaskId,
                        principalTable: "JobTask",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Job_JobTaskId",
                table: "Job",
                column: "JobTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_JobTaskCondition_JobTaskId",
                table: "JobTaskCondition",
                column: "JobTaskId");


            migrationBuilder.CreateIndex(
                name: "IX_JobTaskCondition_TriggeredTaskId",
                table: "JobTaskCondition",
                column: "TriggeredTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_JobTaskExecution_JobTaskId",
                table: "JobTaskExecution",
                column: "JobTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_JobTaskExecution_TriggeredTaskId",
                table: "JobTaskExecution",
                column: "TriggeredTaskId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Job");

            migrationBuilder.DropTable(
                name: "JobTaskCondition");

            migrationBuilder.DropTable(
                name: "JobTaskExecution");

            migrationBuilder.DropTable(
                name: "JobTask");
        }
    }
}
