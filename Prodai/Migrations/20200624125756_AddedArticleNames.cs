using Microsoft.EntityFrameworkCore.Migrations;

namespace Prodai.Migrations
{
    public partial class AddedArticleNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ArticleName",
                table: "Products",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ArticleName",
                table: "BuyProducts",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArticleName",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ArticleName",
                table: "BuyProducts");
        }
    }
}
