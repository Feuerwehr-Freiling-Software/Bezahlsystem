using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Paymentsystem.Shared.Migrations
{
    public partial class UpdatedRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserHasNotifications",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserBoughtArticleFromSlots",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Topups",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ExecutorId",
                table: "Topups",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Suggestions",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_UserHasNotifications_UserId",
                table: "UserHasNotifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBoughtArticleFromSlots_SlotInStorageHasArticleId",
                table: "UserBoughtArticleFromSlots",
                column: "SlotInStorageHasArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBoughtArticleFromSlots_UserId",
                table: "UserBoughtArticleFromSlots",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Topups_ExecutorId",
                table: "Topups",
                column: "ExecutorId");

            migrationBuilder.CreateIndex(
                name: "IX_Topups_UserId",
                table: "Topups",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Suggestions_UserId",
                table: "Suggestions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SlotInStorageHasArticles_ArticleId",
                table: "SlotInStorageHasArticles",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_SlotInStorageHasArticles_SlotId",
                table: "SlotInStorageHasArticles",
                column: "SlotId");

            migrationBuilder.AddForeignKey(
                name: "FK_SlotInStorageHasArticles_Articles_ArticleId",
                table: "SlotInStorageHasArticles",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_SlotInStorageHasArticles_Slots_SlotId",
                table: "SlotInStorageHasArticles",
                column: "SlotId",
                principalTable: "Slots",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Suggestions_Users_UserId",
                table: "Suggestions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Topups_Users_ExecutorId",
                table: "Topups",
                column: "ExecutorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Topups_Users_UserId",
                table: "Topups",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBoughtArticleFromSlots_SlotInStorageHasArticles_SlotInStorageHasArticleId",
                table: "UserBoughtArticleFromSlots",
                column: "SlotInStorageHasArticleId",
                principalTable: "SlotInStorageHasArticles",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBoughtArticleFromSlots_Users_UserId",
                table: "UserBoughtArticleFromSlots",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_UserHasNotifications_Users_UserId",
                table: "UserHasNotifications",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SlotInStorageHasArticles_Articles_ArticleId",
                table: "SlotInStorageHasArticles");

            migrationBuilder.DropForeignKey(
                name: "FK_SlotInStorageHasArticles_Slots_SlotId",
                table: "SlotInStorageHasArticles");

            migrationBuilder.DropForeignKey(
                name: "FK_Suggestions_Users_UserId",
                table: "Suggestions");

            migrationBuilder.DropForeignKey(
                name: "FK_Topups_Users_ExecutorId",
                table: "Topups");

            migrationBuilder.DropForeignKey(
                name: "FK_Topups_Users_UserId",
                table: "Topups");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBoughtArticleFromSlots_SlotInStorageHasArticles_SlotInStorageHasArticleId",
                table: "UserBoughtArticleFromSlots");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBoughtArticleFromSlots_Users_UserId",
                table: "UserBoughtArticleFromSlots");

            migrationBuilder.DropForeignKey(
                name: "FK_UserHasNotifications_Users_UserId",
                table: "UserHasNotifications");

            migrationBuilder.DropIndex(
                name: "IX_UserHasNotifications_UserId",
                table: "UserHasNotifications");

            migrationBuilder.DropIndex(
                name: "IX_UserBoughtArticleFromSlots_SlotInStorageHasArticleId",
                table: "UserBoughtArticleFromSlots");

            migrationBuilder.DropIndex(
                name: "IX_UserBoughtArticleFromSlots_UserId",
                table: "UserBoughtArticleFromSlots");

            migrationBuilder.DropIndex(
                name: "IX_Topups_ExecutorId",
                table: "Topups");

            migrationBuilder.DropIndex(
                name: "IX_Topups_UserId",
                table: "Topups");

            migrationBuilder.DropIndex(
                name: "IX_Suggestions_UserId",
                table: "Suggestions");

            migrationBuilder.DropIndex(
                name: "IX_SlotInStorageHasArticles_ArticleId",
                table: "SlotInStorageHasArticles");

            migrationBuilder.DropIndex(
                name: "IX_SlotInStorageHasArticles_SlotId",
                table: "SlotInStorageHasArticles");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserHasNotifications",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserBoughtArticleFromSlots",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Topups",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ExecutorId",
                table: "Topups",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Suggestions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
