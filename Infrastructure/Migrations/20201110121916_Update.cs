using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TShirts_AspNetUsers_UserId",
                table: "TShirts");

            migrationBuilder.DropIndex(
                name: "IX_TShirts_UserId",
                table: "TShirts");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TShirts",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreateDate",
                table: "TShirts",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "TShirts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "TShirts");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TShirts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "TShirts",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TShirts_UserId",
                table: "TShirts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TShirts_AspNetUsers_UserId",
                table: "TShirts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
