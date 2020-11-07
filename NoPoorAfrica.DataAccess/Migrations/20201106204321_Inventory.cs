using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NoPoorAfrica.DataAccess.Migrations
{
    public partial class Inventory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Inventory",
                table: "StoreItem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Article",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    TitleFont = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true),
                    BodyFont = table.Column<string>(nullable: true),
                    BodyTextSize = table.Column<float>(nullable: false),
                    Template = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    RouteName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Size",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Size", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Article");

            migrationBuilder.DropTable(
                name: "Size");

            migrationBuilder.DropColumn(
                name: "Inventory",
                table: "StoreItem");
        }
    }
}
