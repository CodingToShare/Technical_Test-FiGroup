using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class IdentityUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TaskStatus",
                keyColumn: "TaskStatusID",
                keyValue: new Guid("596d93ab-acd0-4d8e-a512-310e7026722a"));

            migrationBuilder.DeleteData(
                table: "TaskStatus",
                keyColumn: "TaskStatusID",
                keyValue: new Guid("9305c59d-9c36-4bbe-855f-4eebdd7bc65d"));

            migrationBuilder.InsertData(
                table: "TaskStatus",
                columns: new[] { "TaskStatusID", "Created", "CreatedBy", "Modified", "ModifiedBy", "TaskStatusName" },
                values: new object[,]
                {
                    { new Guid("5fc73765-cdfe-4d80-80da-b0ed803d0c60"), new DateTime(2023, 9, 4, 19, 32, 18, 978, DateTimeKind.Utc).AddTicks(3495), "WebAPITask", new DateTime(2023, 9, 4, 19, 32, 18, 978, DateTimeKind.Utc).AddTicks(3502), "WebAPITask", "Completada" },
                    { new Guid("cee4a830-f4d0-48ab-87ef-2cc9e1eedb83"), new DateTime(2023, 9, 4, 19, 32, 18, 978, DateTimeKind.Utc).AddTicks(3504), "WebAPITask", new DateTime(2023, 9, 4, 19, 32, 18, 978, DateTimeKind.Utc).AddTicks(3505), "WebAPITask", "No completada" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TaskStatus",
                keyColumn: "TaskStatusID",
                keyValue: new Guid("5fc73765-cdfe-4d80-80da-b0ed803d0c60"));

            migrationBuilder.DeleteData(
                table: "TaskStatus",
                keyColumn: "TaskStatusID",
                keyValue: new Guid("cee4a830-f4d0-48ab-87ef-2cc9e1eedb83"));

            migrationBuilder.InsertData(
                table: "TaskStatus",
                columns: new[] { "TaskStatusID", "Created", "CreatedBy", "Modified", "ModifiedBy", "TaskStatusName" },
                values: new object[,]
                {
                    { new Guid("596d93ab-acd0-4d8e-a512-310e7026722a"), new DateTime(2023, 9, 3, 16, 58, 21, 279, DateTimeKind.Utc).AddTicks(2219), "WebAPITask", new DateTime(2023, 9, 3, 16, 58, 21, 279, DateTimeKind.Utc).AddTicks(2219), "WebAPITask", "No completada" },
                    { new Guid("9305c59d-9c36-4bbe-855f-4eebdd7bc65d"), new DateTime(2023, 9, 3, 16, 58, 21, 279, DateTimeKind.Utc).AddTicks(2199), "WebAPITask", new DateTime(2023, 9, 3, 16, 58, 21, 279, DateTimeKind.Utc).AddTicks(2215), "WebAPITask", "Completada" }
                });
        }
    }
}
