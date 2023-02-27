using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class MaintenanceUpdate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleMaintenances_VehicleMaintenanceWorkshops_VehicleMaintenanceWorkshopId",
                table: "VehicleMaintenances");

            migrationBuilder.DropColumn(
                name: "CarryPerson",
                table: "VehicleMaintenances");

            migrationBuilder.RenameColumn(
                name: "WhereServiceMaintenance",
                table: "VehicleMaintenances",
                newName: "ReasonForMaintenance");

            migrationBuilder.RenameColumn(
                name: "VehicleMaintenanceWorkshopId",
                table: "VehicleMaintenances",
                newName: "WorkShopId");

            migrationBuilder.RenameColumn(
                name: "NextServiceMaintenance",
                table: "VehicleMaintenances",
                newName: "MaintenanceDate");

            migrationBuilder.RenameColumn(
                name: "CauseServiceMaintenance",
                table: "VehicleMaintenances",
                newName: "Comment");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleMaintenances_VehicleMaintenanceWorkshopId",
                table: "VehicleMaintenances",
                newName: "IX_VehicleMaintenances_WorkShopId");

            migrationBuilder.AddColumn<int>(
                name: "ApprovedByUserId",
                table: "VehicleMaintenances",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FinalFuel",
                table: "VehicleMaintenances",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FinalMileage",
                table: "VehicleMaintenances",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InitialFuel",
                table: "VehicleMaintenances",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InitialMileage",
                table: "VehicleMaintenances",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReportId",
                table: "VehicleMaintenances",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "VehicleMaintenances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleMaintenances_ApprovedByUserId",
                table: "VehicleMaintenances",
                column: "ApprovedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleMaintenances_ReportId",
                table: "VehicleMaintenances",
                column: "ReportId");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleMaintenances_AspNetUsers_ApprovedByUserId",
                table: "VehicleMaintenances",
                column: "ApprovedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleMaintenances_VehicleMaintenanceWorkshops_WorkShopId",
                table: "VehicleMaintenances",
                column: "WorkShopId",
                principalTable: "VehicleMaintenanceWorkshops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleMaintenances_VehicleReports_ReportId",
                table: "VehicleMaintenances",
                column: "ReportId",
                principalTable: "VehicleReports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleMaintenances_AspNetUsers_ApprovedByUserId",
                table: "VehicleMaintenances");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleMaintenances_VehicleMaintenanceWorkshops_WorkShopId",
                table: "VehicleMaintenances");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleMaintenances_VehicleReports_ReportId",
                table: "VehicleMaintenances");

            migrationBuilder.DropIndex(
                name: "IX_VehicleMaintenances_ApprovedByUserId",
                table: "VehicleMaintenances");

            migrationBuilder.DropIndex(
                name: "IX_VehicleMaintenances_ReportId",
                table: "VehicleMaintenances");

            migrationBuilder.DropColumn(
                name: "ApprovedByUserId",
                table: "VehicleMaintenances");

            migrationBuilder.DropColumn(
                name: "FinalFuel",
                table: "VehicleMaintenances");

            migrationBuilder.DropColumn(
                name: "FinalMileage",
                table: "VehicleMaintenances");

            migrationBuilder.DropColumn(
                name: "InitialFuel",
                table: "VehicleMaintenances");

            migrationBuilder.DropColumn(
                name: "InitialMileage",
                table: "VehicleMaintenances");

            migrationBuilder.DropColumn(
                name: "ReportId",
                table: "VehicleMaintenances");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "VehicleMaintenances");

            migrationBuilder.RenameColumn(
                name: "WorkShopId",
                table: "VehicleMaintenances",
                newName: "VehicleMaintenanceWorkshopId");

            migrationBuilder.RenameColumn(
                name: "ReasonForMaintenance",
                table: "VehicleMaintenances",
                newName: "WhereServiceMaintenance");

            migrationBuilder.RenameColumn(
                name: "MaintenanceDate",
                table: "VehicleMaintenances",
                newName: "NextServiceMaintenance");

            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "VehicleMaintenances",
                newName: "CauseServiceMaintenance");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleMaintenances_WorkShopId",
                table: "VehicleMaintenances",
                newName: "IX_VehicleMaintenances_VehicleMaintenanceWorkshopId");

            migrationBuilder.AddColumn<string>(
                name: "CarryPerson",
                table: "VehicleMaintenances",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleMaintenances_VehicleMaintenanceWorkshops_VehicleMaintenanceWorkshopId",
                table: "VehicleMaintenances",
                column: "VehicleMaintenanceWorkshopId",
                principalTable: "VehicleMaintenanceWorkshops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
