using Microsoft.EntityFrameworkCore.Migrations;

namespace SongsApp.Migrations
{
    public partial class correctmistake : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Singers_Singers_SingerId1",
                table: "Singers");

            migrationBuilder.DropIndex(
                name: "IX_Singers_SingerId1",
                table: "Singers");

            migrationBuilder.DropColumn(
                name: "SingerId1",
                table: "Singers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SingerId1",
                table: "Singers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Singers_SingerId1",
                table: "Singers",
                column: "SingerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Singers_Singers_SingerId1",
                table: "Singers",
                column: "SingerId1",
                principalTable: "Singers",
                principalColumn: "SingerId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
