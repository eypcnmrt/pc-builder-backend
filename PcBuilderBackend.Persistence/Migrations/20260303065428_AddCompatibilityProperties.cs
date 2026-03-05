using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PcBuilderBackend.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCompatibilityProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SupportedRamType",
                table: "Motherboards",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "LengthMm",
                table: "Gpus",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HeightMm",
                table: "Coolers",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SupportedRamType",
                table: "Motherboards");

            migrationBuilder.DropColumn(
                name: "LengthMm",
                table: "Gpus");

            migrationBuilder.DropColumn(
                name: "HeightMm",
                table: "Coolers");
        }
    }
}
