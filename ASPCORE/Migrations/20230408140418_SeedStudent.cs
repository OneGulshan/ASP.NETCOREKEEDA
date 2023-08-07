using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASPCORE.Migrations
{
    /// <inheritdoc />
    public partial class SeedStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "Id", "Enrolled", "Name" },
                values: new object[] { 1, new DateTime(2023, 5, 2, 17, 50, 0, 0, DateTimeKind.Unspecified), "Pankaj" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
