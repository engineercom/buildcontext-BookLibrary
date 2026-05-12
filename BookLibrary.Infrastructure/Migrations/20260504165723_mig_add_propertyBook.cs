using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookLibrary.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig_add_propertyBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Authors",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GoogleId",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubTitle",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Authors",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "GoogleId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "SubTitle",
                table: "Books");
        }
    }
}
