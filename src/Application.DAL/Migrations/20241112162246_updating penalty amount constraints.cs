using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Application.DAL.Migrations
{
    /// <inheritdoc />
    public partial class updatingpenaltyamountconstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Penalties",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,3)",
                oldPrecision: 3,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { new Guid("179e6695-e784-4ba9-89b7-29079dce496a"), false, "Non-Fiction" },
                    { new Guid("1abe99c2-576a-43ed-acff-efc7556b0b4f"), false, "Romance" },
                    { new Guid("5365b81e-4084-453b-83f0-bb36693c0c1a"), false, "Self-Help" },
                    { new Guid("5742387b-1861-4f92-94de-9f3afdd40cb5"), false, "History" },
                    { new Guid("838f8c25-f15c-4220-ade0-02d05cf1769c"), false, "Biography" },
                    { new Guid("911483bc-53dc-493e-bf64-b26652524e2e"), false, "Fantasy" },
                    { new Guid("a3454546-6d06-4843-a392-a101af500ed5"), false, "Fiction" },
                    { new Guid("af49b995-bb7b-43e5-8b97-04d5560c5047"), false, "Mystery" },
                    { new Guid("cdadd978-51e1-4ee8-97e8-996ba8717c0e"), false, "Technology" },
                    { new Guid("d555565d-0787-427e-952e-0458b707c67b"), false, "Science" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("179e6695-e784-4ba9-89b7-29079dce496a"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("1abe99c2-576a-43ed-acff-efc7556b0b4f"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("5365b81e-4084-453b-83f0-bb36693c0c1a"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("5742387b-1861-4f92-94de-9f3afdd40cb5"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("838f8c25-f15c-4220-ade0-02d05cf1769c"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("911483bc-53dc-493e-bf64-b26652524e2e"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("a3454546-6d06-4843-a392-a101af500ed5"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("af49b995-bb7b-43e5-8b97-04d5560c5047"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("cdadd978-51e1-4ee8-97e8-996ba8717c0e"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("d555565d-0787-427e-952e-0458b707c67b"));

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Penalties",
                type: "decimal(3,3)",
                precision: 3,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

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
    }
}
