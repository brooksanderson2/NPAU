using Microsoft.EntityFrameworkCore.Migrations;

namespace NoPoorAfrica.DataAccess.Migrations
{
    public partial class GoalProgress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddColumn<double>(
                name: "GoalProgress",
                table: "DonationCause",
                nullable: false,
                defaultValue: 0.0);

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {     


            migrationBuilder.DropColumn(
                name: "GoalProgress",
                table: "DonationCause");          
        }
    }
}
