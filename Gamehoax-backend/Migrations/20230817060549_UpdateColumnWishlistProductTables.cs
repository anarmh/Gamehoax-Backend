using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamehoax_backend.Migrations
{
    public partial class UpdateColumnWishlistProductTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "WishlistProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "WishlistProducts");
        }
    }
}
