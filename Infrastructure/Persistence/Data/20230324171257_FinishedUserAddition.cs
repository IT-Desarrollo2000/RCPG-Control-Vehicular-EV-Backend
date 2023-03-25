using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class FinishedUserAddition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FinishedByAdminId",
                table: "VehicleReportUses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FinishedByDriverId",
                table: "VehicleReportUses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleReportUses_FinishedByAdminId",
                table: "VehicleReportUses",
                column: "FinishedByAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleReportUses_FinishedByDriverId",
                table: "VehicleReportUses",
                column: "FinishedByDriverId");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleReportUses_AspNetUsers_FinishedByAdminId",
                table: "VehicleReportUses",
                column: "FinishedByAdminId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleReportUses_UserProfiles_FinishedByDriverId",
                table: "VehicleReportUses",
                column: "FinishedByDriverId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleReportUses_AspNetUsers_FinishedByAdminId",
                table: "VehicleReportUses");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleReportUses_UserProfiles_FinishedByDriverId",
                table: "VehicleReportUses");

            migrationBuilder.DropIndex(
                name: "IX_VehicleReportUses_FinishedByAdminId",
                table: "VehicleReportUses");

            migrationBuilder.DropIndex(
                name: "IX_VehicleReportUses_FinishedByDriverId",
                table: "VehicleReportUses");

            migrationBuilder.DropColumn(
                name: "FinishedByAdminId",
                table: "VehicleReportUses");

            migrationBuilder.DropColumn(
                name: "FinishedByDriverId",
                table: "VehicleReportUses");
        }
    }
}
