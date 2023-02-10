using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class ReportFix1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleReports_AspNetUsers_AppUserId",
                table: "VehicleReports");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleReports_UserProfiles_UserProfileId",
                table: "VehicleReports");

            migrationBuilder.DropIndex(
                name: "IX_VehicleReports_AppUserId",
                table: "VehicleReports");

            migrationBuilder.RenameColumn(
                name: "UserProfileId",
                table: "VehicleReports",
                newName: "SolvedByAdminUserId");

            migrationBuilder.RenameColumn(
                name: "GasolineLoad",
                table: "VehicleReports",
                newName: "MobileUserId");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "VehicleReports",
                newName: "GasolineLoadAmount");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleReports_UserProfileId",
                table: "VehicleReports",
                newName: "IX_VehicleReports_SolvedByAdminUserId");

            migrationBuilder.AddColumn<int>(
                name: "AdminUserId",
                table: "VehicleReports",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GasolineCurrentKM",
                table: "VehicleReports",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleReports_AdminUserId",
                table: "VehicleReports",
                column: "AdminUserId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleReports_MobileUserId",
                table: "VehicleReports",
                column: "MobileUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleReports_AspNetUsers_AdminUserId",
                table: "VehicleReports",
                column: "AdminUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleReports_AspNetUsers_SolvedByAdminUserId",
                table: "VehicleReports",
                column: "SolvedByAdminUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleReports_UserProfiles_MobileUserId",
                table: "VehicleReports",
                column: "MobileUserId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleReports_AspNetUsers_AdminUserId",
                table: "VehicleReports");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleReports_AspNetUsers_SolvedByAdminUserId",
                table: "VehicleReports");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleReports_UserProfiles_MobileUserId",
                table: "VehicleReports");

            migrationBuilder.DropIndex(
                name: "IX_VehicleReports_AdminUserId",
                table: "VehicleReports");

            migrationBuilder.DropIndex(
                name: "IX_VehicleReports_MobileUserId",
                table: "VehicleReports");

            migrationBuilder.DropColumn(
                name: "AdminUserId",
                table: "VehicleReports");

            migrationBuilder.DropColumn(
                name: "GasolineCurrentKM",
                table: "VehicleReports");

            migrationBuilder.RenameColumn(
                name: "SolvedByAdminUserId",
                table: "VehicleReports",
                newName: "UserProfileId");

            migrationBuilder.RenameColumn(
                name: "MobileUserId",
                table: "VehicleReports",
                newName: "GasolineLoad");

            migrationBuilder.RenameColumn(
                name: "GasolineLoadAmount",
                table: "VehicleReports",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleReports_SolvedByAdminUserId",
                table: "VehicleReports",
                newName: "IX_VehicleReports_UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleReports_AppUserId",
                table: "VehicleReports",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleReports_AspNetUsers_AppUserId",
                table: "VehicleReports",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleReports_UserProfiles_UserProfileId",
                table: "VehicleReports",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
