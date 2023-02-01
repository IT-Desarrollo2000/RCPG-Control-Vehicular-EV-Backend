using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class SupervisorDepartments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUserDepartaments",
                columns: table => new
                {
                    AssignedDepartmentsId = table.Column<int>(type: "int", nullable: false),
                    SupervisorsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserDepartaments", x => new { x.AssignedDepartmentsId, x.SupervisorsId });
                    table.ForeignKey(
                        name: "FK_AppUserDepartaments_AspNetUsers_SupervisorsId",
                        column: x => x.SupervisorsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserDepartaments_Departaments_AssignedDepartmentsId",
                        column: x => x.AssignedDepartmentsId,
                        principalTable: "Departaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserDepartaments_SupervisorsId",
                table: "AppUserDepartaments",
                column: "SupervisorsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserDepartaments");
        }
    }
}
