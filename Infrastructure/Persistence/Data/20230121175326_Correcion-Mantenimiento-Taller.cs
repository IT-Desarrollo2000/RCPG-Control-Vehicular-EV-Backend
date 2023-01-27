using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class CorrecionMantenimientoTaller : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleMaintenanceWorkshops_VehicleMaintenances_VehicleMaintenanceId",
                table: "VehicleMaintenanceWorkshops");

            migrationBuilder.DropIndex(
                name: "IX_VehicleMaintenanceWorkshops_VehicleMaintenanceId",
                table: "VehicleMaintenanceWorkshops");

            migrationBuilder.DropColumn(
                name: "VehicleMaintenanceId",
                table: "VehicleMaintenanceWorkshops");

            migrationBuilder.AddColumn<int>(
                name: "VehicleMaintenanceWorkshopId",
                table: "VehicleMaintenances",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleMaintenances_VehicleMaintenanceWorkshopId",
                table: "VehicleMaintenances",
                column: "VehicleMaintenanceWorkshopId");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleMaintenances_VehicleMaintenanceWorkshops_VehicleMaintenanceWorkshopId",
                table: "VehicleMaintenances",
                column: "VehicleMaintenanceWorkshopId",
                principalTable: "VehicleMaintenanceWorkshops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleMaintenances_VehicleMaintenanceWorkshops_VehicleMaintenanceWorkshopId",
                table: "VehicleMaintenances");

            migrationBuilder.DropIndex(
                name: "IX_VehicleMaintenances_VehicleMaintenanceWorkshopId",
                table: "VehicleMaintenances");

            migrationBuilder.DropColumn(
                name: "VehicleMaintenanceWorkshopId",
                table: "VehicleMaintenances");

            migrationBuilder.AddColumn<int>(
                name: "VehicleMaintenanceId",
                table: "VehicleMaintenanceWorkshops",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleMaintenanceWorkshops_VehicleMaintenanceId",
                table: "VehicleMaintenanceWorkshops",
                column: "VehicleMaintenanceId");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleMaintenanceWorkshops_VehicleMaintenances_VehicleMaintenanceId",
                table: "VehicleMaintenanceWorkshops",
                column: "VehicleMaintenanceId",
                principalTable: "VehicleMaintenances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
