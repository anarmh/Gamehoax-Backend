using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamehoax_backend.Migrations
{
    public partial class CreateBlogsColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BlogIcon",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlogIcon",
                table: "Blogs");
        }
    }
}
