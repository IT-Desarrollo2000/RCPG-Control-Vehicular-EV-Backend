using Application.Extensions;
using Application.Interfaces;
using AutoMapper;
using Domain.CustomEntities;
using Domain.DTOs.Filters;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Registered_Cars;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AdditionalInformationServices : IAdditionalInformationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly PaginationOptions _paginationOptions;

        public AdditionalInformationServices(IUnitOfWork unitOfWork, IMapper mapper, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _paginationOptions = options.Value;
        }


        public async Task<PagedList<AdditionalInformation>> GetAdditionalInformation(AdditionalInformationFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            string properties = "";
            IEnumerable<AdditionalInformation> additionalInformation = null;
            Expression<Func<AdditionalInformation, bool>> Query = null;

            if (filter.CreatedAfterDate.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.CreatedDate >= filter.CreatedAfterDate.Value);
                }
                else { Query = p => p.CreatedDate >= filter.CreatedAfterDate.Value; }
            }

            if (filter.CreatedBeforeDate.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.CreatedDate <= filter.CreatedBeforeDate.Value);
                }
                else { Query = p => p.CreatedDate <= filter.CreatedBeforeDate.Value; }
            }

            if (filter.VehicleType.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.VehicleType == filter.VehicleType.Value);
                }
                else { Query = p => p.VehicleType == filter.VehicleType.Value; }
            }


            if (!string.IsNullOrEmpty(filter.Models))
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.Models.Contains(filter.Models));
                }
                else { Query = p => p.Models.Contains(filter.Models); }
            }

            if (!string.IsNullOrEmpty(filter.LWH))
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.LWH.Contains(filter.LWH));
                }
                else { Query = p => p.LWH.Contains(filter.LWH); }
            }

            if (filter.WheelBase.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.WheelBase == filter.WheelBase.Value);
                }
                else { Query = p => p.WheelBase == filter.WheelBase.Value; }
            }

            if (!string.IsNullOrEmpty(filter.WheelTrack))
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.WheelTrack.Contains(filter.WheelTrack));
                }
                else { Query = p => p.WheelTrack.Contains(filter.WheelTrack); }
            }

            if (filter.MinTurningRadius.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.MinTurningRadius == filter.MinTurningRadius.Value);
                }
                else { Query = p => p.MinTurningRadius == filter.MinTurningRadius.Value; }
            }

            if (filter.UnladdenMass.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.UnladdenMass == filter.UnladdenMass.Value);
                }
                else { Query = p => p.UnladdenMass == filter.UnladdenMass.Value; }
            }

            if (filter.Passenger.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.Passenger == filter.Passenger.Value);
                }
                else { Query = p => p.Passenger == filter.Passenger.Value; }
            }

            if (!string.IsNullOrEmpty(filter.WheelSize))
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.WheelSize.Contains(filter.WheelSize));
                }
                else { Query = p => p.WheelSize.Contains(filter.WheelSize); }
            }

            if (filter.SystemVoltage.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.SystemVoltage == filter.SystemVoltage.Value);
                }
                else { Query = p => p.SystemVoltage == filter.SystemVoltage.Value; }
            }

            if (filter.BatteryCapacity.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.BatteryCapacity == filter.BatteryCapacity.Value);
                }
                else { Query = p => p.BatteryCapacity == filter.BatteryCapacity.Value; }
            }

            if (!string.IsNullOrEmpty(filter.Battery))
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.Battery.Contains(filter.Battery));
                }
                else { Query = p => p.Battery.Contains(filter.Battery); }
            }

            if (filter.MotorType.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.MotorType == filter.MotorType.Value);
                }
                else { Query = p => p.MotorType == filter.MotorType.Value; }
            }

            if (filter.RatedPower.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.RatedPower == filter.RatedPower.Value);
                }
                else { Query = p => p.RatedPower == filter.RatedPower.Value; }
            }

            if (filter.IntelligentProtectionOfController.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.IntelligentProtectionOfController == filter.IntelligentProtectionOfController.Value);
                }
                else { Query = p => p.IntelligentProtectionOfController == filter.IntelligentProtectionOfController.Value; }
            }

            if (filter.FailureIndicationOfDrivingSystem.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.FailureIndicationOfDrivingSystem == filter.FailureIndicationOfDrivingSystem.Value);
                }
                else { Query = p => p.FailureIndicationOfDrivingSystem == filter.FailureIndicationOfDrivingSystem.Value; }
            }


            if (filter.PowerOutputMode.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.PowerOutputMode == filter.PowerOutputMode.Value);
                }
                else { Query = p => p.PowerOutputMode == filter.PowerOutputMode.Value; }
            }

            if (!string.IsNullOrEmpty(filter.SpeedRatioOrReducer))
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.SpeedRatioOrReducer.Contains(filter.SpeedRatioOrReducer));
                }
                else { Query = p => p.SpeedRatioOrReducer.Contains(filter.SpeedRatioOrReducer); }
            }

            if (filter.MaxSpeed.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.MaxSpeed == filter.MaxSpeed.Value);
                }
                else { Query = p => p.MaxSpeed == filter.MaxSpeed.Value; }
            }

            if (filter.MaxCruisingRange.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.MaxCruisingRange == filter.MaxCruisingRange.Value);
                }
                else { Query = p => p.MaxCruisingRange == filter.MaxCruisingRange.Value; }
            }


            if (filter.OperatingCruisingRange.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.OperatingCruisingRange == filter.OperatingCruisingRange.Value);
                }
                else { Query = p => p.OperatingCruisingRange == filter.OperatingCruisingRange.Value; }
            }

            if (!string.IsNullOrEmpty(filter.CrusingRangeWithUsingAC))
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.CrusingRangeWithUsingAC.Contains(filter.CrusingRangeWithUsingAC));
                }
                else { Query = p => p.CrusingRangeWithUsingAC.Contains(filter.CrusingRangeWithUsingAC); }
            }

            if (filter.PreventSlipping.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.PreventSlipping == filter.PreventSlipping.Value);
                }
                else { Query = p => p.PreventSlipping == filter.PreventSlipping.Value; }
            }

            if (filter.FailureIndicationOfDrivingSystem.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.PowerConsumption == filter.PowerConsumption.Value);
                }
                else { Query = p => p.PowerConsumption == filter.PowerConsumption.Value; }
            }


            if (filter.ChargeTime.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.ChargeTime == filter.ChargeTime.Value);
                }
                else { Query = p => p.ChargeTime == filter.ChargeTime.Value; }
            }

            if (filter.FrameType.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.FrameType == filter.FrameType.Value);
                }
                else { Query = p => p.FrameType == filter.FrameType.Value; }
            }

            if (filter.FoldableRearViewMirror.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.FoldableRearViewMirror == filter.FoldableRearViewMirror.Value);
                }
                else { Query = p => p.FoldableRearViewMirror == filter.FoldableRearViewMirror.Value; }
            }


            if (filter.AluminiumWheel.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.AluminiumWheel == filter.AluminiumWheel.Value);
                }
                else { Query = p => p.AluminiumWheel == filter.AluminiumWheel.Value; }
            }

            if (filter.LEDDigitalInstrument.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.FrameType == filter.FrameType.Value);
                }
                else { Query = p => p.FrameType == filter.FrameType.Value; }
            }

            if (filter.LEDTailight.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.FoldableRearViewMirror == filter.FoldableRearViewMirror.Value);
                }
                else { Query = p => p.FoldableRearViewMirror == filter.FoldableRearViewMirror.Value; }
            }

            if (filter.LEDHeadlamp.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.AluminiumWheel == filter.AluminiumWheel.Value);
                }
                else { Query = p => p.AluminiumWheel == filter.AluminiumWheel.Value; }
            }

            if (filter.HighMountedBrakeLamp.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.HighMountedBrakeLamp == filter.HighMountedBrakeLamp.Value);
                }
                else { Query = p => p.HighMountedBrakeLamp == filter.HighMountedBrakeLamp.Value; }
            }

            if (!string.IsNullOrEmpty(filter.FrontSuspension))
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.FrontSuspension.Contains(filter.FrontSuspension));
                }
                else { Query = p => p.FrontSuspension.Contains(filter.FrontSuspension); }
            }

            if (!string.IsNullOrEmpty(filter.RearSuspension))
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.RearSuspension.Contains(filter.RearSuspension));
                }
                else { Query = p => p.RearSuspension.Contains(filter.RearSuspension); }
            }

            if (filter.BrakeSystem.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.BrakeSystem == filter.BrakeSystem.Value);
                }
                else { Query = p => p.BrakeSystem == filter.BrakeSystem.Value; }
            }


            if (filter.AutomaticSteeringWheelReturn.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.AutomaticSteeringWheelReturn == filter.AutomaticSteeringWheelReturn.Value);
                }
                else { Query = p => p.AutomaticSteeringWheelReturn == filter.AutomaticSteeringWheelReturn.Value; }
            }


            if (filter.BrakeBooster.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.BrakeBooster == filter.BrakeBooster.Value);
                }
                else { Query = p => p.BrakeBooster == filter.BrakeBooster.Value; }
            }


            if (filter.BrakingDistance.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.BrakingDistance == filter.BrakingDistance.Value);
                }
                else { Query = p => p.BrakingDistance == filter.BrakingDistance.Value; }
            }

            if (filter.EPS.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.EPS == filter.EPS.Value);
                }
                else { Query = p => p.EPS == filter.EPS.Value; }
            }


            if (filter.RotationsOfSteeringWheel.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.RotationsOfSteeringWheel == filter.RotationsOfSteeringWheel.Value);
                }
                else { Query = p => p.RotationsOfSteeringWheel == filter.RotationsOfSteeringWheel.Value; }
            }

            if (filter.FullAutomaticAC.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.FullAutomaticAC == filter.FullAutomaticAC.Value);
                }
                else { Query = p => p.FullAutomaticAC == filter.FullAutomaticAC.Value; }
            }


            if (filter.Heater.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.Heater == filter.Heater.Value);
                }
                else { Query = p => p.Heater == filter.Heater.Value; }
            }

            if (filter.SeatBletWarning.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.SeatBletWarning == filter.SeatBletWarning.Value);
                }
                else { Query = p => p.SeatBletWarning == filter.SeatBletWarning.Value; }
            }


            if (filter.InermittentWindshieldWiper.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.InermittentWindshieldWiper == filter.InermittentWindshieldWiper.Value);
                }
                else { Query = p => p.InermittentWindshieldWiper == filter.InermittentWindshieldWiper.Value; }
            }

            if (filter.DigitalReversingRader.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.DigitalReversingRader == filter.DigitalReversingRader.Value);
                }
                else { Query = p => p.DigitalReversingRader == filter.DigitalReversingRader.Value; }
            }

            if (filter.AutoInductionHeadlamp.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.AutoInductionHeadlamp == filter.AutoInductionHeadlamp.Value);
                }
                else { Query = p => p.AutoInductionHeadlamp == filter.AutoInductionHeadlamp.Value; }
            }

            if (filter.AutoStart.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.AutoStart == filter.AutoStart.Value);
                }
                else { Query = p => p.AutoStart == filter.AutoStart.Value; }
            }

            if (filter.AdjustableSeat.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.AdjustableSeat == filter.AdjustableSeat.Value);
                }
                else { Query = p => p.AdjustableSeat == filter.AdjustableSeat.Value; }
            }


            if (filter.ElectricDoorsWindows.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.ElectricDoorsWindows == filter.ElectricDoorsWindows.Value);
                }
                else { Query = p => p.ElectricDoorsWindows == filter.ElectricDoorsWindows.Value; }
            }

            if (filter.AntiGlareInsideRearViewMirror.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.AntiGlareInsideRearViewMirror == filter.AntiGlareInsideRearViewMirror.Value);
                }
                else { Query = p => p.AntiGlareInsideRearViewMirror == filter.AntiGlareInsideRearViewMirror; }
            }

            if (filter.ReadingLamp.HasValue)
            {
                    if (Query != null)
                    {
                        Query = Query.And(p => p.ReadingLamp == filter.ReadingLamp.Value);
                    }
                    else { Query = p => p.ReadingLamp == filter.ReadingLamp.Value; }
            }

            if (filter.Instrument.HasValue)
            {
                    if (Query != null)
                    {
                        Query = Query.And(p => p.Instrument == filter.Instrument.Value);
                    }
                    else { Query = p => p.Instrument == filter.Instrument.Value; }
            }

            if (filter.PowerInterFace12V.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.PowerInterFace12V == filter.PowerInterFace12V.Value);
                }
                else { Query = p => p.PowerInterFace12V == filter.PowerInterFace12V.Value; }
            }

            if (filter.CentralControlSystem.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.CentralControlSystem == filter.CentralControlSystem.Value);
                }
                else { Query = p => p.CentralControlSystem == filter.CentralControlSystem; }
            }

            if (filter.SafetyBelt3Point.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.SafetyBelt3Point == filter.SafetyBelt3Point.Value);
                }
                else { Query = p => p.SafetyBelt3Point == filter.SafetyBelt3Point.Value; }
            }

            if (filter.CopilotHandle.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.CopilotHandle == filter.CopilotHandle.Value);
                }
                else { Query = p => p.CopilotHandle == filter.CopilotHandle.Value; }
            }

            if (filter.RearViewCamera.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.RearViewCamera == filter.RearViewCamera.Value);
                }
                else { Query = p => p.RearViewCamera == filter.RearViewCamera.Value; }
            }

            if (filter.IntelligentChargingSystem.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.IntelligentChargingSystem == filter.IntelligentChargingSystem.Value);
                }
                else { Query = p => p.IntelligentChargingSystem == filter.IntelligentChargingSystem; }
            }

            if (filter.AndroidInch9.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.AndroidInch9 == filter.AndroidInch9.Value);
                }
                else { Query = p => p.AndroidInch9 == filter.AndroidInch9.Value; }
            }

            if (filter.IntelligentVehicleNavigation.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.IntelligentVehicleNavigation == filter.IntelligentVehicleNavigation.Value);
                }
                else { Query = p => p.IntelligentVehicleNavigation == filter.IntelligentVehicleNavigation.Value; }
            }


            if (filter.ColorfulAmbientLamp.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.ColorfulAmbientLamp == filter.ColorfulAmbientLamp.Value);
                }
                else { Query = p => p.ColorfulAmbientLamp == filter.ColorfulAmbientLamp.Value; }
            }

            if (filter.BluetoothTelephone.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.BluetoothTelephone == filter.BluetoothTelephone.Value);
                }
                else { Query = p => p.BluetoothTelephone == filter.BluetoothTelephone; }
            }

            if (filter.RadioMP3.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.RadioMP3 == filter.RadioMP3.Value);
                }
                else { Query = p => p.RadioMP3 == filter.RadioMP3.Value; }
            }

            if (filter.Loudspeaker.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.Loudspeaker == filter.Loudspeaker.Value);
                }
                else { Query = p => p.Loudspeaker == filter.Loudspeaker.Value; }
            }

            if (Query != null)
            {
                additionalInformation = await _unitOfWork.AdditionalInformatioRepo.Get(filter: Query, includeProperties: properties);
            }
            else
            {
                additionalInformation = await _unitOfWork.AdditionalInformatioRepo.Get(includeProperties: properties);
            }

            var pagedadditionalinformation = PagedList<AdditionalInformation>.Create(additionalInformation, filter.PageNumber, filter.PageSize);

            return pagedadditionalinformation;
        } 
        

        public async Task<GenericResponse<AdditionalInformationDto>> PostAdditionalInformation(AdditionalInformationRequest additionalInformationRequest)
        {
            GenericResponse<AdditionalInformationDto> response = new GenericResponse<AdditionalInformationDto>();
            try
            {
                var entity = _mapper.Map<AdditionalInformation>(additionalInformationRequest);
                await _unitOfWork.AdditionalInformatioRepo.Add(entity);
                await _unitOfWork.SaveChangesAsync();
                response.success = true;
                var adddto = _mapper.Map<AdditionalInformationDto>(entity);
                response.Data = adddto;

                return response;
            }
            catch (Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);
                return response;
            }
        }

        public async Task<GenericResponse<AdditionalInformationDto>> GetAdditionalInformationById(int id)
        {
            GenericResponse<AdditionalInformationDto> response = new GenericResponse<AdditionalInformationDto>();
            var entity = await _unitOfWork.AdditionalInformatioRepo.Get(filter: a => a.Id == id);
            var check = entity.FirstOrDefault();
            var map = _mapper.Map<AdditionalInformationDto>(check);
            response.success = true;
            response.Data = map;
            return response;
        }

        public async Task<GenericResponse<AdditionalInformationDto>> PutAditionalInformation(AdditionalInformationUpdateDto additionalInformationUpdateDto, int id)
        {

            GenericResponse<AdditionalInformationDto> response = new GenericResponse<AdditionalInformationDto>();
            try
            {
                var result = await _unitOfWork.AdditionalInformatioRepo.Get(r => r.Id == id);
                var check = result.FirstOrDefault();
                if (check == null) return null;

                check.VehicleType = additionalInformationUpdateDto.VehicleType ?? check.VehicleType;
                check.Models = additionalInformationUpdateDto.Models ?? check.Models;
                check.LWH = additionalInformationUpdateDto.LWH ?? check.LWH;
                check.WheelBase = additionalInformationUpdateDto.WheelBase ?? check.WheelBase;
                check.WheelTrack = additionalInformationUpdateDto.WheelTrack ?? check.WheelTrack;
                check.MinTurningRadius = additionalInformationUpdateDto.MinTurningRadius ?? check.MinTurningRadius;
                check.UnladdenMass = additionalInformationUpdateDto.UnladdenMass ?? check.UnladdenMass;
                check.Passenger = additionalInformationUpdateDto.Passenger ?? check.Passenger;
                check.WheelSize = additionalInformationUpdateDto.WheelSize ?? check.WheelSize;
                check.SystemVoltage = additionalInformationUpdateDto.SystemVoltage ?? check.SystemVoltage;
                check.BatteryCapacity = additionalInformationUpdateDto.BatteryCapacity ?? check.BatteryCapacity;
                check.Battery = additionalInformationUpdateDto.Battery ?? check.Battery;
                check.MotorType = additionalInformationUpdateDto.MotorType ?? check.MotorType;
                check.RatedPower = additionalInformationUpdateDto.RatedPower ?? check.RatedPower;
                check.IntelligentProtectionOfController = additionalInformationUpdateDto.IntelligentProtectionOfController ?? check.IntelligentProtectionOfController;
                check.FailureIndicationOfDrivingSystem = additionalInformationUpdateDto.FailureIndicationOfDrivingSystem ?? check.FailureIndicationOfDrivingSystem;
                check.PowerOutputMode = additionalInformationUpdateDto.PowerOutputMode ?? check.PowerOutputMode;
                check.SpeedRatioOrReducer = additionalInformationUpdateDto.SpeedRatioOrReducer ?? check.SpeedRatioOrReducer;
                check.MaxSpeed = additionalInformationUpdateDto.MaxSpeed ?? check.MaxSpeed;
                check.MaxCruisingRange = additionalInformationUpdateDto.MaxCruisingRange ?? check.MaxCruisingRange;
                check.OperatingCruisingRange = additionalInformationUpdateDto.OperatingCruisingRange ?? check.OperatingCruisingRange;
                check.CrusingRangeWithUsingAC = additionalInformationUpdateDto.CrusingRangeWithUsingAC ?? check.CrusingRangeWithUsingAC;
                check.PreventSlipping = additionalInformationUpdateDto.PreventSlipping ?? check.PreventSlipping;
                check.PowerConsumption = additionalInformationUpdateDto.PowerConsumption ?? check.PowerConsumption;
                check.ChargeTime = additionalInformationUpdateDto.ChargeTime ?? check.ChargeTime;
                check.FrameType = additionalInformationUpdateDto.FrameType ?? check.FrameType;
                check.FoldableRearViewMirror = additionalInformationUpdateDto.FoldableRearViewMirror ?? check.FoldableRearViewMirror;
                check.AluminiumWheel = additionalInformationUpdateDto.AluminiumWheel ?? check.AluminiumWheel;
                check.LEDDigitalInstrument = additionalInformationUpdateDto.LEDDigitalInstrument ?? check.LEDDigitalInstrument;
                check.LEDTailight = additionalInformationUpdateDto.LEDTailight ?? check.LEDTailight;
                check.LEDHeadlamp = additionalInformationUpdateDto.LEDHeadlamp ?? check.LEDHeadlamp;
                check.HighMountedBrakeLamp = additionalInformationUpdateDto.HighMountedBrakeLamp ?? check.HighMountedBrakeLamp;
                check.FrontSuspension = additionalInformationUpdateDto.FrontSuspension ?? check.FrontSuspension;
                check.RearSuspension = additionalInformationUpdateDto.RearSuspension ?? check.RearSuspension;
                check.BrakeSystem = additionalInformationUpdateDto.BrakeSystem ?? check.BrakeSystem;
                check.AutomaticSteeringWheelReturn = additionalInformationUpdateDto.AutomaticSteeringWheelReturn ?? check.AutomaticSteeringWheelReturn;
                check.BrakeBooster = additionalInformationUpdateDto.BrakeBooster ?? check.BrakeBooster;
                check.BrakingDistance = additionalInformationUpdateDto.BrakingDistance ?? check.BrakingDistance;
                check.EPS = additionalInformationUpdateDto.EPS ?? check.EPS;
                check.RotationsOfSteeringWheel = additionalInformationUpdateDto.RotationsOfSteeringWheel ?? check.RotationsOfSteeringWheel;
                check.FullAutomaticAC = additionalInformationUpdateDto.FullAutomaticAC ?? check.FullAutomaticAC;
                check.Heater = additionalInformationUpdateDto.Heater ?? check.Heater;
                check.InermittentWindshieldWiper = additionalInformationUpdateDto.InermittentWindshieldWiper ?? check.InermittentWindshieldWiper;
                check.DigitalReversingRader = additionalInformationUpdateDto.DigitalReversingRader ?? check.DigitalReversingRader;
                check.AutoInductionHeadlamp = additionalInformationUpdateDto.AutoInductionHeadlamp ?? check.AutoInductionHeadlamp;
                check.AutoStart = additionalInformationUpdateDto.AutoStart ?? check.AutoStart;
                check.AdjustableSeat = additionalInformationUpdateDto.AdjustableSeat ?? check.AdjustableSeat;
                check.ElectricDoorsWindows = additionalInformationUpdateDto.ElectricDoorsWindows ?? check.ElectricDoorsWindows;
                check.AntiGlareInsideRearViewMirror = additionalInformationUpdateDto.AntiGlareInsideRearViewMirror ?? check.AntiGlareInsideRearViewMirror;
                check.ReadingLamp = additionalInformationUpdateDto.ReadingLamp ?? check.ReadingLamp;
                check.Instrument = additionalInformationUpdateDto.Instrument ?? check.Instrument;
                check.PowerInterFace12V = additionalInformationUpdateDto.PowerInterFace12V ?? check.PowerInterFace12V;
                check.CentralControlSystem = additionalInformationUpdateDto.CentralControlSystem ?? check.CentralControlSystem;
                check.SafetyBelt3Point = additionalInformationUpdateDto.SafetyBelt3Point ?? check.SafetyBelt3Point;
                check.CopilotHandle = additionalInformationUpdateDto.CopilotHandle ?? check.CopilotHandle;
                check.RearViewCamera = additionalInformationUpdateDto.RearViewCamera ?? check.RearViewCamera;
                check.IntelligentChargingSystem = additionalInformationUpdateDto.IntelligentChargingSystem ?? check.IntelligentChargingSystem;
                check.AndroidInch9 = additionalInformationUpdateDto.AndroidInch9 ?? check.AndroidInch9;
                check.IntelligentVehicleNavigation = additionalInformationUpdateDto.IntelligentVehicleNavigation ?? check.IntelligentVehicleNavigation;
                check.ColorfulAmbientLamp = additionalInformationUpdateDto.ColorfulAmbientLamp ?? check.ColorfulAmbientLamp;
                check.BluetoothTelephone = additionalInformationUpdateDto.BluetoothTelephone ?? check.BluetoothTelephone;
                check.RadioMP3 = additionalInformationUpdateDto.RadioMP3 ?? check.RadioMP3;
                check.Loudspeaker = additionalInformationUpdateDto.Loudspeaker ?? check.Loudspeaker;

                await _unitOfWork.AdditionalInformatioRepo.Update(check);
                await _unitOfWork.SaveChangesAsync();
                var map = _mapper.Map<AdditionalInformationDto>(check);
                response.success = true;
                response.Data = map;
                return response;
            }
            catch (Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);
                return response;
            }
        }

        public async Task<GenericResponse<AdditionalInformation>> DeleteAdditionalInformation(int id)
        {
            GenericResponse<AdditionalInformation> response = new GenericResponse<AdditionalInformation>();
            try
            {
                var check = await _unitOfWork.AdditionalInformatioRepo.GetById(id);
                if (check == null) return null;
                var exists = await _unitOfWork.AdditionalInformatioRepo.Delete(id);
                await _unitOfWork.SaveChangesAsync();
                var checkdto = _mapper.Map<AdditionalInformation>(check);
                response.success = true;
                response.Data = checkdto;
                return response;
            }
            catch (Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);
                return response;
            }
        }
    } 
}