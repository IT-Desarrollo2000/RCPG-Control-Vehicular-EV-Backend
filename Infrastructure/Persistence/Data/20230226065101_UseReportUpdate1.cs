using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class UseReportUpdate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InitialCheckListId",
                table: "VehicleReportUses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleReportUses_InitialCheckListId",
                table: "VehicleReportUses",
                column: "InitialCheckListId");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleReportUses_Checklists_InitialCheckListId",
                table: "VehicleReportUses",
                column: "InitialCheckListId",
                principalTable: "Checklists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleReportUses_Checklists_InitialCheckListId",
                table: "VehicleReportUses");

            migrationBuilder.DropIndex(
                name: "IX_VehicleReportUses_InitialCheckListId",
                table: "VehicleReportUses");

            migrationBuilder.DropColumn(
                name: "InitialCheckListId",
                table: "VehicleReportUses");
        }
    }
}
