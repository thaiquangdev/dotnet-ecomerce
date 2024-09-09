using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace asp_mvc.Migrations
{
    /// <inheritdoc />
    public partial class updatePublisher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PublisherThumb",
                table: "Publishers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublisherThumb",
                table: "Publishers");
        }
    }
}
