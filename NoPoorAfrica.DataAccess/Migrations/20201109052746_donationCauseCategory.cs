using Microsoft.EntityFrameworkCore.Migrations;

namespace NoPoorAfrica.DataAccess.Migrations
{
    public partial class donationCauseCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DonationCauseCategoryId",
                table: "DonationCause",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DonationCauseCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonationCauseCategory", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DonationCause_DonationCauseCategoryId",
                table: "DonationCause",
                column: "DonationCauseCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_DonationCause_DonationCauseCategory_DonationCauseCategoryId",
                table: "DonationCause",
                column: "DonationCauseCategoryId",
                principalTable: "DonationCauseCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonationCause_DonationCauseCategory_DonationCauseCategoryId",
                table: "DonationCause");

            migrationBuilder.DropTable(
                name: "DonationCauseCategory");

            migrationBuilder.DropIndex(
                name: "IX_DonationCause_DonationCauseCategoryId",
                table: "DonationCause");

            migrationBuilder.DropColumn(
                name: "DonationCauseCategoryId",
                table: "DonationCause");
        }
    }
}
