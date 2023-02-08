using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class ChecklistFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleReportUses_Checklists_ChecklistId",
                table: "VehicleReportUses");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleReportUses_Checklists_ChecklistId",
                table: "VehicleReportUses",
                column: "ChecklistId",
                principalTable: "Checklists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleReportUses_Checklists_ChecklistId",
                table: "VehicleReportUses");

            migrationBuilder.DropTable(
                name: "ExpensesVehicle");

            migrationBuilder.DropColumn(
                name: "Invoiced",
                table: "Expenses");

            migrationBuilder.AddColumn<int>(
                name: "VehicleId",
                table: "Expenses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_VehicleId",
                table: "Expenses",
                column: "VehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Vehicles_VehicleId",
                table: "Expenses",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleReportUses_Checklists_ChecklistId",
                table: "VehicleReportUses",
                column: "ChecklistId",
                principalTable: "Checklists",
                principalColumn: "Id");
        }
    }
}
