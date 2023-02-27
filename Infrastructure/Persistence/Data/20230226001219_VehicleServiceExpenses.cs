using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class VehicleServiceExpenses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExpenseId",
                table: "VehicleServices",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleServices_ExpenseId",
                table: "VehicleServices",
                column: "ExpenseId");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleServices_Expenses_ExpenseId",
                table: "VehicleServices",
                column: "ExpenseId",
                principalTable: "Expenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleServices_Expenses_ExpenseId",
                table: "VehicleServices");

            migrationBuilder.DropIndex(
                name: "IX_VehicleServices_ExpenseId",
                table: "VehicleServices");

            migrationBuilder.DropColumn(
                name: "ExpenseId",
                table: "VehicleServices");
        }
    }
}
