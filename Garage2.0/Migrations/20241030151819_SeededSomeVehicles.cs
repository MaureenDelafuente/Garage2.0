using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Garage2._0.Migrations
{
    /// <inheritdoc />
    public partial class SeededSomeVehicles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Vehicle",
                columns: new[] { "Id", "ArrivalTime", "Brand", "CheckoutTime", "Color", "Model", "NumberOfWheels", "RegisterNumber", "VehicleType" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "BMW", new DateTime(2024, 10, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Black", "X6", 4, "RGC 234", "Desiel" },
                    { 2, new DateTime(2024, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "VOLVO", new DateTime(2024, 10, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "white", "V40", 4, "INT 765", "Bensin" },
                    { 3, new DateTime(2024, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "AUDI", new DateTime(2024, 10, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "WHITE", "A6", 4, "KMV 456", "EL" },
                    { 4, new DateTime(2024, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "VOLVO", new DateTime(2024, 10, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Black", "GX", 4, "ZDG 980", "Desiel" },
                    { 5, new DateTime(2024, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "SAAB", new DateTime(2024, 10, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Black", "A5", 4, "LKW 285", "Desiel" },
                    { 6, new DateTime(2024, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "KIA", new DateTime(2024, 10, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "RED", "BX", 4, "FFC 170", "BENSIN" },
                    { 7, new DateTime(2024, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "BMW", new DateTime(2024, 10, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "GREEN", "X5", 4, "XZS 376", "Desiel" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Vehicle",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Vehicle",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Vehicle",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Vehicle",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Vehicle",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Vehicle",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Vehicle",
                keyColumn: "Id",
                keyValue: 7);
        }
    }
}
