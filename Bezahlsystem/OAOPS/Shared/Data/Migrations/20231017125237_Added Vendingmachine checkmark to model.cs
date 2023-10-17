using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OAOPS.Server.Data.Migrations
{
    public partial class AddedVendingmachinecheckmarktomodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVendingMachine",
                table: "Storages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVendingMachine",
                table: "Storages");
        }
    }
}
