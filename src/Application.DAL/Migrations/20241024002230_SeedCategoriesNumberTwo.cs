using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Application.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SeedCategoriesNumberTwo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { new Guid("1029e1fd-4a13-4b96-926b-4c44388a7356"), false, "Self-Help" },
                    { new Guid("36d5c6ef-ad7c-42bb-a65d-f0dfa07b9175"), false, "Technology" },
                    { new Guid("43bf38f3-53ca-4fed-839c-a066542176ca"), false, "History" },
                    { new Guid("47d677fb-b1b3-4697-9903-9c3cb4a2e802"), false, "Science" },
                    { new Guid("71cc1e62-7478-4abc-a8e7-4787b5ad5d8c"), false, "Mystery" },
                    { new Guid("ad2e417b-6dd2-494f-9906-7eabb5e756c4"), false, "Fantasy" },
                    { new Guid("b0ebdf8f-cc19-4d95-8b7f-bbfa70c687e1"), false, "Fiction" },
                    { new Guid("d69a1c57-a5c2-49e8-9ff7-d5ca92e20077"), false, "Biography" },
                    { new Guid("e2fdc335-eb24-49c9-a858-e1e4066847c7"), false, "Romance" },
                    { new Guid("f64d3aa9-837d-45ef-ad38-27457f2316de"), false, "Non-Fiction" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("1029e1fd-4a13-4b96-926b-4c44388a7356"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("36d5c6ef-ad7c-42bb-a65d-f0dfa07b9175"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("43bf38f3-53ca-4fed-839c-a066542176ca"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("47d677fb-b1b3-4697-9903-9c3cb4a2e802"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("71cc1e62-7478-4abc-a8e7-4787b5ad5d8c"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("ad2e417b-6dd2-494f-9906-7eabb5e756c4"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("b0ebdf8f-cc19-4d95-8b7f-bbfa70c687e1"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("d69a1c57-a5c2-49e8-9ff7-d5ca92e20077"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("e2fdc335-eb24-49c9-a858-e1e4066847c7"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("f64d3aa9-837d-45ef-ad38-27457f2316de"));
        }
    }
}
