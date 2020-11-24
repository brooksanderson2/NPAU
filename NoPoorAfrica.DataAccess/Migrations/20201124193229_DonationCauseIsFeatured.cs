using Microsoft.EntityFrameworkCore.Migrations;

namespace NoPoorAfrica.DataAccess.Migrations
{
    public partial class DonationCauseIsFeatured : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFeatured",
                table: "DonationCause",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFeatured",
                table: "DonationCause");
        }
    }
}
