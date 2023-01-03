using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Paymentsystem.Shared.Migrations
{
    public partial class UpdatedRelationsAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Opencheckouts",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_ArticleId",
                table: "Prices",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Opencheckouts_UserId",
                table: "Opencheckouts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Opencheckouts_Users_UserId",
                table: "Opencheckouts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_Articles_ArticleId",
                table: "Prices",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Opencheckouts_Users_UserId",
                table: "Opencheckouts");

            migrationBuilder.DropForeignKey(
                name: "FK_Prices_Articles_ArticleId",
                table: "Prices");

            migrationBuilder.DropIndex(
                name: "IX_Prices_ArticleId",
                table: "Prices");

            migrationBuilder.DropIndex(
                name: "IX_Opencheckouts_UserId",
                table: "Opencheckouts");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Opencheckouts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
