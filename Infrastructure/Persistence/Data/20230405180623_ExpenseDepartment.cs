using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class ExpenseDepartment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Expenses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_DepartmentId",
                table: "Expenses",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Departaments_DepartmentId",
                table: "Expenses",
                column: "DepartmentId",
                principalTable: "Departaments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Departaments_DepartmentId",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_DepartmentId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Expenses");
        }
    }
}
