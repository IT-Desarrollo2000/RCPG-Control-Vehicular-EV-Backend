using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class WorkshopChanges1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Telephone",
                table: "VehicleMaintenanceWorkshops",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<double>(
                name: "Longitude",
                table: "VehicleMaintenanceWorkshops",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "Latitude",
                table: "VehicleMaintenanceWorkshops",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<int>(
                name: "VehicleReportId",
                table: "Expenses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "VehicleReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportType = table.Column<int>(type: "int", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    Commentary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserProfileId = table.Column<int>(type: "int", nullable: true),
                    AppUserId = table.Column<int>(type: "int", nullable: false),
                    ReportDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsResolved = table.Column<bool>(type: "bit", nullable: false),
                    GasolineLoad = table.Column<int>(type: "int", nullable: true),
                    ReportSolutionComment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReportStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleReports_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VehicleReports_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehicleReports_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehicleReportImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleReportId = table.Column<int>(type: "int", nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleReportImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleReportImages_VehicleReports_VehicleReportId",
                        column: x => x.VehicleReportId,
                        principalTable: "VehicleReports",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_VehicleReportId",
                table: "Expenses",
                column: "VehicleReportId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleReportImages_VehicleReportId",
                table: "VehicleReportImages",
                column: "VehicleReportId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleReports_AppUserId",
                table: "VehicleReports",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleReports_UserProfileId",
                table: "VehicleReports",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleReports_VehicleId",
                table: "VehicleReports",
                column: "VehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_VehicleReports_VehicleReportId",
                table: "Expenses",
                column: "VehicleReportId",
                principalTable: "VehicleReports",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_VehicleReports_VehicleReportId",
                table: "Expenses");

            migrationBuilder.DropTable(
                name: "VehicleReportImages");

            migrationBuilder.DropTable(
                name: "VehicleReports");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_VehicleReportId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "VehicleReportId",
                table: "Expenses");

            migrationBuilder.AlterColumn<string>(
                name: "Telephone",
                table: "VehicleMaintenanceWorkshops",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Longitude",
                table: "VehicleMaintenanceWorkshops",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Latitude",
                table: "VehicleMaintenanceWorkshops",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);
        }
    }
}
