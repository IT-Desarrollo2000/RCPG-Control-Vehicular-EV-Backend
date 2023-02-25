using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class VehicleServiceUpdate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WhereService",
                table: "VehicleServices");

            migrationBuilder.RenameColumn(
                name: "CarryPerson",
                table: "VehicleServices",
                newName: "Comment");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NextService",
                table: "VehicleServices",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "FinalFuel",
                table: "VehicleServices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FinalMileage",
                table: "VehicleServices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InitialFuel",
                table: "VehicleServices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InitialMileage",
                table: "VehicleServices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NextServiceKM",
                table: "VehicleServices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ServiceUserId",
                table: "VehicleServices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "VehicleServices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VehicleMaintenanceWorkshopId",
                table: "VehicleServices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkShopId",
                table: "VehicleServices",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleServices_ServiceUserId",
                table: "VehicleServices",
                column: "ServiceUserId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleServices_VehicleMaintenanceWorkshopId",
                table: "VehicleServices",
                column: "VehicleMaintenanceWorkshopId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleServices_WorkShopId",
                table: "VehicleServices",
                column: "WorkShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleServices_AspNetUsers_ServiceUserId",
                table: "VehicleServices",
                column: "ServiceUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleServices_VehicleMaintenanceWorkshops_VehicleMaintenanceWorkshopId",
                table: "VehicleServices",
                column: "VehicleMaintenanceWorkshopId",
                principalTable: "VehicleMaintenanceWorkshops",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleServices_VehicleMaintenanceWorkshops_WorkShopId",
                table: "VehicleServices",
                column: "WorkShopId",
                principalTable: "VehicleMaintenanceWorkshops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleServices_AspNetUsers_ServiceUserId",
                table: "VehicleServices");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleServices_VehicleMaintenanceWorkshops_VehicleMaintenanceWorkshopId",
                table: "VehicleServices");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleServices_VehicleMaintenanceWorkshops_WorkShopId",
                table: "VehicleServices");

            migrationBuilder.DropIndex(
                name: "IX_VehicleServices_ServiceUserId",
                table: "VehicleServices");

            migrationBuilder.DropIndex(
                name: "IX_VehicleServices_VehicleMaintenanceWorkshopId",
                table: "VehicleServices");

            migrationBuilder.DropIndex(
                name: "IX_VehicleServices_WorkShopId",
                table: "VehicleServices");

            migrationBuilder.DropColumn(
                name: "FinalFuel",
                table: "VehicleServices");

            migrationBuilder.DropColumn(
                name: "FinalMileage",
                table: "VehicleServices");

            migrationBuilder.DropColumn(
                name: "InitialFuel",
                table: "VehicleServices");

            migrationBuilder.DropColumn(
                name: "InitialMileage",
                table: "VehicleServices");

            migrationBuilder.DropColumn(
                name: "NextServiceKM",
                table: "VehicleServices");

            migrationBuilder.DropColumn(
                name: "ServiceUserId",
                table: "VehicleServices");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "VehicleServices");

            migrationBuilder.DropColumn(
                name: "VehicleMaintenanceWorkshopId",
                table: "VehicleServices");

            migrationBuilder.DropColumn(
                name: "WorkShopId",
                table: "VehicleServices");

            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "VehicleServices",
                newName: "CarryPerson");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NextService",
                table: "VehicleServices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WhereService",
                table: "VehicleServices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
