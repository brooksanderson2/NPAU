using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NoPoorAfrica.DataAccess.Migrations
{
    public partial class pendingWire : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PendingWire",
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
                    Comments = table.Column<string>(nullable: true),
                    FollowUp = table.Column<bool>(nullable: false),
                    TransactionId = table.Column<string>(nullable: true),
                    DonationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PendingWire", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PendingWire_DonationCause_DonationCauseId",
                        column: x => x.DonationCauseId,
                        principalTable: "DonationCause",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PendingWire_DonationCauseId",
                table: "PendingWire",
                column: "DonationCauseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PendingWire");
        }
    }
}
