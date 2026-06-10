using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VictoryCloudApi.Migrations
{
    /// <inheritdoc />
    public partial class writing_update2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE \"WritingChapterContent\" DROP COLUMN \"WritingContentPosition\";");
            migrationBuilder.Sql("ALTER TABLE \"WritingChapterContent\" ADD COLUMN \"WritingContentPosition\" integer NOT NULL DEFAULT 0;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE \"WritingChapterContent\" DROP COLUMN \"WritingContentPosition\";");
            migrationBuilder.Sql("ALTER TABLE \"WritingChapterContent\" ADD COLUMN \"WritingContentPosition\" text NOT NULL DEFAULT '';");
        }
    }
}
