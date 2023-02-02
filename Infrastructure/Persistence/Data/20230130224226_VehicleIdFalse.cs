using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class VehicleIdFalse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_VehicleReports_VehicleReportId",
                table: "Expenses");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_VehicleReports_VehicleReportId",
                table: "Expenses");

            migrationBuilder.AlterColumn<int>(
                name: "VehicleReportId",
                table: "Expenses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_VehicleReports_VehicleReportId",
                table: "Expenses",
                column: "VehicleReportId",
                principalTable: "VehicleReports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
