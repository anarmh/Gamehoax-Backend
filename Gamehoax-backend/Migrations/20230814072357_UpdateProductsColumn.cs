using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamehoax_backend.Migrations
{
    public partial class UpdateProductsColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RateCount",
                table: "Products",
                newName: "Popularity");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Popularity",
                table: "Products",
                newName: "RateCount");
        }
    }
}
