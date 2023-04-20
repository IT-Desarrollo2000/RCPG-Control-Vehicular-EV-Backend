using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class PoliciesRevengeance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Policy_Vehicles_VehicleId",
                table: "Policy");

            migrationBuilder.DropIndex(
                name: "IX_Policy_VehicleId",
                table: "Policy");

            migrationBuilder.AddColumn<int>(
                name: "PolicyId",
                table: "Vehicles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrentVehicleId",
                table: "Policy",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Policy_CurrentVehicleId",
                table: "Policy",
                column: "CurrentVehicleId",
                unique: true,
                filter: "[CurrentVehicleId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Policy_VehicleId",
                table: "Policy",
                column: "VehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Policy_Vehicles_CurrentVehicleId",
                table: "Policy",
                column: "CurrentVehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Policy_Vehicles_VehicleId",
                table: "Policy",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Policy_Vehicles_CurrentVehicleId",
                table: "Policy");

            migrationBuilder.DropForeignKey(
                name: "FK_Policy_Vehicles_VehicleId",
                table: "Policy");

            migrationBuilder.DropIndex(
                name: "IX_Policy_CurrentVehicleId",
                table: "Policy");

            migrationBuilder.DropIndex(
                name: "IX_Policy_VehicleId",
                table: "Policy");

            migrationBuilder.DropColumn(
                name: "PolicyId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "CurrentVehicleId",
                table: "Policy");

            migrationBuilder.CreateIndex(
                name: "IX_Policy_VehicleId",
                table: "Policy",
                column: "VehicleId",
                unique: true,
                filter: "[VehicleId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Policy_Vehicles_VehicleId",
                table: "Policy",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
