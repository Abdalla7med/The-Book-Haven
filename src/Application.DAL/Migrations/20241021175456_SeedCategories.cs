using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SeedCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name", "IsDeleted" },
                values: new object[,]
                {
                { Guid.NewGuid(), "Fiction", false },
                { Guid.NewGuid(), "Non-Fiction", false },
                { Guid.NewGuid(), "Science", false },
                { Guid.NewGuid(), "Technology", false },
                { Guid.NewGuid(), "History", false },
                { Guid.NewGuid(), "Biography", false },
                { Guid.NewGuid(), "Fantasy", false },
                { Guid.NewGuid(), "Mystery", false },
                { Guid.NewGuid(), "Romance", false },
                { Guid.NewGuid(), "Self-Help", false }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValues: new object[]
                {
                    // Include the GUIDs of the categories to delete here
                });
        }
    }
}
