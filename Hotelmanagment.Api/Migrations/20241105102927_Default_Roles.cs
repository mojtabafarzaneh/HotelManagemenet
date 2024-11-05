using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hotelmanagment.Api.Migrations
{
    /// <inheritdoc />
    public partial class Default_Roles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b7ed33e4-74a1-4d52-84a8-404014ce06f9", null, "User", "USER" },
                    { "d3f9e7c0-7ee4-4a70-b8b7-b9f530705958", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b7ed33e4-74a1-4d52-84a8-404014ce06f9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3f9e7c0-7ee4-4a70-b8b7-b9f530705958");
        }
    }
}
