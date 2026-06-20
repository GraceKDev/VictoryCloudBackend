using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VictoryCloudApi.Migrations
{
    /// <inheritdoc />
    public partial class updateuploaddate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UpdatedAt",
                table: "WritingChapter",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UploadedAt",
                table: "WritingChapter",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UpdatedAt",
                table: "Writing",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UpdatedAt",
                table: "Comics",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UploadedAt",
                table: "Comics",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UpdatedAt",
                table: "ComicChapters",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UploadedAt",
                table: "ComicChapters",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UpdatedAt",
                table: "Art",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UploadedAt",
                table: "Art",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "WritingChapter");

            migrationBuilder.DropColumn(
                name: "UploadedAt",
                table: "WritingChapter");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Writing");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Comics");

            migrationBuilder.DropColumn(
                name: "UploadedAt",
                table: "Comics");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ComicChapters");

            migrationBuilder.DropColumn(
                name: "UploadedAt",
                table: "ComicChapters");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Art");

            migrationBuilder.DropColumn(
                name: "UploadedAt",
                table: "Art");
        }
    }
}
