using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class UserApprovals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DriversLicenceUrl",
                table: "UserProfiles",
                newName: "DriversLicenceFrontUrl");

            migrationBuilder.RenameColumn(
                name: "DriversLicencePath",
                table: "UserProfiles",
                newName: "DriversLicenceFrontPath");

            migrationBuilder.RenameColumn(
                name: "DriversLicenceUrl",
                table: "UserApprovals",
                newName: "DriversLicenceFrontUrl");

            migrationBuilder.RenameColumn(
                name: "DriversLicencePath",
                table: "UserApprovals",
                newName: "DriversLicenceFrontPath");

            migrationBuilder.AddColumn<string>(
                name: "DriversLicenceBackPath",
                table: "UserProfiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DriversLicenceBackUrl",
                table: "UserProfiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LicenceType",
                table: "UserProfiles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DriversLicenceBackPath",
                table: "UserApprovals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DriversLicenceBackUrl",
                table: "UserApprovals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "LicenceType",
                table: "UserApprovals",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DriversLicenceBackPath",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "DriversLicenceBackUrl",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "LicenceType",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "DriversLicenceBackPath",
                table: "UserApprovals");

            migrationBuilder.DropColumn(
                name: "DriversLicenceBackUrl",
                table: "UserApprovals");

            migrationBuilder.DropColumn(
                name: "LicenceType",
                table: "UserApprovals");

            migrationBuilder.RenameColumn(
                name: "DriversLicenceFrontUrl",
                table: "UserProfiles",
                newName: "DriversLicenceUrl");

            migrationBuilder.RenameColumn(
                name: "DriversLicenceFrontPath",
                table: "UserProfiles",
                newName: "DriversLicencePath");

            migrationBuilder.RenameColumn(
                name: "DriversLicenceFrontUrl",
                table: "UserApprovals",
                newName: "DriversLicenceUrl");

            migrationBuilder.RenameColumn(
                name: "DriversLicenceFrontPath",
                table: "UserApprovals",
                newName: "DriversLicencePath");
        }
    }
}
