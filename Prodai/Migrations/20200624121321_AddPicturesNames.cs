using Microsoft.EntityFrameworkCore.Migrations;

namespace Prodai.Migrations
{
    public partial class AddPicturesNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PictureName1",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PictureName2",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PictureName3",
                table: "Products",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureName1",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PictureName2",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PictureName3",
                table: "Products");
        }
    }
}
