using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class Propietary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InvoiceNumber",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PropietaryId",
                table: "Vehicles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Propietaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SurnameP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SurnameM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsMoralPerson = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Propietaries", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_PropietaryId",
                table: "Vehicles",
                column: "PropietaryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Propietaries_PropietaryId",
                table: "Vehicles",
                column: "PropietaryId",
                principalTable: "Propietaries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Propietaries_PropietaryId",
                table: "Vehicles");

            migrationBuilder.DropTable(
                name: "Propietaries");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_PropietaryId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "InvoiceNumber",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "PropietaryId",
                table: "Vehicles");
        }
    }
}
