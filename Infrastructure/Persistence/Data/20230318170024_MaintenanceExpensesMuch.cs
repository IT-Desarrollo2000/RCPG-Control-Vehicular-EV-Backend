using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class MaintenanceExpensesMuch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleMaintenances_Expenses_ExpenseId",
                table: "VehicleMaintenances");

            migrationBuilder.DropIndex(
                name: "IX_VehicleMaintenances_ExpenseId",
                table: "VehicleMaintenances");

            migrationBuilder.AddColumn<int>(
                name: "VehicleMaintenanceId",
                table: "Expenses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_VehicleMaintenanceId",
                table: "Expenses",
                column: "VehicleMaintenanceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_VehicleMaintenances_VehicleMaintenanceId",
                table: "Expenses",
                column: "VehicleMaintenanceId",
                principalTable: "VehicleMaintenances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_VehicleMaintenances_VehicleMaintenanceId",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_VehicleMaintenanceId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "VehicleMaintenanceId",
                table: "Expenses");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleMaintenances_ExpenseId",
                table: "VehicleMaintenances",
                column: "ExpenseId");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleMaintenances_Expenses_ExpenseId",
                table: "VehicleMaintenances",
                column: "ExpenseId",
                principalTable: "Expenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
