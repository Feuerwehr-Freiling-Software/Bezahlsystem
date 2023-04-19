using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OAOPS.Server.Data.Migrations
{
    public partial class MinorChangesToModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBoughtArticleFromSlots_AspNetUsers_UserId1",
                table: "UserBoughtArticleFromSlots");

            migrationBuilder.DropIndex(
                name: "IX_UserBoughtArticleFromSlots_UserId1",
                table: "UserBoughtArticleFromSlots");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserBoughtArticleFromSlots");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserBoughtArticleFromSlots",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_UserBoughtArticleFromSlots_UserId",
                table: "UserBoughtArticleFromSlots",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBoughtArticleFromSlots_AspNetUsers_UserId",
                table: "UserBoughtArticleFromSlots",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBoughtArticleFromSlots_AspNetUsers_UserId",
                table: "UserBoughtArticleFromSlots");

            migrationBuilder.DropIndex(
                name: "IX_UserBoughtArticleFromSlots_UserId",
                table: "UserBoughtArticleFromSlots");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserBoughtArticleFromSlots",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "UserBoughtArticleFromSlots",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserBoughtArticleFromSlots_UserId1",
                table: "UserBoughtArticleFromSlots",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBoughtArticleFromSlots_AspNetUsers_UserId1",
                table: "UserBoughtArticleFromSlots",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
