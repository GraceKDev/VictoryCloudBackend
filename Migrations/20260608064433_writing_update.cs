using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VictoryCloudApi.Migrations
{
    /// <inheritdoc />
    public partial class writing_update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WritingChapter_Writing_WritingId",
                table: "WritingChapter");

            migrationBuilder.DropForeignKey(
                name: "FK_WritingChapterContent_WritingChapter_WritingChapterId",
                table: "WritingChapterContent");

            migrationBuilder.DropColumn(
                name: "WritingChapterParentId",
                table: "WritingChapterContent");

            migrationBuilder.DropColumn(
                name: "WritingParentId",
                table: "WritingChapter");

            migrationBuilder.AlterColumn<int>(
                name: "WritingChapterId",
                table: "WritingChapterContent",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "WritingId",
                table: "WritingChapter",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WritingChapter_Writing_WritingId",
                table: "WritingChapter",
                column: "WritingId",
                principalTable: "Writing",
                principalColumn: "WritingId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WritingChapterContent_WritingChapter_WritingChapterId",
                table: "WritingChapterContent",
                column: "WritingChapterId",
                principalTable: "WritingChapter",
                principalColumn: "WritingChapterId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WritingChapter_Writing_WritingId",
                table: "WritingChapter");

            migrationBuilder.DropForeignKey(
                name: "FK_WritingChapterContent_WritingChapter_WritingChapterId",
                table: "WritingChapterContent");

            migrationBuilder.AlterColumn<int>(
                name: "WritingChapterId",
                table: "WritingChapterContent",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "WritingChapterParentId",
                table: "WritingChapterContent",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "WritingId",
                table: "WritingChapter",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "WritingParentId",
                table: "WritingChapter",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_WritingChapter_Writing_WritingId",
                table: "WritingChapter",
                column: "WritingId",
                principalTable: "Writing",
                principalColumn: "WritingId");

            migrationBuilder.AddForeignKey(
                name: "FK_WritingChapterContent_WritingChapter_WritingChapterId",
                table: "WritingChapterContent",
                column: "WritingChapterId",
                principalTable: "WritingChapter",
                principalColumn: "WritingChapterId");
        }
    }
}
