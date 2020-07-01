using Microsoft.EntityFrameworkCore.Migrations;

namespace Prodai.Migrations
{
    public partial class AddCategoryToBuyProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "BuyProducts",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "BuyProducts");
        }
    }
}
