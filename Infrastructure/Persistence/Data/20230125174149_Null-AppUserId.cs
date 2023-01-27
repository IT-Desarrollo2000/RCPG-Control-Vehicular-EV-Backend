using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class NullAppUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleReports_AspNetUsers_AppUserId",
                table: "VehicleReports",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleReports_AspNetUsers_AppUserId",
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
