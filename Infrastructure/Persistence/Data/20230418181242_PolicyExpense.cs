using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class PolicyExpense : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExpenseId",
                table: "Policy",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PolicyId",
                table: "Expenses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_PolicyId",
                table: "Expenses",
                column: "PolicyId",
                unique: true,
                filter: "[PolicyId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Policy_PolicyId",
                table: "Expenses",
                column: "PolicyId",
                principalTable: "Policy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Policy_PolicyId",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_PolicyId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "ExpenseId",
                table: "Policy");

            migrationBuilder.DropColumn(
                name: "PolicyId",
                table: "Expenses");
        }
    }
}
