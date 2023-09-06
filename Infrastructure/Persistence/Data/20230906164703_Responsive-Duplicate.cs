using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class ResponsiveDuplicate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DuplicateKey",
                table: "Vehicles",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ResponsiveLetter",
                table: "Vehicles",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DuplicateKey",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "ResponsiveLetter",
                table: "Vehicles");
        }
    }
}
