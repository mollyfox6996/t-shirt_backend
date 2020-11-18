using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class UpdateTshirtModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "TShirts",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TShirts_AspNetUsers_UserId",
                table: "TShirts");

            migrationBuilder.DropIndex(
                name: "IX_TShirts_UserId",
                table: "TShirts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TShirts");
        }
    }
}
