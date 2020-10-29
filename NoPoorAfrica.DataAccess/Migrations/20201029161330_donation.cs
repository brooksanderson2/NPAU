using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NoPoorAfrica.DataAccess.Migrations
{
    public partial class donation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DonationCause",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    FromDate = table.Column<DateTime>(nullable: false),
                    ToDate = table.Column<DateTime>(nullable: false),
                    FundingGoal = table.Column<double>(nullable: false),
                    Country = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonationCause", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Donation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: true),
                    DonationAmount = table.Column<double>(nullable: false),
                    Country = table.Column<string>(nullable: true),
                    HowDidYouHear = table.Column<string>(nullable: true),
                    FollowUp = table.Column<bool>(nullable: false),
                    CauseId = table.Column<int>(nullable: false),
                    DonationCauseId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Donation_DonationCause_DonationCauseId",
                        column: x => x.DonationCauseId,
                        principalTable: "DonationCause",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Donation_DonationCauseId",
                table: "Donation",
                column: "DonationCauseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Donation");

            migrationBuilder.DropTable(
                name: "DonationCause");
        }
    }
}
