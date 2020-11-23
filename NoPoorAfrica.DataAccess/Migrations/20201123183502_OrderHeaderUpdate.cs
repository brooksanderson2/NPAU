using Microsoft.EntityFrameworkCore.Migrations;

namespace NoPoorAfrica.DataAccess.Migrations
{
    public partial class OrderHeaderUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "OrderHeader",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailPreference",
                table: "OrderHeader",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "OrderHeader");

            migrationBuilder.DropColumn(
                name: "EmailPreference",
                table: "OrderHeader");
        }
    }
}
