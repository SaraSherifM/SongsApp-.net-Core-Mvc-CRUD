using Microsoft.EntityFrameworkCore.Migrations;

namespace SongsApp.Migrations
{
    public partial class CreateDbPlease : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Singers",
                columns: table => new
                {
                    SingerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SingerName = table.Column<string>(nullable: true),
                    SingerId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Singers", x => x.SingerId);
                    table.ForeignKey(
                        name: "FK_Singers_Singers_SingerId1",
                        column: x => x.SingerId1,
                        principalTable: "Singers",
                        principalColumn: "SingerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Songs",
                columns: table => new
                {
                    SongId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SongName = table.Column<string>(nullable: true),
                    ReleaseDate = table.Column<int>(nullable: false),
                    AlbumName = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    SingerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Songs", x => x.SongId);
                    table.ForeignKey(
                        name: "FK_Songs_Singers_SingerId",
                        column: x => x.SingerId,
                        principalTable: "Singers",
                        principalColumn: "SingerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Singers_SingerId1",
                table: "Singers",
                column: "SingerId1");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_SingerId",
                table: "Songs",
                column: "SingerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Songs");

            migrationBuilder.DropTable(
                name: "Singers");
        }
    }
}
