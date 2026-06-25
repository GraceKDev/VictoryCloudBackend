using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VictoryCloudApi.Migrations
{
    /// <inheritdoc />
    public partial class writingcontrollerupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoverUrl",
                table: "Writing",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverUrl",
                table: "Writing");
        }
    }
}
