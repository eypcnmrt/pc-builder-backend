using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PcBuilderBackend.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddImageUrlToAllComponents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Storages",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Rams",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Psus",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Processors",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "PcCases",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Motherboards",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Gpus",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Coolers",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Storages");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Rams");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Psus");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Processors");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "PcCases");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Motherboards");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Gpus");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Coolers");
        }
    }
}
