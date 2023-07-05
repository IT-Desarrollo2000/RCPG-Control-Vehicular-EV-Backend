using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class AdditionalInformation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdditionalInformationId",
                table: "Vehicles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AdditionalInformation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleType = table.Column<int>(type: "int", nullable: false),
                    Models = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LWH = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WheelBase = table.Column<int>(type: "int", nullable: true),
                    WheelTrack = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinTurningRadius = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UnladdenMass = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Passenger = table.Column<int>(type: "int", nullable: true),
                    WheelSize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SystemVoltage = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BatteryCapacity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Battery = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotorType = table.Column<int>(type: "int", nullable: false),
                    RatedPower = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IntelligentProtectionOfController = table.Column<bool>(type: "bit", nullable: false),
                    FailureIndicationOfDrivingSystem = table.Column<bool>(type: "bit", nullable: false),
                    PowerOutputMode = table.Column<int>(type: "int", nullable: false),
                    SpeedRatioOrReducer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxSpeed = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MaxCruisingRange = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OperatingCruisingRange = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CrusingRangeWithUsingAC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreventSlipping = table.Column<bool>(type: "bit", nullable: false),
                    PowerConsumption = table.Column<int>(type: "int", nullable: false),
                    ChargeTime = table.Column<int>(type: "int", nullable: false),
                    FrameType = table.Column<int>(type: "int", nullable: false),
                    FoldableRearViewMirror = table.Column<bool>(type: "bit", nullable: false),
                    AluminiumWheel = table.Column<bool>(type: "bit", nullable: false),
                    LEDDigitalInstrument = table.Column<bool>(type: "bit", nullable: false),
                    LEDTailight = table.Column<bool>(type: "bit", nullable: false),
                    LEDHeadlamp = table.Column<bool>(type: "bit", nullable: false),
                    HighMountedBrakeLamp = table.Column<bool>(type: "bit", nullable: false),
                    FrontSuspension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RearSuspension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrakeSystem = table.Column<int>(type: "int", nullable: false),
                    AutomaticSteeringWheelReturn = table.Column<bool>(type: "bit", nullable: false),
                    BrakeBooster = table.Column<bool>(type: "bit", nullable: false),
                    BrakingDistance = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EPS = table.Column<bool>(type: "bit", nullable: false),
                    RotationsOfSteeringWheel = table.Column<int>(type: "int", nullable: true),
                    FullAutomaticAC = table.Column<bool>(type: "bit", nullable: false),
                    Heater = table.Column<bool>(type: "bit", nullable: false),
                    SeatBletWarning = table.Column<bool>(type: "bit", nullable: false),
                    InermittentWindshieldWiper = table.Column<bool>(type: "bit", nullable: false),
                    DigitalReversingRader = table.Column<bool>(type: "bit", nullable: false),
                    AutoInductionHeadlamp = table.Column<bool>(type: "bit", nullable: false),
                    AutoStart = table.Column<bool>(type: "bit", nullable: false),
                    AdjustableSeat = table.Column<bool>(type: "bit", nullable: false),
                    ElectricDoorsWindows = table.Column<bool>(type: "bit", nullable: false),
                    AntiGlareInsideRearViewMirror = table.Column<bool>(type: "bit", nullable: false),
                    ReadingLamp = table.Column<bool>(type: "bit", nullable: false),
                    Instrument = table.Column<int>(type: "int", nullable: false),
                    PowerInterFace12V = table.Column<int>(type: "int", nullable: true),
                    CentralControlSystem = table.Column<bool>(type: "bit", nullable: false),
                    SafetyBelt3Point = table.Column<bool>(type: "bit", nullable: false),
                    CopilotHandle = table.Column<bool>(type: "bit", nullable: false),
                    RearViewCamera = table.Column<bool>(type: "bit", nullable: false),
                    IntelligentChargingSystem = table.Column<bool>(type: "bit", nullable: false),
                    AndroidInch9 = table.Column<bool>(type: "bit", nullable: false),
                    IntelligentVehicleNavigation = table.Column<bool>(type: "bit", nullable: false),
                    ColorfulAmbientLamp = table.Column<bool>(type: "bit", nullable: false),
                    BluetoothTelephone = table.Column<bool>(type: "bit", nullable: false),
                    RadioMP3 = table.Column<bool>(type: "bit", nullable: false),
                    Loudspeaker = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalInformation", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_AdditionalInformationId",
                table: "Vehicles",
                column: "AdditionalInformationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_AdditionalInformation_AdditionalInformationId",
                table: "Vehicles",
                column: "AdditionalInformationId",
                principalTable: "AdditionalInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_AdditionalInformation_AdditionalInformationId",
                table: "Vehicles");

            migrationBuilder.DropTable(
                name: "AdditionalInformation");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_AdditionalInformationId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "AdditionalInformationId",
                table: "Vehicles");
        }
    }
}
