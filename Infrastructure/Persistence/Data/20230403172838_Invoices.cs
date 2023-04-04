using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class Invoices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpensesId = table.Column<int>(type: "int", nullable: false),
                    Folio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileURL1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileURL2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoicedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_Expenses_ExpensesId",
                        column: x => x.ExpensesId,
                        principalTable: "Expenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ExpensesId",
                table: "Invoices",
                column: "ExpensesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invoices");
        }
    }
}
