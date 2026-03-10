using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PcBuilderBackend.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddProcessorDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Architecture",
                table: "Processors",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IntegratedGraphics",
                table: "Processors",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "L3Cache",
                table: "Processors",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MemoryType",
                table: "Processors",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Processors",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Series",
                table: "Processors",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Architecture",
                table: "Processors");

            migrationBuilder.DropColumn(
                name: "IntegratedGraphics",
                table: "Processors");

            migrationBuilder.DropColumn(
                name: "L3Cache",
                table: "Processors");

            migrationBuilder.DropColumn(
                name: "MemoryType",
                table: "Processors");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Processors");

            migrationBuilder.DropColumn(
                name: "Series",
                table: "Processors");
        }
    }
}
