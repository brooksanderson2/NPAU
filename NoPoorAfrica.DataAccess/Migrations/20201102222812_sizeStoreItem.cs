using Microsoft.EntityFrameworkCore.Migrations;

namespace NoPoorAfrica.DataAccess.Migrations
{
    public partial class sizeStoreItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SizeId",
                table: "StoreItem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StoreItem_SizeId",
                table: "StoreItem",
                column: "SizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_StoreItem_Size_SizeId",
                table: "StoreItem",
                column: "SizeId",
                principalTable: "Size",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoreItem_Size_SizeId",
                table: "StoreItem");

            migrationBuilder.DropIndex(
                name: "IX_StoreItem_SizeId",
                table: "StoreItem");

            migrationBuilder.DropColumn(
                name: "SizeId",
                table: "StoreItem");
        }
    }
}
