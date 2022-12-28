using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class Companies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DriversLicencePath",
                table: "UserProfiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DriversLicenceUrl",
                table: "UserProfiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "UserProfiles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LicenceExpeditionDate",
                table: "UserProfiles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LicenceExpirationDate",
                table: "UserProfiles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LicenceValidityYears",
                table: "UserProfiles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReasonSocial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserApprovals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DriversLicenceUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DriversLicencePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LicenceValidityYears = table.Column<int>(type: "int", nullable: false),
                    LicenceExpeditionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LicenceExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProfileId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserApprovals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserApprovals_UserProfiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserApprovals_ProfileId",
                table: "UserApprovals",
                column: "ProfileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "UserApprovals");

            migrationBuilder.DropColumn(
                name: "DriversLicencePath",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "DriversLicenceUrl",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "LicenceExpeditionDate",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "LicenceExpirationDate",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "LicenceValidityYears",
                table: "UserProfiles");
        }
    }
}
