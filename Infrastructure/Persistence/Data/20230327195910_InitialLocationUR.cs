using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class InitialLocationUR : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "InitialLatitude",
                table: "VehicleReportUses",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "InitialLongitude",
                table: "VehicleReportUses",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InitialLatitude",
                table: "VehicleReportUses");

            migrationBuilder.DropColumn(
                name: "InitialLongitude",
                table: "VehicleReportUses");
        }
    }
}
