using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class CONFIGURATIONEXPENSES : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Vehicles_VehicleId",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_VehicleId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "Expenses");


            migrationBuilder.CreateTable(
                name: "ExpensesVehicle",
                columns: table => new
                {
                    ExpensesId = table.Column<int>(type: "int", nullable: false),
                    VehiclesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpensesVehicle", x => new { x.ExpensesId, x.VehiclesId });
                    table.ForeignKey(
                        name: "FK_ExpensesVehicle_Expenses_ExpensesId",
                        column: x => x.ExpensesId,
                        principalTable: "Expenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpensesVehicle_Vehicles_VehiclesId",
                        column: x => x.VehiclesId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpensesVehicle_VehiclesId",
                table: "ExpensesVehicle",
                column: "VehiclesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpensesVehicle");


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
        }
    }
}
