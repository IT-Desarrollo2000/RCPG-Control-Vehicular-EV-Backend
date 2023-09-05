using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Filters
{
    public class AdditionalInformationFilter
    {
        public DateTime? CreatedAfterDate { get; set; }
        public DateTime? CreatedBeforeDate { get; set; }
        public int? VehicleId { get; set; }
        public VehicleTypeAdditionalInformation? VehicleType { get; set; }
        public string? Models { get; set; }
        public string? LWH { get; set; }
        public int? WheelBase { get; set; }
        public string? WheelTrack { get; set; }
        public Decimal? MinTurningRadius { get; set; }
        public Decimal? UnladdenMass { get; set; }
        public int? Passenger { get; set; }
        public string? WheelSize { get; set; }
        public Decimal? SystemVoltage { get; set; }
        public Decimal? BatteryCapacity { get; set; }
        public string? Battery { get; set; }
        public MotorType? MotorType { get; set; }
        public Decimal? RatedPower { get; set; }
        public bool? IntelligentProtectionOfController { get; set; }
        public bool? FailureIndicationOfDrivingSystem { get; set; }
        public PowerOutputMode? PowerOutputMode { get; set; }
        public string? SpeedRatioOrReducer { get; set; }
        public Decimal? MaxSpeed { get; set; }
        public Decimal? MaxCruisingRange { get; set; }
        public Decimal? OperatingCruisingRange { get; set; }
        public string? CrusingRangeWithUsingAC { get; set; }
        public bool? PreventSlipping { get; set; }
        public PowerConsumption? PowerConsumption { get; set; }
        public ChargeTime? ChargeTime { get; set; }
        public FrameType? FrameType { get; set; }
        public bool? FoldableRearViewMirror { get; set; }
        public bool? AluminiumWheel { get; set; }
        public bool? LEDDigitalInstrument { get; set; }
        public bool? LEDTailight { get; set; }
        public bool? LEDHeadlamp { get; set; }
        public bool? HighMountedBrakeLamp { get; set; }
        public string? FrontSuspension { get; set; }
        public string? RearSuspension { get; set; }
        public BrakeSystem? BrakeSystem { get; set; }
        public bool? AutomaticSteeringWheelReturn { get; set; }
        public bool? BrakeBooster { get; set; }
        public Decimal? BrakingDistance { get; set; }
        public bool? EPS { get; set; }
        public int? RotationsOfSteeringWheel { get; set; }
        public bool? FullAutomaticAC { get; set; }
        public bool? Heater { get; set; }
        public bool? SeatBletWarning { get; set; }
        public bool? InermittentWindshieldWiper { get; set; }
        public bool? DigitalReversingRader { get; set; }
        public bool? AutoInductionHeadlamp { get; set; }
        public bool? AutoStart { get; set; }
        public bool? AdjustableSeat { get; set; }
        public bool? ElectricDoorsWindows { get; set; }
        public bool? AntiGlareInsideRearViewMirror { get; set; }
        public bool? ReadingLamp { get; set; }
        public Instrument? Instrument { get; set; }
        public int? PowerInterFace12V { get; set; }
        public bool? CentralControlSystem { get; set; }
        public bool? SafetyBelt3Point { get; set; }
        public bool? CopilotHandle { get; set; }
        public bool? RearViewCamera { get; set; }
        public bool? IntelligentChargingSystem { get; set; }
        public bool? AndroidInch9 { get; set; }
        public bool? IntelligentVehicleNavigation { get; set; }
        public bool? ColorfulAmbientLamp { get; set; }
        public bool? BluetoothTelephone { get; set; }
        public bool? RadioMP3 { get; set; }
        public int? Loudspeaker { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
