using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class InvoiceConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Expenses_ExpensesId",
                table: "Invoices");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Expenses_ExpensesId",
                table: "Invoices",
                column: "ExpensesId",
                principalTable: "Expenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Expenses_ExpensesId",
                table: "Invoices");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Expenses_ExpensesId",
                table: "Invoices",
                column: "ExpensesId",
                principalTable: "Expenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
