using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NoPoorAfrica.DataAccess.Migrations
{
    public partial class ArticleFiles111120 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "PurchaseHistory",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Article",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Template",
                table: "Article",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RouteName",
                table: "Article",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ArticleFiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticleId = table.Column<int>(nullable: false),
                    FilePath = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleFiles_Article_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Article",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseHistory_ApplicationUserId",
                table: "PurchaseHistory",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseHistory_StoreItemId",
                table: "PurchaseHistory",
                column: "StoreItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleFiles_ArticleId",
                table: "ArticleFiles",
                column: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseHistory_AspNetUsers_ApplicationUserId",
                table: "PurchaseHistory",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseHistory_StoreItem_StoreItemId",
                table: "PurchaseHistory",
                column: "StoreItemId",
                principalTable: "StoreItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseHistory_AspNetUsers_ApplicationUserId",
                table: "PurchaseHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseHistory_StoreItem_StoreItemId",
                table: "PurchaseHistory");

            migrationBuilder.DropTable(
                name: "ArticleCategory");

            migrationBuilder.DropTable(
                name: "ArticleFiles");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseHistory_ApplicationUserId",
                table: "PurchaseHistory");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseHistory_StoreItemId",
                table: "PurchaseHistory");

            migrationBuilder.DropIndex(
                name: "IX_Article_ArticleCategoryId",
                table: "Article");

            migrationBuilder.DropColumn(
                name: "ArticleCategoryId",
                table: "Article");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Article");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "PurchaseHistory",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Article",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Template",
                table: "Article",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "RouteName",
                table: "Article",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
