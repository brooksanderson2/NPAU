using Microsoft.EntityFrameworkCore.Migrations;

namespace NoPoorAfrica.DataAccess.Migrations
{
    public partial class DonationCauseIamge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "DonationCause",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "DonationCause");
        }
    }
}
