using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogCore.DataAccess.Migrations
{
    public partial class ArticlesModelUpdateColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Articles");

            migrationBuilder.AddColumn<string>(
                name: "ArticleCreationDate",
                table: "Articles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArticleDescription",
                table: "Articles",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ArticleImageUrl",
                table: "Articles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArticleCreationDate",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "ArticleDescription",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "ArticleImageUrl",
                table: "Articles");

            migrationBuilder.AddColumn<string>(
                name: "CreateAt",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
