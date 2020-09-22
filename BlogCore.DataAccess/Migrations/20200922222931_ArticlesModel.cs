using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogCore.DataAccess.Migrations
{
    public partial class ArticlesModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    ArticleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticleName = table.Column<string>(nullable: false),
                    CreateAt = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    Fk_CategoryID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.ArticleID);
                    table.ForeignKey(
                        name: "FK_Articles_Categories_Fk_CategoryID",
                        column: x => x.Fk_CategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_Fk_CategoryID",
                table: "Articles",
                column: "Fk_CategoryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");
        }
    }
}
