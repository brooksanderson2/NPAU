using Microsoft.EntityFrameworkCore.Migrations;

namespace NoPoorAfrica.DataAccess.Migrations
{
    public partial class DonationDetailsUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "DonationDetails",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "FollowUp",
                table: "DonationDetails",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comments",
                table: "DonationDetails");

            migrationBuilder.DropColumn(
                name: "FollowUp",
                table: "DonationDetails");
        }
    }
}
