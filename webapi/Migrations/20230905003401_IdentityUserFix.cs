using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class IdentityUserFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { new Guid("45617958-e263-459c-9ddb-d7a920792fdb"), new DateTime(2023, 9, 4, 19, 34, 1, 699, DateTimeKind.Utc).AddTicks(3787), "WebAPITask", new DateTime(2023, 9, 4, 19, 34, 1, 699, DateTimeKind.Utc).AddTicks(3793), "WebAPITask", "Completada" },
                    { new Guid("e49795b0-cf64-4781-849f-c7c33d84f4d1"), new DateTime(2023, 9, 4, 19, 34, 1, 699, DateTimeKind.Utc).AddTicks(3795), "WebAPITask", new DateTime(2023, 9, 4, 19, 34, 1, 699, DateTimeKind.Utc).AddTicks(3796), "WebAPITask", "No completada" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TaskStatus",
                keyColumn: "TaskStatusID",
                keyValue: new Guid("45617958-e263-459c-9ddb-d7a920792fdb"));

            migrationBuilder.DeleteData(
                table: "TaskStatus",
                keyColumn: "TaskStatusID",
                keyValue: new Guid("e49795b0-cf64-4781-849f-c7c33d84f4d1"));

            migrationBuilder.InsertData(
                table: "TaskStatus",
                columns: new[] { "TaskStatusID", "Created", "CreatedBy", "Modified", "ModifiedBy", "TaskStatusName" },
                values: new object[,]
                {
                    { new Guid("5fc73765-cdfe-4d80-80da-b0ed803d0c60"), new DateTime(2023, 9, 4, 19, 32, 18, 978, DateTimeKind.Utc).AddTicks(3495), "WebAPITask", new DateTime(2023, 9, 4, 19, 32, 18, 978, DateTimeKind.Utc).AddTicks(3502), "WebAPITask", "Completada" },
                    { new Guid("cee4a830-f4d0-48ab-87ef-2cc9e1eedb83"), new DateTime(2023, 9, 4, 19, 32, 18, 978, DateTimeKind.Utc).AddTicks(3504), "WebAPITask", new DateTime(2023, 9, 4, 19, 32, 18, 978, DateTimeKind.Utc).AddTicks(3505), "WebAPITask", "No completada" }
                });
        }
    }
}
