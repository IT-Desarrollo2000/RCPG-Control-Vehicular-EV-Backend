using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class maintenanceExpenses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExpenseId",
                table: "VehicleMaintenances",
                type: "int",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleMaintenances_Expenses_ExpenseId",
                table: "VehicleMaintenances");

            migrationBuilder.DropIndex(
                name: "IX_VehicleMaintenances_ExpenseId",
                table: "VehicleMaintenances");

            migrationBuilder.DropColumn(
                name: "ExpenseId",
                table: "VehicleMaintenances");
        }
    }
}
