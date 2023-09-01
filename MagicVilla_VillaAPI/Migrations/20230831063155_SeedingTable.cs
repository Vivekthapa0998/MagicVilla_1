using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVilla_VillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenity", "CreatedDate", "Details", "ImageUrl", "Name", "Occupancy", "Rate", "Sqft", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 8, 31, 12, 1, 54, 903, DateTimeKind.Local).AddTicks(8352), "this villa is very beautiful villa with beautiful surrounding and sunrise view", "https://stock.adobe.com/in/images/tropical-villa-at-night/309464116", "Royal villa", 4, 20000.0, 2100, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 1, new DateTime(2023, 8, 31, 12, 1, 54, 903, DateTimeKind.Local).AddTicks(8373), "this villa is very beautiful villa with beautiful surrounding and sunrise view", "https://stock.adobe.com/in/images/tropical-villa-at-night/309464116", "vivek villa", 4, 20000.0, 2100, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 1, new DateTime(2023, 8, 31, 12, 1, 54, 903, DateTimeKind.Local).AddTicks(8375), "this villa is very beautiful villa with beautiful surrounding and sunrise view", "https://stock.adobe.com/in/images/tropical-villa-at-night/309464116", "Royal hill villa", 4, 20000.0, 2100, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 1, new DateTime(2023, 8, 31, 12, 1, 54, 903, DateTimeKind.Local).AddTicks(8378), "this villa is very beautiful villa with beautiful surrounding and sunrise view", "https://stock.adobe.com/in/images/tropical-villa-at-night/309464116", "villa in hills", 4, 20000.0, 2100, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 1, new DateTime(2023, 8, 31, 12, 1, 54, 903, DateTimeKind.Local).AddTicks(8380), "this villa is very beautiful villa with beautiful surrounding and sunrise view", "https://stock.adobe.com/in/images/tropical-villa-at-night/309464116", "villa rosemary", 4, 20000.0, 2100, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
