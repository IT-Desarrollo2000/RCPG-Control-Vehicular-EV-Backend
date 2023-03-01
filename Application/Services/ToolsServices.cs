using Application.Interfaces;
using AutoMapper;
using Domain.CustomEntities;
using Domain.DTOs.Reponses;
using Domain.Entities.Registered_Cars;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Application.Services
{
    public class ToolsServices : IToolsServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ToolsServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<GenericResponse<List<LicenceExpiredDto>>> GetLicencesExpirations(LicenceExpStopLight request)
        {
            GenericResponse<List<LicenceExpiredDto>> response = new GenericResponse<List<LicenceExpiredDto>>();

            try
            {
                switch (request)
                {
                    case LicenceExpStopLight.EXPIRADOS: 
                        var explicences = await _unitOfWork.UserProfileRepo.Get(u => u.LicenceExpirationDate <= DateTime.UtcNow);
                        var dtos = new List<LicenceExpiredDto>();
                        foreach(var licence in explicences)
                        {
                            var dto = _mapper.Map<LicenceExpiredDto>(licence);
                            dto.StatusName = "Expirado";
                            dto.StatusMessage = "La Licencia se encuentra expirada";
                            dto.StatusColor = "#e41212";
                            dto.ExpirationType = LicenceExpStopLight.EXPIRADOS;

                            dtos.Add(dto);
                        }

                        response.Data = dtos;
                        response.success = true;

                        return response;
                    case LicenceExpStopLight.TRES_MESES:
                        var licences3m = await _unitOfWork.UserProfileRepo.Get(u => u.LicenceExpirationDate >= DateTime.UtcNow.AddMonths(3) && u.LicenceExpirationDate <= DateTime.UtcNow.AddMonths(6));

                        var dtos1 = new List<LicenceExpiredDto>();
                        foreach (var licence in licences3m)
                        {
                            var dto = _mapper.Map<LicenceExpiredDto>(licence);
                            dto.StatusName = "3 MESES";
                            dto.StatusMessage = "Licencia con 3 a 6 meses de vigencia";
                            dto.StatusColor = "#efbc38";
                            dto.ExpirationType = LicenceExpStopLight.TRES_MESES;

                            dtos1.Add(dto);
                        }

                        response.Data = dtos1;
                        response.success = true;

                        return response;
                    case LicenceExpStopLight.SEIS_MESES:
                        var licences6m = await _unitOfWork.UserProfileRepo.Get(u => u.LicenceExpirationDate >= DateTime.UtcNow.AddMonths(6) && u.LicenceExpirationDate <= DateTime.UtcNow.AddMonths(12));

                        var dtos2 = new List<LicenceExpiredDto>();
                        foreach (var licence in licences6m)
                        {
                            var dto = _mapper.Map<LicenceExpiredDto>(licence);
                            dto.StatusName = "6 MESES";
                            dto.StatusMessage = "Licencia con 6 a 12 meses de vigencia";
                            dto.StatusColor = "#f3d132";
                            dto.ExpirationType = LicenceExpStopLight.SEIS_MESES;

                            dtos2.Add(dto);
                        }

                        response.Data = dtos2;
                        response.success = true;

                        return response;
                    case LicenceExpStopLight.DOCE_MESES:
                        var licences12m = await _unitOfWork.UserProfileRepo.Get(u => u.LicenceExpirationDate >= DateTime.UtcNow.AddMonths(12));

                        var dtos3 = new List<LicenceExpiredDto>();
                        foreach (var licence in licences12m)
                        {
                            var dto = _mapper.Map<LicenceExpiredDto>(licence);
                            dto.StatusName = "12 MESES";
                            dto.StatusMessage = "Licencia con 12 meses o mas de vigencia";
                            dto.StatusColor = "#3ee80b";
                            dto.ExpirationType = LicenceExpStopLight.DOCE_MESES;

                            dtos3.Add(dto);
                        }

                        response.Data = dtos3;
                        response.success = true;

                        return response;
                    default:
                        response.success = false;
                        return response;
                }
            }
            catch (Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);

                return response;
            }
        }

        public async Task<GenericResponse<List<PolicyExpiredDto>>> GetPoliciesExpiration(LicenceExpStopLight request)
        {
            GenericResponse<List<PolicyExpiredDto>> response = new GenericResponse<List<PolicyExpiredDto>>();

            try
            {
                switch (request)
                {
                    case LicenceExpStopLight.EXPIRADOS:
                        var expPolicies = await _unitOfWork.PolicyRepo.Get(u => u.ExpirationDate <= DateTime.UtcNow, includeProperties: "Vehicle");
                        var dtos = new List<PolicyExpiredDto>();
                        foreach (var policy in expPolicies)
                        {
                            var dto = _mapper.Map<PolicyExpiredDto>(policy);
                            dto.StatusName = "Expirado";
                            dto.StatusMessage = "La poliza se encuentra expirada";
                            dto.StatusColor = "#e41212";
                            dto.ExpirationType = LicenceExpStopLight.EXPIRADOS;

                            dtos.Add(dto);
                        }

                        response.Data = dtos;
                        response.success = true;

                        return response;
                    case LicenceExpStopLight.TRES_MESES:
                        var policy3m = await _unitOfWork.PolicyRepo.Get(u => u.ExpirationDate >= DateTime.UtcNow.AddMonths(3) && u.ExpirationDate <= DateTime.UtcNow.AddMonths(6), includeProperties: "Vehicle");

                        var dtos1 = new List<PolicyExpiredDto>();
                        foreach (var policy in policy3m)
                        {
                            var dto = _mapper.Map<PolicyExpiredDto>(policy);
                            dto.StatusName = "3 MESES";
                            dto.StatusMessage = "Poliza con 3 a 6 meses de vigencia";
                            dto.StatusColor = "#efbc38";
                            dto.ExpirationType = LicenceExpStopLight.TRES_MESES;

                            dtos1.Add(dto);
                        }

                        response.Data = dtos1;
                        response.success = true;

                        return response;
                    case LicenceExpStopLight.SEIS_MESES:
                        var policy6m = await _unitOfWork.PolicyRepo.Get(u => u.ExpirationDate >= DateTime.UtcNow.AddMonths(6) && u.ExpirationDate <= DateTime.UtcNow.AddMonths(12), includeProperties: "Vehicle");

                        var dtos2 = new List<PolicyExpiredDto>();
                        foreach (var policy in policy6m)
                        {
                            var dto = _mapper.Map<PolicyExpiredDto>(policy);
                            dto.StatusName = "6 MESES";
                            dto.StatusMessage = "Poliza con 6 a 12 meses de vigencia";
                            dto.StatusColor = "#f3d132";
                            dto.ExpirationType = LicenceExpStopLight.SEIS_MESES;

                            dtos2.Add(dto);
                        }

                        response.Data = dtos2;
                        response.success = true;

                        return response;
                    case LicenceExpStopLight.DOCE_MESES:
                        var policy12m = await _unitOfWork.PolicyRepo.Get(u => u.ExpirationDate   >= DateTime.UtcNow.AddMonths(12), includeProperties: "Vehicle");

                        var dtos3 = new List<PolicyExpiredDto>();
                        foreach (var policy in policy12m)
                        {
                            var dto = _mapper.Map<PolicyExpiredDto>(policy);
                            dto.StatusName = "12 MESES";
                            dto.StatusMessage = "Poliza con 12 meses o mas de vigencia";
                            dto.StatusColor = "#3ee80b";
                            dto.ExpirationType = LicenceExpStopLight.DOCE_MESES;

                            dtos3.Add(dto);
                        }

                        response.Data = dtos3;
                        response.success = true;

                        return response;
                    default:
                        response.success = false;
                        return response;
                }
            }
            catch(Exception ex) 
            {
                response.success = false;
                response.AddError("Error",ex.Message, 1);
                return response;
            }
        }

        public async Task<GenericResponse<List<MaintenanceSpotlightDto>>> GetMaintenanceSpotlight()
        {
            GenericResponse<List<MaintenanceSpotlightDto>> response = new GenericResponse<List<MaintenanceSpotlightDto>>();

            try
            {
                //Lista de vehiculos de pronto servicio
                List<MaintenanceSpotlightDto> dtos = new List<MaintenanceSpotlightDto>();

                //Obtener los autos
                var vehicles = await _unitOfWork.VehicleRepo.Get(includeProperties: "VehicleServices");
                
                foreach(var vehicle in vehicles)
                {
                    var lastServicesQuery = await _unitOfWork.VehicleServiceRepo.Get(filter: s => s.Status == VehicleServiceStatus.FINALIZADO && s.VehicleId == vehicle.Id, includeProperties: "Vehicle");
                    var lastServices = lastServicesQuery.FirstOrDefault();
                    
                    //Verificar si ya cuenta con un servicio previo
                    if(lastServices != null)
                    {
                        //Revisar por fecha
                        TimeSpan timespan = lastServices.NextService.Value - DateTime.UtcNow;
                        switch (timespan.TotalDays)
                        {
                            case double d when d > 30:
                                MaintenanceSpotlightDto dto = _mapper.Map<MaintenanceSpotlightDto>(lastServices);
                                dto.StatusMessage = "No requiere de servicio";
                                dto.StatusName = "OK";
                                dto.StatusColor = "#3ee80b";
                                dto.AlertType = StopLightAlert.VERDE;
                                dtos.Add(dto);
                                break;
                            case double d when d >= 15 && d <= 30:
                                MaintenanceSpotlightDto dtoyellow = _mapper.Map<MaintenanceSpotlightDto>(lastServices);
                                dtoyellow.StatusMessage = "El vehiculo requiere de servicio pronto";
                                dtoyellow.StatusName = "ATENCIÓN";
                                dtoyellow.StatusColor = "#f3d132";
                                dtoyellow.AlertType = StopLightAlert.AMARILLO;
                                dtos.Add(dtoyellow);
                                break;
                            case double d when d >= 5 && d < 15:
                                MaintenanceSpotlightDto dtogreen = _mapper.Map<MaintenanceSpotlightDto>(lastServices);
                                dtogreen.StatusMessage = "El vehiculo requiere de servicio";
                                dtogreen.StatusName = "ATENCIÓN!!";
                                dtogreen.StatusColor = "#efbc38";
                                dtogreen.AlertType= StopLightAlert.NARANJA;
                                dtos.Add(dtogreen);
                                break;
                            case double d when d < 5:
                                MaintenanceSpotlightDto dtored = _mapper.Map<MaintenanceSpotlightDto>(lastServices);
                                dtored.StatusMessage = "Es requerido llevar el vehiculo a servicio";
                                dtored.StatusName = "SERVICIO NECESARIO!!";
                                dtored.StatusColor = "#e41212";
                                dtored.AlertType = StopLightAlert.ROJO;
                                dtos.Add(dtored);
                                break;
                        }

                        //Revisar por odometro
                        if (lastServices.NextServiceKM <= vehicle.CurrentKM)
                        {
                            MaintenanceSpotlightDto dtored = _mapper.Map<MaintenanceSpotlightDto>(lastServices);
                            dtored.StatusMessage = "Es requerido llevar el vehiculo a servicio";
                            dtored.StatusName = "SERVICIO NECESARIO!!";
                            dtored.StatusColor = "#e41212";
                            dtored.AlertType = StopLightAlert.ROJO;
                            dtos.Add(dtored);
                        }
                    }
                    else
                    {
                        if(vehicle.CurrentKM >= vehicle.ServicePeriodKM)
                        {
                            MaintenanceSpotlightDto dtored = _mapper.Map<MaintenanceSpotlightDto>(vehicle);
                            dtored.Type = VehicleServiceType.Kilometraje;
                            dtored.StatusMessage = "Es requerido llevar el vehiculo a servicio";
                            dtored.StatusName = "SERVICIO NECESARIO!!";
                            dtored.StatusColor = "#e41212";
                            dtored.AlertType = StopLightAlert.ROJO;
                            dtos.Add(dtored);
                        }

                        var timespan = vehicle.CreatedDate.Value.AddMonths(vehicle.ServicePeriodMonths) - DateTime.UtcNow;
                        switch (timespan.TotalDays)
                        {
                            case double d when d > 30:
                                MaintenanceSpotlightDto dto = _mapper.Map<MaintenanceSpotlightDto>(vehicle);
                                dto.Type = VehicleServiceType.Fecha;
                                dto.StatusMessage = "No requiere de servicio";
                                dto.StatusName = "OK";
                                dto.StatusColor = "#3ee80b";
                                dto.AlertType = StopLightAlert.VERDE;
                                dtos.Add(dto);
                                break;
                            case double d when d >= 15 && d <= 30:
                                MaintenanceSpotlightDto dtoyellow = _mapper.Map<MaintenanceSpotlightDto>(vehicle);
                                dtoyellow.Type = VehicleServiceType.Fecha;
                                dtoyellow.StatusMessage = "El vehiculo requiere de servicio pronto";
                                dtoyellow.StatusName = "ATENCIÓN";
                                dtoyellow.StatusColor = "#f3d132";
                                dtoyellow.AlertType = StopLightAlert.AMARILLO;
                                dtos.Add(dtoyellow);
                                break;
                            case double d when d >= 5 && d < 15:
                                MaintenanceSpotlightDto dtogreen = _mapper.Map<MaintenanceSpotlightDto>(vehicle);
                                dtogreen.Type = VehicleServiceType.Fecha;
                                dtogreen.StatusMessage = "El vehiculo requiere de servicio";
                                dtogreen.StatusName = "ATENCIÓN!!";
                                dtogreen.StatusColor = "#efbc38";
                                dtogreen.AlertType = StopLightAlert.NARANJA;
                                dtos.Add(dtogreen);
                                break;
                            case double d when d < 5:
                                MaintenanceSpotlightDto dtored = _mapper.Map<MaintenanceSpotlightDto>(vehicle);
                                dtored.Type = VehicleServiceType.Fecha;
                                dtored.StatusMessage = "Es requerido llevar el vehiculo a servicio";
                                dtored.StatusName = "SERVICIO NECESARIO!!";
                                dtored.StatusColor = "#e41212";
                                dtored.AlertType = StopLightAlert.ROJO;
                                dtos.Add(dtored);
                                break;
                        }
                    }
                }

                response.success = true;
                response.Data = dtos;

                return response;
            }
            catch(Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);

                return response;
            }
        }
        
        //GetAllVehicleStatus
        public async Task<GenericResponse<List<GetVehicleActiveDto>>> GetAllVehiclesActive()
        {
            GenericResponse<List<GetVehicleActiveDto>> response = new GenericResponse<List<GetVehicleActiveDto>>();
            try
            {
                var VehicleA = await _unitOfWork.VehicleReportUseRepo.Get(filter: status => status.StatusReportUse == Domain.Enums.ReportUseType.ViajeNormal, includeProperties: "Vehicle,UserProfile,Destinations");
                if (VehicleA == null)
                {
                    response.success = false;
                    response.AddError("No existe Vehiculos Por Mostrar", "No Hay Vehiculos en Viaje actualmente", 1);
                    return response;
                }

                var dtos = _mapper.Map<List<GetVehicleActiveDto>>(VehicleA);
                response.success = true;
                response.Data = dtos;
                return response;

            }
            catch(Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);

                return response;

            }
            
        }

        public async Task<GenericResponse<List<GraphicsPerfomanceDto>>> GetAllPerfomance(int VehicleId)
        {
            GenericResponse<List<GraphicsPerfomanceDto>> response = new GenericResponse<List<GraphicsPerfomanceDto>>();

            try 
            {
                var Rendimiento = await _unitOfWork.VehicleReportRepo.Get(filter: reportStatus => reportStatus.ReportType == Domain.Enums.ReportType.Carga_Gasolina && reportStatus.VehicleId == VehicleId, includeProperties: "Vehicle,VehicleReportUses");
                var result =  Rendimiento.FirstOrDefault();
                var list = new List<GraphicsPerfomanceDto>();

                if(Rendimiento == null)
                {
                    response.success = false;
                    response.AddError("No existe ",$"No existe Vehiculo con el Id { VehicleId }", 1);
                    return response;
                }

                if(Rendimiento.Count() == 0)
                {
                    var Perfomance = new GraphicsPerfomanceDto()
                    {
                        VehicleId = VehicleId,
                        VehicleName = "No se obtuvo informacion del documento solicitado",
                        CurrentKm = 0,
                        LastKm = 0,
                        GasolineLoadAmount = 0,
                        MileageTraveled = 0,
                        Perfomance = 0,
                        error = $"No existe rendimiento por reportes de Vehiculo { VehicleId } "
                    };

                    list.Add(Perfomance);

                }

                else
                {
                    foreach (var Aray in Rendimiento)
                    {

                        var KmActual = Aray.VehicleReportUses.FinalMileage;
                        var KmUltimo = Aray.VehicleReportUses.InitialMileage;
                        var GasolinaCarga = Aray.GasolineLoadAmount;

                        var KmRecorrido = KmActual - KmUltimo;
                        var KmPorLitros = KmRecorrido / GasolinaCarga;


                        var Perfomance = new GraphicsPerfomanceDto()
                        {
                            VehicleId = Aray.VehicleId,
                            VehicleName = Aray.Vehicle.Name,
                            CurrentKm = Aray.VehicleReportUses.FinalMileage ?? 0,
                            LastKm = Aray.VehicleReportUses.InitialMileage ?? 0,
                            GasolineLoadAmount = Aray.GasolineLoadAmount ?? 0,
                            MileageTraveled = KmRecorrido ?? 0,
                            Perfomance = KmPorLitros ?? 0
                        };

                        list.Add(Perfomance);

                    }

                }

                response.success = true;
                response.Data = list;

                return response;

            }
            catch (Exception ex) 
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);

                return response;
            }

        }

        public async Task<GenericResponse<TotalPerfomanceDto>> GetTotalPerfomance(int VehicleId)
        {
            GenericResponse<TotalPerfomanceDto> response = new GenericResponse<TotalPerfomanceDto>();
            var list = new List<TotalPerfomanceDto>();

            try
            {
                var Rendimiento = await _unitOfWork.VehicleReportRepo.Get(filter: reportStatus => reportStatus.ReportType == Domain.Enums.ReportType.Carga_Gasolina && reportStatus.VehicleId == VehicleId, includeProperties: "Vehicle,VehicleReportUses");
             
                var listt = new List<GraphicsPerfomanceDto>();
         
     


                if (Rendimiento == null)
                {
                    response.success = false;
                    response.AddError("No existe ", $"No existe Vehiculo con el Id {VehicleId}", 1);
                    return response;
                }

                if(Rendimiento.Count() == 0)
                {
                    var Totale = new TotalPerfomanceDto()
                    {
                        VehicleId = VehicleId,
                        VehicleName = "No se obtuvo informacion del documento solicitado",
                        TotalMileageTraveled = 0,
                        TotalPerfomance = 0,
                        error = $"No existe datos de Rendimiento para { VehicleId } "
                    };

                    response.success = true;
                    response.Data = Totale;
                    return response;

                }

                else
                {
                    var Name = Rendimiento.FirstOrDefault().Vehicle.Name;
                    double sum = 0;
                    double sum2 = 0;

                    foreach (var Aray in Rendimiento)
                    {
                        var KmActual = Aray.VehicleReportUses.FinalMileage;
                        var KmUltimo = Aray.VehicleReportUses.InitialMileage;
                        var GasolinaCarga = Aray.GasolineLoadAmount;

                        var KmRecorrido = KmActual - KmUltimo;
                        var KmPorLitros = KmRecorrido / GasolinaCarga;


                        var Perfomance = new GraphicsPerfomanceDto()
                        {
                            VehicleId = Aray.VehicleId,
                            VehicleName = Aray.Vehicle.Name,
                            CurrentKm = Aray.VehicleReportUses.FinalMileage ?? 0,
                            LastKm = Aray.VehicleReportUses.InitialMileage ?? 0,
                            GasolineLoadAmount = Aray.GasolineLoadAmount ?? 0,
                            MileageTraveled = KmRecorrido ?? 0,
                            Perfomance = KmPorLitros ?? 0
                        };

                        listt.Add(Perfomance);

                        sum += Perfomance.MileageTraveled;
                        sum2 += Perfomance.Perfomance;


                    }
                    var Total = new TotalPerfomanceDto()
                    {
                        VehicleId = VehicleId,
                        VehicleName = Name,
                        TotalMileageTraveled = sum / Rendimiento.Count(),
                        TotalPerfomance = sum2 / Rendimiento.Count()
                    };

                    response.success = true;
                    response.Data = Total;
                    return response;

                }
      

            }
            catch (Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);

                return response;
            }

        }

        public async Task<GenericResponse<List<TotalPerfomanceDto>>> GetListTotalPerfomance([FromForm] ListTotalPerfomanceDto listTotalPerfomanceDto )
        {
            GenericResponse<List<TotalPerfomanceDto>> response = new GenericResponse<List<TotalPerfomanceDto>>();
            var list = new List<TotalPerfomanceDto>();
            try
            {

                foreach(var Enteros in listTotalPerfomanceDto.VehicleId)
                {
                    var Rendimiento = await _unitOfWork.VehicleReportRepo.Get(filter: reportStatus => reportStatus.ReportType == Domain.Enums.ReportType.Carga_Gasolina && reportStatus.VehicleId == Enteros, includeProperties: "Vehicle,VehicleReportUses");
                
                    var listt = new List<GraphicsPerfomanceDto>();
                 
                    

                    if (Rendimiento == null)
                    {
                        response.success = false;
                        response.AddError("No existe ", $"No existe Vehiculo con el Id { Enteros }", 1);
                        return response;
                    }

                    if(Rendimiento.Count() == 0)
                    {
                        var Totale = new TotalPerfomanceDto()
                        {
                            VehicleId = Enteros,
                            VehicleName = "No se obtuvo informacion del vehiculo solicitado",
                            TotalMileageTraveled = 0,
                            TotalPerfomance = 0,
                            error = $"No existe datos de Rendimiento para { Enteros } "
                        };

                        list.Add(Totale);

                    }

                    else
                    {
                        var Name = Rendimiento.FirstOrDefault().Vehicle.Name;
                        double sum = 0;
                        double sum2 = 0;
                        foreach (var Aray in Rendimiento)
                        {
                            var KmActual = Aray.VehicleReportUses.FinalMileage;
                            var KmUltimo = Aray.VehicleReportUses.InitialMileage;
                            var GasolinaCarga = Aray.GasolineLoadAmount;

                            var KmRecorrido = KmActual - KmUltimo;
                            var KmPorLitros = KmRecorrido / GasolinaCarga;


                            var Perfomance = new GraphicsPerfomanceDto()
                            {
                                VehicleId = Aray.VehicleId,
                                VehicleName = Aray.Vehicle.Name,
                                CurrentKm = Aray.VehicleReportUses.FinalMileage ?? 0,
                                LastKm = Aray.VehicleReportUses.InitialMileage ?? 0,
                                GasolineLoadAmount = Aray.GasolineLoadAmount ?? 0,
                                MileageTraveled = KmRecorrido ?? 0,
                                Perfomance = KmPorLitros ?? 0
                            };

                            listt.Add(Perfomance);

                            sum += Perfomance.MileageTraveled;
                            sum2 += Perfomance.Perfomance;


                        }
                        var Total = new TotalPerfomanceDto()
                        {
                            VehicleId = Enteros,
                            VehicleName = Name,
                            TotalMileageTraveled = sum / Rendimiento.Count(),
                            TotalPerfomance = sum2 / Rendimiento.Count()
                        };

                        list.Add(Total);

                    }

                }
                response.success = true;
                response.Data = list;
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
