using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class Workshops : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MechanicalWorkshop",
                table: "Expenses");

            migrationBuilder.AddColumn<int>(
                name: "VehicleMaintenanceWorkshopId",
                table: "Expenses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_VehicleMaintenanceWorkshopId",
                table: "Expenses",
                column: "VehicleMaintenanceWorkshopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_VehicleMaintenanceWorkshops_VehicleMaintenanceWorkshopId",
                table: "Expenses",
                column: "VehicleMaintenanceWorkshopId",
                principalTable: "VehicleMaintenanceWorkshops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_VehicleMaintenanceWorkshops_VehicleMaintenanceWorkshopId",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_VehicleMaintenanceWorkshopId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "VehicleMaintenanceWorkshopId",
                table: "Expenses");

            migrationBuilder.AddColumn<string>(
                name: "MechanicalWorkshop",
                table: "Expenses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
