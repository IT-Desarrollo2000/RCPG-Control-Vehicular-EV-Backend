using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class VehicleReportUseWithThings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_VehicleReports_VehicleReportId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleReportImages_VehicleReports_VehicleReportId",
                table: "VehicleReportImages");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleReports_AspNetUsers_AppUserId",
                table: "VehicleReports");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "VehicleReports",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "VehicleReportUseId",
                table: "VehicleReports",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VehicleReportId",
                table: "VehicleReportImages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VehicleReportId",
                table: "Expenses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "VehicleReportUses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    FinalMileage = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    StatusReportUse = table.Column<int>(type: "int", nullable: false),
                    Observations = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChecklistId = table.Column<int>(type: "int", nullable: true),
                    UseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserProfileId = table.Column<int>(type: "int", nullable: true),
                    AppUserId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleReportUses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleReportUses_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VehicleReportUses_Checklists_ChecklistId",
                        column: x => x.ChecklistId,
                        principalTable: "Checklists",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VehicleReportUses_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehicleReportUses_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DestinationOfReportUses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DestinationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitud = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    VehicleReportUseId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DestinationOfReportUses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DestinationOfReportUses_VehicleReportUses_VehicleReportUseId",
                        column: x => x.VehicleReportUseId,
                        principalTable: "VehicleReportUses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleReports_VehicleReportUseId",
                table: "VehicleReports",
                column: "VehicleReportUseId");

            migrationBuilder.CreateIndex(
                name: "IX_DestinationOfReportUses_VehicleReportUseId",
                table: "DestinationOfReportUses",
                column: "VehicleReportUseId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleReportUses_AppUserId",
                table: "VehicleReportUses",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleReportUses_ChecklistId",
                table: "VehicleReportUses",
                column: "ChecklistId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleReportUses_UserProfileId",
                table: "VehicleReportUses",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleReportUses_VehicleId",
                table: "VehicleReportUses",
                column: "VehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_VehicleReports_VehicleReportId",
                table: "Expenses",
                column: "VehicleReportId",
                principalTable: "VehicleReports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleReportImages_VehicleReports_VehicleReportId",
                table: "VehicleReportImages",
                column: "VehicleReportId",
                principalTable: "VehicleReports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleReports_AspNetUsers_AppUserId",
                table: "VehicleReports",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleReports_VehicleReportUses_VehicleReportUseId",
                table: "VehicleReports",
                column: "VehicleReportUseId",
                principalTable: "VehicleReportUses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_VehicleReports_VehicleReportId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleReportImages_VehicleReports_VehicleReportId",
                table: "VehicleReportImages");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleReports_AspNetUsers_AppUserId",
                table: "VehicleReports");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleReports_VehicleReportUses_VehicleReportUseId",
                table: "VehicleReports");

            migrationBuilder.DropTable(
                name: "DestinationOfReportUses");

            migrationBuilder.DropTable(
                name: "VehicleReportUses");

            migrationBuilder.DropIndex(
                name: "IX_VehicleReports_VehicleReportUseId",
                table: "VehicleReports");

            migrationBuilder.DropColumn(
                name: "CurrentFuel",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "VehicleReportUseId",
                table: "VehicleReports");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "VehicleReports",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VehicleReportId",
                table: "VehicleReportImages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "VehicleReportId",
                table: "Expenses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_VehicleReports_VehicleReportId",
                table: "Expenses",
                column: "VehicleReportId",
                principalTable: "VehicleReports",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleReportImages_VehicleReports_VehicleReportId",
                table: "VehicleReportImages",
                column: "VehicleReportId",
                principalTable: "VehicleReports",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleReports_AspNetUsers_AppUserId",
                table: "VehicleReports",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
