using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SeedRolesData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
           table: "AspNetRoles",
           columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
           values: new object[,]
           {
                { Guid.NewGuid().ToString(), "Admin", "ADMIN", Guid.NewGuid().ToString() },
                { Guid.NewGuid().ToString(), "Member", "MEMBER", Guid.NewGuid().ToString() },
                { Guid.NewGuid().ToString(), "Author", "AUTHOR", Guid.NewGuid().ToString() }
           });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove the roles in the Down method
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Name",
                keyValues: new object[] { "Admin", "Member", "Author" });

        }
    }
}
