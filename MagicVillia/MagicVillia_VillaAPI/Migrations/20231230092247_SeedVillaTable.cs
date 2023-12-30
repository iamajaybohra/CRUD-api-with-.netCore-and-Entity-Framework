using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVillia_VillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedVillaTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenity", "CreatedDate", "Details", "ImageUrl", "Name", "Occupancy", "Rate", "Sqft", "UpdatedDate" },
                values: new object[] { 1, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Based in Jaipur", "https://unsplash.com/photos/tranquil-morning-at-famous-indian-tourist-landmark-jal-mahal-water-palace-at-sunrise-in-jaipur-ducks-and-birds-around-enjoy-the-serene-morning-jaipur-rajasthan-india-hx0IZ3Inw-Q", "Royal Palace", 5, 5.0, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
