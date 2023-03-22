using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class FixServiceMtto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleServices_Expenses_ExpenseId",
                table: "VehicleServices");

            migrationBuilder.DropIndex(
                name: "IX_VehicleServices_ExpenseId",
                table: "VehicleServices");

            migrationBuilder.AddColumn<int>(
                name: "VehicleServiceId",
                table: "Expenses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_VehicleServiceId",
                table: "Expenses",
                column: "VehicleServiceId",
                unique: true,
                filter: "[VehicleServiceId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_VehicleServices_VehicleServiceId",
                table: "Expenses",
                column: "VehicleServiceId",
                principalTable: "VehicleServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_VehicleServices_VehicleServiceId",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_VehicleServiceId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "VehicleServiceId",
                table: "Expenses");

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
    }
}
