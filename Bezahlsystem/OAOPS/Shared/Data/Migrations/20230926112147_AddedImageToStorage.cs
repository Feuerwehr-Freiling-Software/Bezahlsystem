using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OAOPS.Server.Data.Migrations
{
    public partial class AddedImageToStorage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageData",
                table: "Storages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Storages");
        }
    }
}
