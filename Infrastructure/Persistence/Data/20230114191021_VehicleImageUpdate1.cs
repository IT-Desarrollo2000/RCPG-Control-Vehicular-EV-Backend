using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class VehicleImageUpdate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleMaintenanceWorkshops_VehicleMaintenances_VehicleMaintenanceId",
                table: "VehicleMaintenanceWorkshops");

            migrationBuilder.AlterColumn<int>(
                name: "VehicleMaintenanceId",
                table: "VehicleMaintenanceWorkshops",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "DepartamentsVehicle",
                columns: table => new
                {
                    AssignedDepartmentsId = table.Column<int>(type: "int", nullable: false),
                    AssignedVehiclesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartamentsVehicle", x => new { x.AssignedDepartmentsId, x.AssignedVehiclesId });
                    table.ForeignKey(
                        name: "FK_DepartamentsVehicle_Departaments_AssignedDepartmentsId",
                        column: x => x.AssignedDepartmentsId,
                        principalTable: "Departaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartamentsVehicle_Vehicles_AssignedVehiclesId",
                        column: x => x.AssignedVehiclesId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepartamentsVehicle_AssignedVehiclesId",
                table: "DepartamentsVehicle",
                column: "AssignedVehiclesId");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleMaintenanceWorkshops_VehicleMaintenances_VehicleMaintenanceId",
                table: "VehicleMaintenanceWorkshops",
                column: "VehicleMaintenanceId",
                principalTable: "VehicleMaintenances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleMaintenanceWorkshops_VehicleMaintenances_VehicleMaintenanceId",
                table: "VehicleMaintenanceWorkshops");

            migrationBuilder.DropTable(
                name: "DepartamentsVehicle");

            migrationBuilder.AlterColumn<int>(
                name: "VehicleMaintenanceId",
                table: "VehicleMaintenanceWorkshops",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleMaintenanceWorkshops_VehicleMaintenances_VehicleMaintenanceId",
                table: "VehicleMaintenanceWorkshops",
                column: "VehicleMaintenanceId",
                principalTable: "VehicleMaintenances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
