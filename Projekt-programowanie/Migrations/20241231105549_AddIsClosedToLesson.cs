using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjektProgramowanie.Migrations
{
    /// <inheritdoc />
    public partial class AddIsClosedToLesson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsClosed",
                table: "Lessons",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsClosed",
                table: "Lessons");
        }
    }
}
