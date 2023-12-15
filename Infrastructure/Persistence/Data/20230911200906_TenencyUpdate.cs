using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class TenencyUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InvoiceFilePath",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InvoiceFileUrl",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PlatePaymentDate",
                table: "Vehicles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TenencyPaymentDate",
                table: "Vehicles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "VehicleTenencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    TenencyPaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TenencyYear = table.Column<int>(type: "int", nullable: false),
                    TenencyCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExpenseId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleTenencies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleTenencies_Expenses_ExpenseId",
                        column: x => x.ExpenseId,
                        principalTable: "Expenses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VehicleTenencies_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleTenencies_ExpenseId",
                table: "VehicleTenencies",
                column: "ExpenseId",
                unique: true,
                filter: "[ExpenseId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleTenencies_VehicleId",
                table: "VehicleTenencies",
                column: "VehicleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehicleTenencies");

            migrationBuilder.DropColumn(
                name: "InvoiceFilePath",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "InvoiceFileUrl",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "PlatePaymentDate",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "TenencyPaymentDate",
                table: "Vehicles");
        }
    }
}
