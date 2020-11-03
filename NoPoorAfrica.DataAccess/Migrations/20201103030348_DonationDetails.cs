using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NoPoorAfrica.DataAccess.Migrations
{
    public partial class DonationDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DonationDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    DonationCauseId = table.Column<int>(nullable: false),
                    DonorName = table.Column<string>(nullable: true),
                    DonationTotal = table.Column<double>(nullable: false),
                    PaymentStatus = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    TransactionId = table.Column<string>(nullable: true),
                    DonationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonationDetails", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DonationDetails");
        }
    }
}
