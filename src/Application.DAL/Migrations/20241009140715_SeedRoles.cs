using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
        table: "AspNetRoles",
        columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
        values: new object[,]
        {
                { Guid.NewGuid(), "Admin", "ADMIN", Guid.NewGuid().ToString() },
                { Guid.NewGuid(), "Member", "MEMBER", Guid.NewGuid().ToString() },
                { Guid.NewGuid(), "Author", "AUTHOR", Guid.NewGuid().ToString()},
                { Guid.NewGuid(),  "PendingApproval", "PENDINGAPPROVAL", new Guid().ToString()}
        });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
