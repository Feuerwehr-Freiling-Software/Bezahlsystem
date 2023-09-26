using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OAOPS.Server.Data.Migrations
{
    public partial class AddedBase64PictureToArticle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PicturePath",
                table: "Articles");

            migrationBuilder.AddColumn<string>(
                name: "Base64data",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Base64data",
                table: "Articles");

            migrationBuilder.AddColumn<string>(
                name: "PicturePath",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
