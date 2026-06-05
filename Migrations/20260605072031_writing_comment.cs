using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace VictoryCloudApi.Migrations
{
    /// <inheritdoc />
    public partial class writing_comment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chapters");

            migrationBuilder.CreateTable(
                name: "ComicChapters",
                columns: table => new
                {
                    ComicChapterId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChapterTitle = table.Column<string>(type: "text", nullable: false),
                    Images = table.Column<string[]>(type: "text[]", nullable: false),
                    ComicId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComicChapters", x => x.ComicChapterId);
                    table.ForeignKey(
                        name: "FK_ComicChapters_Comics_ComicId",
                        column: x => x.ComicId,
                        principalTable: "Comics",
                        principalColumn: "ComicId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Writing",
                columns: table => new
                {
                    WritingId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Tags = table.Column<string[]>(type: "text[]", nullable: false),
                    Links = table.Column<string[]>(type: "text[]", nullable: false),
                    UploadedAt = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Writing", x => x.WritingId);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    CommentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CommentMessage = table.Column<string>(type: "text", nullable: false),
                    Author = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<string>(type: "text", nullable: false),
                    Likes = table.Column<int>(type: "integer", nullable: false),
                    ParentId = table.Column<int>(type: "integer", nullable: true),
                    WritingId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comment_Writing_WritingId",
                        column: x => x.WritingId,
                        principalTable: "Writing",
                        principalColumn: "WritingId");
                });

            migrationBuilder.CreateTable(
                name: "WritingChapter",
                columns: table => new
                {
                    WritingChapterId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WritingParentId = table.Column<int>(type: "integer", nullable: false),
                    WritingChapterTitle = table.Column<string>(type: "text", nullable: false),
                    WritingId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WritingChapter", x => x.WritingChapterId);
                    table.ForeignKey(
                        name: "FK_WritingChapter_Writing_WritingId",
                        column: x => x.WritingId,
                        principalTable: "Writing",
                        principalColumn: "WritingId");
                });

            migrationBuilder.CreateTable(
                name: "WritingChapterContent",
                columns: table => new
                {
                    WritingChapterContentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WritingChapterParentId = table.Column<int>(type: "integer", nullable: false),
                    WritingContentPosition = table.Column<string>(type: "text", nullable: false),
                    WritingContentType = table.Column<string>(type: "text", nullable: false),
                    WritingChapterId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WritingChapterContent", x => x.WritingChapterContentId);
                    table.ForeignKey(
                        name: "FK_WritingChapterContent_WritingChapter_WritingChapterId",
                        column: x => x.WritingChapterId,
                        principalTable: "WritingChapter",
                        principalColumn: "WritingChapterId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComicChapters_ComicId",
                table: "ComicChapters",
                column: "ComicId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_WritingId",
                table: "Comment",
                column: "WritingId");

            migrationBuilder.CreateIndex(
                name: "IX_WritingChapter_WritingId",
                table: "WritingChapter",
                column: "WritingId");

            migrationBuilder.CreateIndex(
                name: "IX_WritingChapterContent_WritingChapterId",
                table: "WritingChapterContent",
                column: "WritingChapterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComicChapters");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "WritingChapterContent");

            migrationBuilder.DropTable(
                name: "WritingChapter");

            migrationBuilder.DropTable(
                name: "Writing");

            migrationBuilder.CreateTable(
                name: "Chapters",
                columns: table => new
                {
                    ChapterId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ComicId = table.Column<int>(type: "integer", nullable: false),
                    ChapterTitle = table.Column<string>(type: "text", nullable: false),
                    Images = table.Column<string[]>(type: "text[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chapters", x => x.ChapterId);
                    table.ForeignKey(
                        name: "FK_Chapters_Comics_ComicId",
                        column: x => x.ComicId,
                        principalTable: "Comics",
                        principalColumn: "ComicId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chapters_ComicId",
                table: "Chapters",
                column: "ComicId");
        }
    }
}
