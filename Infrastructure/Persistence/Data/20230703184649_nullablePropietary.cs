using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class nullablePropietary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Propietaries_PropietaryId",
                table: "Vehicles");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Propietaries_PropietaryId",
                table: "Vehicles",
                column: "PropietaryId",
                principalTable: "Propietaries",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Propietaries_PropietaryId",
                table: "Vehicles");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Propietaries_PropietaryId",
                table: "Vehicles",
                column: "PropietaryId",
                principalTable: "Propietaries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
