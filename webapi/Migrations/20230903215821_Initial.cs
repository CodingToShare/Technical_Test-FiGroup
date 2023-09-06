using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskStatus",
                columns: table => new
                {
                    TaskStatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskStatusName = table.Column<string>(type: "varchar(200)", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskStatus", x => x.TaskStatusID);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    TaskID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskDescription = table.Column<string>(type: "varchar(200)", nullable: false),
                    TaskStatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.TaskID);
                    table.ForeignKey(
                        name: "FK_Tasks_TaskStatus_TaskStatusID",
                        column: x => x.TaskStatusID,
                        principalTable: "TaskStatus",
                        principalColumn: "TaskStatusID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TaskStatus",
                columns: new[] { "TaskStatusID", "Created", "CreatedBy", "Modified", "ModifiedBy", "TaskStatusName" },
                values: new object[,]
                {
                    { new Guid("596d93ab-acd0-4d8e-a512-310e7026722a"), new DateTime(2023, 9, 3, 16, 58, 21, 279, DateTimeKind.Utc).AddTicks(2219), "WebAPITask", new DateTime(2023, 9, 3, 16, 58, 21, 279, DateTimeKind.Utc).AddTicks(2219), "WebAPITask", "No completada" },
                    { new Guid("9305c59d-9c36-4bbe-855f-4eebdd7bc65d"), new DateTime(2023, 9, 3, 16, 58, 21, 279, DateTimeKind.Utc).AddTicks(2199), "WebAPITask", new DateTime(2023, 9, 3, 16, 58, 21, 279, DateTimeKind.Utc).AddTicks(2215), "WebAPITask", "Completada" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_TaskStatusID",
                table: "Tasks",
                column: "TaskStatusID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "TaskStatus");
        }
    }
}
