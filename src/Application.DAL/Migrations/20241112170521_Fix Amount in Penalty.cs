using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Application.DAL.Migrations
{
    /// <inheritdoc />
    public partial class FixAmountinPenalty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                type: "decimal(5,2)",
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
                    { new Guid("4d4cad8b-3132-4361-b9d5-289735b0f24f"), false, "Fiction" },
                    { new Guid("51d707b3-37b0-431e-b4e2-35cfdfe6d97e"), false, "Romance" },
                    { new Guid("5309d513-12d2-4fec-9d13-91fe5b817215"), false, "History" },
                    { new Guid("5b1cb046-981f-45fd-aab3-c868639a0d7b"), false, "Biography" },
                    { new Guid("897e6c49-054d-47d3-815d-d8afa8e11e15"), false, "Self-Help" },
                    { new Guid("99183737-d16f-49c0-8c57-98fa08e474fb"), false, "Fantasy" },
                    { new Guid("b167d21f-89e7-4e03-af45-5dba4480318d"), false, "Technology" },
                    { new Guid("cce5aec2-bd93-4ebb-9d0b-deff18dbf2c6"), false, "Non-Fiction" },
                    { new Guid("eaf144d5-3d84-4b8d-91b8-bfbfc9aa7182"), false, "Mystery" },
                    { new Guid("f55162c9-afce-4966-a55d-c56866eb23a1"), false, "Science" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("4d4cad8b-3132-4361-b9d5-289735b0f24f"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("51d707b3-37b0-431e-b4e2-35cfdfe6d97e"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("5309d513-12d2-4fec-9d13-91fe5b817215"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("5b1cb046-981f-45fd-aab3-c868639a0d7b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("897e6c49-054d-47d3-815d-d8afa8e11e15"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("99183737-d16f-49c0-8c57-98fa08e474fb"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("b167d21f-89e7-4e03-af45-5dba4480318d"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("cce5aec2-bd93-4ebb-9d0b-deff18dbf2c6"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("eaf144d5-3d84-4b8d-91b8-bfbfc9aa7182"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("f55162c9-afce-4966-a55d-c56866eb23a1"));

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Penalties",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
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
    }
}
