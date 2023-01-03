using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class Checklist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Checklists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    CirculationCard = table.Column<bool>(type: "bit", nullable: false),
                    CarInsurancePolicy = table.Column<bool>(type: "bit", nullable: false),
                    HydraulicTires = table.Column<bool>(type: "bit", nullable: false),
                    TireRefurmishment = table.Column<bool>(type: "bit", nullable: false),
                    JumperCable = table.Column<bool>(type: "bit", nullable: false),
                    SecurityDice = table.Column<bool>(type: "bit", nullable: false),
                    Extinguisher = table.Column<bool>(type: "bit", nullable: false),
                    CarJack = table.Column<bool>(type: "bit", nullable: false),
                    CarJackKey = table.Column<bool>(type: "bit", nullable: false),
                    ToolBag = table.Column<bool>(type: "bit", nullable: false),
                    SafetyTriangle = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checklists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Checklists_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Checklists_VehicleId",
                table: "Checklists",
                column: "VehicleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Checklists");
        }
    }
}
