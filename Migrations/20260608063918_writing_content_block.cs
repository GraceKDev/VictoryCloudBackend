using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace VictoryCloudApi.Migrations
{
    /// <inheritdoc />
    public partial class writing_content_block : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContentId",
                table: "Comment",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WritingChapterContentBlock",
                columns: table => new
                {
                    WritingChapterContentBlockId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WritingChapterContentId = table.Column<int>(type: "integer", nullable: false),
                    WritingContentBlockContent = table.Column<string>(type: "text", nullable: true),
                    WritingContentBlockImageUrl = table.Column<string>(type: "text", nullable: true),
                    WritingContentBlockAltText = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WritingChapterContentBlock", x => x.WritingChapterContentBlockId);
                    table.ForeignKey(
                        name: "FK_WritingChapterContentBlock_WritingChapterContent_WritingCha~",
                        column: x => x.WritingChapterContentId,
                        principalTable: "WritingChapterContent",
                        principalColumn: "WritingChapterContentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WritingChapterContentBlock_WritingChapterContentId",
                table: "WritingChapterContentBlock",
                column: "WritingChapterContentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WritingChapterContentBlock");

            migrationBuilder.DropColumn(
                name: "ContentId",
                table: "Comment");
        }
    }
}
