using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastNameP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastNameM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Propietaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SurnameP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SurnameM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsMoralPerson = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Propietaries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypesOfExpenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypesOfExpenses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleAdditionalInfo",
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
                    table.PrimaryKey("PK_VehicleAdditionalInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleMaintenanceWorkshops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ubication = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleMaintenanceWorkshops", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUserSocials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FirebaseUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NetworkType = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserSocials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUserSocials_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JwtId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Departaments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departaments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departaments_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                    table.ForeignKey(
                        name: "FK_States_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SurnameP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SurnameM = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfileImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfileImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfileImageUploadDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
                    CanDriveInHighway = table.Column<bool>(type: "bit", nullable: true),
                    DriversLicenceFrontUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DriversLicenceBackUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DriversLicenceFrontPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DriversLicenceBackPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LicenceValidityYears = table.Column<int>(type: "int", nullable: true),
                    LicenceType = table.Column<int>(type: "int", nullable: true),
                    LicenceExpeditionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LicenceExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfiles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProfiles_Departaments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departaments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Municipalities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipalities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Municipalities_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    DriversLicenceFrontUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DriversLicenceBackUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DriversLicenceFrontPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DriversLicenceBackPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LicenceType = table.Column<int>(type: "int", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Serial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsUtilitary = table.Column<bool>(type: "bit", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModelYear = table.Column<int>(type: "int", nullable: false),
                    ChargeCapacityKwH = table.Column<int>(type: "int", nullable: true),
                    CurrentKM = table.Column<int>(type: "int", nullable: true),
                    InitialKM = table.Column<int>(type: "int", nullable: false),
                    CurrentChargeKwH = table.Column<int>(type: "int", nullable: false),
                    VehicleType = table.Column<int>(type: "int", nullable: false),
                    VehicleStatus = table.Column<int>(type: "int", nullable: false),
                    AdditionalInformationId = table.Column<int>(type: "int", nullable: true),
                    ServicePeriodMonths = table.Column<int>(type: "int", nullable: false),
                    ServicePeriodKM = table.Column<int>(type: "int", nullable: false),
                    OwnershipType = table.Column<int>(type: "int", nullable: false),
                    OwnersName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DesiredPerformance = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    VehicleQRId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehicleObservation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarRegistrationPlate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsClean = table.Column<bool>(type: "bit", nullable: false),
                    PolicyId = table.Column<int>(type: "int", nullable: true),
                    FuelCardNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleResponsibleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotorSerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PropietaryId = table.Column<int>(type: "int", nullable: true),
                    MunicipalityId = table.Column<int>(type: "int", nullable: true),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IVA = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ResponsiveLetter = table.Column<bool>(type: "bit", nullable: true),
                    DuplicateKey = table.Column<bool>(type: "bit", nullable: true),
                    TenencyPaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PlatePaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InvoiceFilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceFileUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_Municipalities_MunicipalityId",
                        column: x => x.MunicipalityId,
                        principalTable: "Municipalities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vehicles_Propietaries_PropietaryId",
                        column: x => x.PropietaryId,
                        principalTable: "Propietaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Vehicles_VehicleAdditionalInfo_AdditionalInformationId",
                        column: x => x.AdditionalInformationId,
                        principalTable: "VehicleAdditionalInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateTable(
                name: "DepartamentsVehicle",
                columns: table => new
                {
                    AssignedDepartmentsId = table.Column<int>(type: "int", nullable: false),
                    AssignedVehiclesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartamentsVehicle", x => new { x.AssignedDepartmentsId, x.AssignedVehiclesId });
                    table.ForeignKey(
                        name: "FK_DepartamentsVehicle_Departaments_AssignedDepartmentsId",
                        column: x => x.AssignedDepartmentsId,
                        principalTable: "Departaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartamentsVehicle_Vehicles_AssignedVehiclesId",
                        column: x => x.AssignedVehiclesId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhotosOfCirculationCard",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotosOfCirculationCard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhotosOfCirculationCard_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Policy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PolicyNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NameCompany = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: true),
                    ExpenseId = table.Column<int>(type: "int", nullable: true),
                    CurrentVehicleId = table.Column<int>(type: "int", nullable: true),
                    PolicyCostValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Policy_Vehicles_CurrentVehicleId",
                        column: x => x.CurrentVehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Policy_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VehicleImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleImages_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehicleServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkShopId = table.Column<int>(type: "int", nullable: true),
                    ServiceUserId = table.Column<int>(type: "int", nullable: true),
                    TypeService = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    NextService = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NextServiceKM = table.Column<int>(type: "int", nullable: true),
                    InitialMileage = table.Column<int>(type: "int", nullable: true),
                    InitialCharge = table.Column<int>(type: "int", nullable: true),
                    FinalMileage = table.Column<int>(type: "int", nullable: true),
                    FinalCharge = table.Column<int>(type: "int", nullable: true),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpenseId = table.Column<int>(type: "int", nullable: true),
                    VehicleMaintenanceWorkshopId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleServices_AspNetUsers_ServiceUserId",
                        column: x => x.ServiceUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehicleServices_VehicleMaintenanceWorkshops_VehicleMaintenanceWorkshopId",
                        column: x => x.VehicleMaintenanceWorkshopId,
                        principalTable: "VehicleMaintenanceWorkshops",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VehicleServices_VehicleMaintenanceWorkshops_WorkShopId",
                        column: x => x.WorkShopId,
                        principalTable: "VehicleMaintenanceWorkshops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehicleServices_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehicleReportUses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    InitialMileage = table.Column<double>(type: "float", nullable: true),
                    FinalMileage = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    StatusReportUse = table.Column<int>(type: "int", nullable: false),
                    Observations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChecklistId = table.Column<int>(type: "int", nullable: true),
                    UseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserProfileId = table.Column<int>(type: "int", nullable: true),
                    AppUserId = table.Column<int>(type: "int", nullable: true),
                    CurrentChargeLoad = table.Column<int>(type: "int", nullable: true),
                    LastChargeLoad = table.Column<int>(type: "int", nullable: true),
                    Verification = table.Column<bool>(type: "bit", nullable: true),
                    InitialCheckListId = table.Column<int>(type: "int", nullable: true),
                    FinishedByDriverId = table.Column<int>(type: "int", nullable: true),
                    InitialLatitude = table.Column<double>(type: "float", nullable: true),
                    InitialLongitude = table.Column<double>(type: "float", nullable: true),
                    FinishedByAdminId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleReportUses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleReportUses_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VehicleReportUses_AspNetUsers_FinishedByAdminId",
                        column: x => x.FinishedByAdminId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehicleReportUses_Checklists_ChecklistId",
                        column: x => x.ChecklistId,
                        principalTable: "Checklists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehicleReportUses_Checklists_InitialCheckListId",
                        column: x => x.InitialCheckListId,
                        principalTable: "Checklists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehicleReportUses_UserProfiles_FinishedByDriverId",
                        column: x => x.FinishedByDriverId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehicleReportUses_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehicleReportUses_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhotosOfPolicy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PolicyId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotosOfPolicy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhotosOfPolicy_Policy_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "Policy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DestinationOfReportUses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DestinationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitud = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    VehicleReportUseId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DestinationOfReportUses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DestinationOfReportUses_VehicleReportUses_VehicleReportUseId",
                        column: x => x.VehicleReportUseId,
                        principalTable: "VehicleReportUses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VehicleReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportType = table.Column<int>(type: "int", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    Commentary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileUserId = table.Column<int>(type: "int", nullable: true),
                    AdminUserId = table.Column<int>(type: "int", nullable: true),
                    ReportDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsResolved = table.Column<bool>(type: "bit", nullable: false),
                    GasolineLoadAmount = table.Column<double>(type: "float", nullable: true),
                    GasolineCurrentKM = table.Column<int>(type: "int", nullable: true),
                    ReportSolutionComment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReportStatus = table.Column<int>(type: "int", nullable: false),
                    VehicleReportUseId = table.Column<int>(type: "int", nullable: true),
                    SolvedByAdminUserId = table.Column<int>(type: "int", nullable: true),
                    AmountGasoline = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleReports_AspNetUsers_AdminUserId",
                        column: x => x.AdminUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VehicleReports_AspNetUsers_SolvedByAdminUserId",
                        column: x => x.SolvedByAdminUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehicleReports_UserProfiles_MobileUserId",
                        column: x => x.MobileUserId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehicleReports_VehicleReportUses_VehicleReportUseId",
                        column: x => x.VehicleReportUseId,
                        principalTable: "VehicleReportUses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VehicleReports_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehicleMaintenances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReasonForMaintenance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaintenanceDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    InitialMileage = table.Column<int>(type: "int", nullable: true),
                    InitialCharge = table.Column<int>(type: "int", nullable: true),
                    FinalMileage = table.Column<int>(type: "int", nullable: true),
                    FinalCharge = table.Column<int>(type: "int", nullable: true),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    WorkShopId = table.Column<int>(type: "int", nullable: true),
                    ApprovedByUserId = table.Column<int>(type: "int", nullable: true),
                    ReportId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleMaintenances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleMaintenances_AspNetUsers_ApprovedByUserId",
                        column: x => x.ApprovedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehicleMaintenances_VehicleMaintenanceWorkshops_WorkShopId",
                        column: x => x.WorkShopId,
                        principalTable: "VehicleMaintenanceWorkshops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehicleMaintenances_VehicleReports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "VehicleReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehicleMaintenances_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehicleReportImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleReportId = table.Column<int>(type: "int", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleReportImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleReportImages_VehicleReports_VehicleReportId",
                        column: x => x.VehicleReportId,
                        principalTable: "VehicleReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypesOfExpensesId = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Invoiced = table.Column<bool>(type: "bit", nullable: false),
                    ExpenseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ERPFolio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehicleReportId = table.Column<int>(type: "int", nullable: true),
                    VehicleMaintenanceWorkshopId = table.Column<int>(type: "int", nullable: true),
                    VehicleMaintenanceId = table.Column<int>(type: "int", nullable: true),
                    VehicleServiceId = table.Column<int>(type: "int", nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: true),
                    PolicyId = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_Departaments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Expenses_Policy_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "Policy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Expenses_TypesOfExpenses_TypesOfExpensesId",
                        column: x => x.TypesOfExpensesId,
                        principalTable: "TypesOfExpenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Expenses_VehicleMaintenanceWorkshops_VehicleMaintenanceWorkshopId",
                        column: x => x.VehicleMaintenanceWorkshopId,
                        principalTable: "VehicleMaintenanceWorkshops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Expenses_VehicleMaintenances_VehicleMaintenanceId",
                        column: x => x.VehicleMaintenanceId,
                        principalTable: "VehicleMaintenances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Expenses_VehicleReports_VehicleReportId",
                        column: x => x.VehicleReportId,
                        principalTable: "VehicleReports",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Expenses_VehicleServices_VehicleServiceId",
                        column: x => x.VehicleServiceId,
                        principalTable: "VehicleServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MaintenanceProgresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleMaintenanceId = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobileUserId = table.Column<int>(type: "int", nullable: true),
                    AdminUserId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceProgresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaintenanceProgresses_AspNetUsers_AdminUserId",
                        column: x => x.AdminUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaintenanceProgresses_UserProfiles_MobileUserId",
                        column: x => x.MobileUserId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaintenanceProgresses_VehicleMaintenances_VehicleMaintenanceId",
                        column: x => x.VehicleMaintenanceId,
                        principalTable: "VehicleMaintenances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpensesVehicle",
                columns: table => new
                {
                    ExpensesId = table.Column<int>(type: "int", nullable: false),
                    VehiclesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpensesVehicle", x => new { x.ExpensesId, x.VehiclesId });
                    table.ForeignKey(
                        name: "FK_ExpensesVehicle_Expenses_ExpensesId",
                        column: x => x.ExpensesId,
                        principalTable: "Expenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpensesVehicle_Vehicles_VehiclesId",
                        column: x => x.VehiclesId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpensesId = table.Column<int>(type: "int", nullable: false),
                    Folio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilePath1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileURL1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilePath2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileURL2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoicedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhotosOfSpendings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpensesId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotosOfSpendings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhotosOfSpendings_Expenses_ExpensesId",
                        column: x => x.ExpensesId,
                        principalTable: "Expenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VehicleTenencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    TenencyPaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TenencyYear = table.Column<int>(type: "int", nullable: false),
                    TenencyCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExpenseId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleTenencies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleTenencies_Expenses_ExpenseId",
                        column: x => x.ExpenseId,
                        principalTable: "Expenses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VehicleTenencies_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MaintenanceProgressImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProgressId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceProgressImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaintenanceProgressImages_MaintenanceProgresses_ProgressId",
                        column: x => x.ProgressId,
                        principalTable: "MaintenanceProgresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserDepartaments_SupervisorsId",
                table: "AppUserDepartaments",
                column: "SupervisorsId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserSocials_UserId",
                table: "AppUserSocials",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Checklists_VehicleId",
                table: "Checklists",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Departaments_CompanyId",
                table: "Departaments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartamentsVehicle_AssignedVehiclesId",
                table: "DepartamentsVehicle",
                column: "AssignedVehiclesId");

            migrationBuilder.CreateIndex(
                name: "IX_DestinationOfReportUses_VehicleReportUseId",
                table: "DestinationOfReportUses",
                column: "VehicleReportUseId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_DepartmentId",
                table: "Expenses",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_PolicyId",
                table: "Expenses",
                column: "PolicyId",
                unique: true,
                filter: "[PolicyId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_TypesOfExpensesId",
                table: "Expenses",
                column: "TypesOfExpensesId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_VehicleMaintenanceId",
                table: "Expenses",
                column: "VehicleMaintenanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_VehicleMaintenanceWorkshopId",
                table: "Expenses",
                column: "VehicleMaintenanceWorkshopId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_VehicleReportId",
                table: "Expenses",
                column: "VehicleReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_VehicleServiceId",
                table: "Expenses",
                column: "VehicleServiceId",
                unique: true,
                filter: "[VehicleServiceId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ExpensesVehicle_VehiclesId",
                table: "ExpensesVehicle",
                column: "VehiclesId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ExpensesId",
                table: "Invoices",
                column: "ExpensesId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceProgresses_AdminUserId",
                table: "MaintenanceProgresses",
                column: "AdminUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceProgresses_MobileUserId",
                table: "MaintenanceProgresses",
                column: "MobileUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceProgresses_VehicleMaintenanceId",
                table: "MaintenanceProgresses",
                column: "VehicleMaintenanceId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceProgressImages_ProgressId",
                table: "MaintenanceProgressImages",
                column: "ProgressId");

            migrationBuilder.CreateIndex(
                name: "IX_Municipalities_StateId",
                table: "Municipalities",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotosOfCirculationCard_VehicleId",
                table: "PhotosOfCirculationCard",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotosOfPolicy_PolicyId",
                table: "PhotosOfPolicy",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotosOfSpendings_ExpensesId",
                table: "PhotosOfSpendings",
                column: "ExpensesId");

            migrationBuilder.CreateIndex(
                name: "IX_Policy_CurrentVehicleId",
                table: "Policy",
                column: "CurrentVehicleId",
                unique: true,
                filter: "[CurrentVehicleId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Policy_VehicleId",
                table: "Policy",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_States_CountryId",
                table: "States",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserApprovals_ProfileId",
                table: "UserApprovals",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_DepartmentId",
                table: "UserProfiles",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_UserId",
                table: "UserProfiles",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleImages_VehicleId",
                table: "VehicleImages",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleMaintenances_ApprovedByUserId",
                table: "VehicleMaintenances",
                column: "ApprovedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleMaintenances_ReportId",
                table: "VehicleMaintenances",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleMaintenances_VehicleId",
                table: "VehicleMaintenances",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleMaintenances_WorkShopId",
                table: "VehicleMaintenances",
                column: "WorkShopId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleReportImages_VehicleReportId",
                table: "VehicleReportImages",
                column: "VehicleReportId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleReports_AdminUserId",
                table: "VehicleReports",
                column: "AdminUserId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleReports_MobileUserId",
                table: "VehicleReports",
                column: "MobileUserId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleReports_SolvedByAdminUserId",
                table: "VehicleReports",
                column: "SolvedByAdminUserId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleReports_VehicleId",
                table: "VehicleReports",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleReports_VehicleReportUseId",
                table: "VehicleReports",
                column: "VehicleReportUseId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleReportUses_AppUserId",
                table: "VehicleReportUses",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleReportUses_ChecklistId",
                table: "VehicleReportUses",
                column: "ChecklistId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleReportUses_FinishedByAdminId",
                table: "VehicleReportUses",
                column: "FinishedByAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleReportUses_FinishedByDriverId",
                table: "VehicleReportUses",
                column: "FinishedByDriverId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleReportUses_InitialCheckListId",
                table: "VehicleReportUses",
                column: "InitialCheckListId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleReportUses_UserProfileId",
                table: "VehicleReportUses",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleReportUses_VehicleId",
                table: "VehicleReportUses",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_AdditionalInformationId",
                table: "Vehicles",
                column: "AdditionalInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_MunicipalityId",
                table: "Vehicles",
                column: "MunicipalityId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_PropietaryId",
                table: "Vehicles",
                column: "PropietaryId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleServices_ServiceUserId",
                table: "VehicleServices",
                column: "ServiceUserId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleServices_VehicleId",
                table: "VehicleServices",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleServices_VehicleMaintenanceWorkshopId",
                table: "VehicleServices",
                column: "VehicleMaintenanceWorkshopId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleServices_WorkShopId",
                table: "VehicleServices",
                column: "WorkShopId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleTenencies_ExpenseId",
                table: "VehicleTenencies",
                column: "ExpenseId",
                unique: true,
                filter: "[ExpenseId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleTenencies_VehicleId",
                table: "VehicleTenencies",
                column: "VehicleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserDepartaments");

            migrationBuilder.DropTable(
                name: "AppUserSocials");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "DepartamentsVehicle");

            migrationBuilder.DropTable(
                name: "DestinationOfReportUses");

            migrationBuilder.DropTable(
                name: "ExpensesVehicle");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "MaintenanceProgressImages");

            migrationBuilder.DropTable(
                name: "PhotosOfCirculationCard");

            migrationBuilder.DropTable(
                name: "PhotosOfPolicy");

            migrationBuilder.DropTable(
                name: "PhotosOfSpendings");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "UserApprovals");

            migrationBuilder.DropTable(
                name: "VehicleImages");

            migrationBuilder.DropTable(
                name: "VehicleReportImages");

            migrationBuilder.DropTable(
                name: "VehicleTenencies");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "MaintenanceProgresses");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "Policy");

            migrationBuilder.DropTable(
                name: "TypesOfExpenses");

            migrationBuilder.DropTable(
                name: "VehicleMaintenances");

            migrationBuilder.DropTable(
                name: "VehicleServices");

            migrationBuilder.DropTable(
                name: "VehicleReports");

            migrationBuilder.DropTable(
                name: "VehicleMaintenanceWorkshops");

            migrationBuilder.DropTable(
                name: "VehicleReportUses");

            migrationBuilder.DropTable(
                name: "Checklists");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Departaments");

            migrationBuilder.DropTable(
                name: "Municipalities");

            migrationBuilder.DropTable(
                name: "Propietaries");

            migrationBuilder.DropTable(
                name: "VehicleAdditionalInfo");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
