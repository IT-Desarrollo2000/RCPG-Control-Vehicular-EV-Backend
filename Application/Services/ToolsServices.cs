using Application.Interfaces;
using AutoMapper;
using Domain.CustomEntities;
using Domain.DTOs.Reponses;
using Domain.Enums;

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
                        foreach (var licence in explicences)
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
                        var licences3mQuery = await _unitOfWork.UserProfileRepo.Get(l => l.LicenceExpirationDate != null);
                        var licences3m = licences3mQuery.Where(u => (u.LicenceExpirationDate.Value - DateTime.UtcNow).TotalDays <= 90 && (u.LicenceExpirationDate.Value - DateTime.UtcNow).TotalDays > 0);
                        var dtos1 = new List<LicenceExpiredDto>();
                        foreach (var licence in licences3m)
                        {
                            var dto = _mapper.Map<LicenceExpiredDto>(licence);
                            dto.StatusName = "3 MESES";
                            dto.StatusMessage = "Licencia con 3 meses de vigencia";
                            dto.StatusColor = "#efbc38";
                            dto.ExpirationType = LicenceExpStopLight.TRES_MESES;

                            dtos1.Add(dto);
                        }

                        response.Data = dtos1;
                        response.success = true;

                        return response;
                    case LicenceExpStopLight.SEIS_MESES:
                        var licences6mQuery = await _unitOfWork.UserProfileRepo.Get(l => l.LicenceExpirationDate != null);
                        var licences6m = licences6mQuery.Where(u => (u.LicenceExpirationDate.Value - DateTime.UtcNow).TotalDays <= 180 && (u.LicenceExpirationDate.Value - DateTime.UtcNow).TotalDays > 90);

                        var dtos2 = new List<LicenceExpiredDto>();
                        foreach (var licence in licences6m)
                        {
                            var dto = _mapper.Map<LicenceExpiredDto>(licence);
                            dto.StatusName = "6 MESES";
                            dto.StatusMessage = "Licencia con 6 meses de vigencia";
                            dto.StatusColor = "#f3d132";
                            dto.ExpirationType = LicenceExpStopLight.SEIS_MESES;

                            dtos2.Add(dto);
                        }

                        response.Data = dtos2;
                        response.success = true;

                        return response;
                    case LicenceExpStopLight.DOCE_MESES:
                        var licences12mQuery = await _unitOfWork.UserProfileRepo.Get(u => u.LicenceExpirationDate != null);
                        var licences12m = licences12mQuery.Where(u => (u.LicenceExpirationDate.Value - DateTime.UtcNow).TotalDays > 180);

                        var dtos3 = new List<LicenceExpiredDto>();
                        foreach (var licence in licences12m)
                        {
                            var dto = _mapper.Map<LicenceExpiredDto>(licence);
                            dto.StatusName = "12 MESES";
                            dto.StatusMessage = "Licencia con mas de 6 meses de vigencia";
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
                        var policy3mQuery = await _unitOfWork.PolicyRepo.Get(includeProperties: "Vehicle");
                        var policy3m = policy3mQuery.Where(u => (u.ExpirationDate - DateTime.UtcNow).TotalDays <= 90 && (u.ExpirationDate - DateTime.UtcNow).TotalDays > 0);

                        var dtos1 = new List<PolicyExpiredDto>();
                        foreach (var policy in policy3m)
                        {
                            var dto = _mapper.Map<PolicyExpiredDto>(policy);
                            dto.StatusName = "3 MESES";
                            dto.StatusMessage = "Poliza con 3 meses de vigencia";
                            dto.StatusColor = "#efbc38";
                            dto.ExpirationType = LicenceExpStopLight.TRES_MESES;

                            dtos1.Add(dto);
                        }

                        response.Data = dtos1;
                        response.success = true;

                        return response;
                    case LicenceExpStopLight.SEIS_MESES:
                        var policy6mQuery = await _unitOfWork.PolicyRepo.Get(u => u.ExpirationDate >= DateTime.UtcNow.AddMonths(6) && u.ExpirationDate <= DateTime.UtcNow.AddMonths(12), includeProperties: "Vehicle");
                        var policy6m = policy6mQuery.Where(u => (u.ExpirationDate - DateTime.UtcNow).TotalDays <= 180 && (u.ExpirationDate - DateTime.UtcNow).TotalDays > 90);
                        var dtos2 = new List<PolicyExpiredDto>();
                        foreach (var policy in policy6m)
                        {
                            var dto = _mapper.Map<PolicyExpiredDto>(policy);
                            dto.StatusName = "6 MESES";
                            dto.StatusMessage = "Poliza con 6 meses de vigencia";
                            dto.StatusColor = "#f3d132";
                            dto.ExpirationType = LicenceExpStopLight.SEIS_MESES;

                            dtos2.Add(dto);
                        }

                        response.Data = dtos2;
                        response.success = true;

                        return response;
                    case LicenceExpStopLight.DOCE_MESES:
                        var policy12mQuery = await _unitOfWork.PolicyRepo.Get(u => u.ExpirationDate >= DateTime.UtcNow.AddMonths(12), includeProperties: "Vehicle");
                        var policy12m = policy12mQuery.Where(u => (u.ExpirationDate - DateTime.UtcNow).TotalDays > 180);
                        var dtos3 = new List<PolicyExpiredDto>();
                        foreach (var policy in policy12m)
                        {
                            var dto = _mapper.Map<PolicyExpiredDto>(policy);
                            dto.StatusName = "12 MESES";
                            dto.StatusMessage = "Poliza con 6 meses o mas de vigencia";
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

        public async Task<GenericResponse<List<MaintenanceSpotlightDto>>> GetMaintenanceSpotlight()
        {
            GenericResponse<List<MaintenanceSpotlightDto>> response = new GenericResponse<List<MaintenanceSpotlightDto>>();

            try
            {
                //Lista de vehiculos de pronto servicio
                List<MaintenanceSpotlightDto> dtos = new List<MaintenanceSpotlightDto>();

                //Obtener los autos
                var vehicles = await _unitOfWork.VehicleRepo.Get(includeProperties: "VehicleServices");

                foreach (var vehicle in vehicles)
                {
                    var lastServicesQuery = await _unitOfWork.VehicleServiceRepo.Get(filter: s => s.Status == VehicleServiceStatus.FINALIZADO && s.VehicleId == vehicle.Id, includeProperties: "Vehicle");
                    var lastServices = lastServicesQuery.LastOrDefault();

                    //Verificar si ya cuenta con un servicio previo
                    if (lastServices != null)
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
                                dto.LastServiceId = lastServices.Id;
                                dto.NextServiceDate = lastServices.NextService;
                                dto.NextServiceKM = lastServices.NextServiceKM;
                                dto.Type = VehicleServiceType.Fecha;
                                dto.LastServiceDate = lastServices.CreatedDate;
                                dtos.Add(dto);
                                break;
                            case double d when d >= 15 && d <= 30:
                                MaintenanceSpotlightDto dtoyellow = _mapper.Map<MaintenanceSpotlightDto>(lastServices);
                                dtoyellow.StatusMessage = "El vehiculo requiere de servicio pronto";
                                dtoyellow.StatusName = "ATENCIÓN";
                                dtoyellow.StatusColor = "#f3d132";
                                dtoyellow.AlertType = StopLightAlert.AMARILLO;
                                dtoyellow.LastServiceId = lastServices.Id;
                                dtoyellow.NextServiceDate = lastServices.NextService;
                                dtoyellow.NextServiceKM = lastServices.NextServiceKM;
                                dtoyellow.Type = VehicleServiceType.Fecha;
                                dtoyellow.LastServiceDate = lastServices.CreatedDate;
                                dtos.Add(dtoyellow);
                                break;
                            case double d when d >= 5 && d < 15:
                                MaintenanceSpotlightDto dtogreen = _mapper.Map<MaintenanceSpotlightDto>(lastServices);
                                dtogreen.StatusMessage = "El vehiculo requiere de servicio";
                                dtogreen.StatusName = "ATENCIÓN!!";
                                dtogreen.StatusColor = "#efbc38";
                                dtogreen.AlertType = StopLightAlert.NARANJA;
                                dtogreen.LastServiceId = lastServices.Id;
                                dtogreen.NextServiceDate = lastServices.NextService;
                                dtogreen.NextServiceKM = lastServices.NextServiceKM;
                                dtogreen.Type = VehicleServiceType.Fecha;
                                dtogreen.LastServiceDate = lastServices.CreatedDate;
                                dtos.Add(dtogreen);
                                break;
                            case double d when d < 5:
                                MaintenanceSpotlightDto dtored = _mapper.Map<MaintenanceSpotlightDto>(lastServices);
                                dtored.StatusMessage = "Es requerido llevar el vehiculo a servicio";
                                dtored.StatusName = "SERVICIO NECESARIO!!";
                                dtored.StatusColor = "#e41212";
                                dtored.AlertType = StopLightAlert.ROJO;
                                dtored.LastServiceId = lastServices.Id;
                                dtored.NextServiceDate = lastServices.NextService;
                                dtored.NextServiceKM = lastServices.NextServiceKM;
                                dtored.Type = VehicleServiceType.Fecha;
                                dtored.LastServiceDate = lastServices.CreatedDate;
                                dtos.Add(dtored);
                                break;
                        }

                        //Revisar por odometro
                        if ((lastServices.NextServiceKM - vehicle.CurrentKM) <= 300)
                        {
                            MaintenanceSpotlightDto dtored = _mapper.Map<MaintenanceSpotlightDto>(lastServices);
                            dtored.StatusMessage = "Es requerido llevar el vehiculo a servicio";
                            dtored.StatusName = "SERVICIO NECESARIO!!";
                            dtored.StatusColor = "#e41212";
                            dtored.AlertType = StopLightAlert.ROJO;
                            dtored.NextServiceDate = lastServices.NextService;
                            dtored.NextServiceKM = lastServices.NextServiceKM;
                            dtored.Type = VehicleServiceType.Kilometraje;
                            dtored.LastServiceDate = lastServices.CreatedDate;
                            dtored.NextServiceKMDiff = lastServices.NextServiceKM - lastServices.Vehicle.CurrentKM;
                            dtos.Add(dtored);
                        }
                        else if ((lastServices.NextServiceKM - vehicle.CurrentKM) <= 1000)
                        {
                            MaintenanceSpotlightDto dtoyellow = _mapper.Map<MaintenanceSpotlightDto>(lastServices);
                            dtoyellow.StatusMessage = "El vehiculo requiere de servicio pronto";
                            dtoyellow.StatusName = "ATENCIÓN";
                            dtoyellow.StatusColor = "#f3d132";
                            dtoyellow.AlertType = StopLightAlert.AMARILLO;
                            dtoyellow.LastServiceId = lastServices.Id;
                            dtoyellow.NextServiceDate = lastServices.NextService;
                            dtoyellow.NextServiceKM = lastServices.NextServiceKM;
                            dtoyellow.Type = VehicleServiceType.Kilometraje;
                            dtoyellow.LastServiceDate= lastServices.CreatedDate;
                            dtoyellow.NextServiceKMDiff = lastServices.NextServiceKM - lastServices.Vehicle.CurrentKM;
                            dtos.Add(dtoyellow);
                        }
                        else if ((lastServices.NextServiceKM - vehicle.CurrentKM) > 1000)
                        {
                            MaintenanceSpotlightDto dto = _mapper.Map<MaintenanceSpotlightDto>(lastServices);
                            dto.StatusMessage = "No requiere de servicio";
                            dto.StatusName = "OK";
                            dto.StatusColor = "#3ee80b";
                            dto.AlertType = StopLightAlert.VERDE;
                            dto.LastServiceId = lastServices.Id;
                            dto.NextServiceDate = lastServices.NextService;
                            dto.NextServiceKM = lastServices.NextServiceKM;
                            dto.Type = VehicleServiceType.Kilometraje;
                            dto.LastServiceDate= lastServices.CreatedDate;
                            dto.NextServiceKMDiff = lastServices.NextServiceKM - lastServices.Vehicle.CurrentKM;
                            dtos.Add(dto);
                        }
                    }
                    else
                    {
                        double differenceKM = vehicle.InitialKM / vehicle.ServicePeriodKM;
                        double periodAmountKM = Math.Ceiling((double)vehicle.CurrentKM / vehicle.ServicePeriodKM) * vehicle.ServicePeriodKM;
                        double KMForNextService = periodAmountKM - vehicle.CurrentKM;
                        if (vehicle.InitialKM == vehicle.CurrentKM)
                        {
                            MaintenanceSpotlightDto dtoNope = _mapper.Map<MaintenanceSpotlightDto>(vehicle);
                            dtoNope.StatusMessage = "No requiere de servicio";
                            dtoNope.StatusName = "OK";
                            dtoNope.StatusColor = "#3ee80b";
                            dtoNope.AlertType = StopLightAlert.VERDE;
                            dtoNope.Type = VehicleServiceType.Kilometraje;
                            dtoNope.NextServiceKMDiff = KMForNextService;
                            dtos.Add(dtoNope);
                        }
                        else if (KMForNextService <= 500)
                        {
                            MaintenanceSpotlightDto dtored = _mapper.Map<MaintenanceSpotlightDto>(vehicle);
                            dtored.Type = VehicleServiceType.Kilometraje;
                            dtored.StatusMessage = "Es requerido llevar el vehiculo a servicio";
                            dtored.StatusName = "SERVICIO NECESARIO!!";
                            dtored.StatusColor = "#e41212";
                            dtored.AlertType = StopLightAlert.ROJO;
                            dtored.NextServiceKMDiff = KMForNextService;
                            dtos.Add(dtored);
                        }
                        else if (KMForNextService <= 900 && KMForNextService > 500)
                        {
                            MaintenanceSpotlightDto dtoOrange = _mapper.Map<MaintenanceSpotlightDto>(vehicle);
                            dtoOrange.StatusMessage = "El vehiculo requiere de servicio pronto";
                            dtoOrange.StatusName = "ATENCIÓN!!";
                            dtoOrange.StatusColor = "#f3d132";
                            dtoOrange.AlertType = StopLightAlert.NARANJA;
                            dtoOrange.Type = VehicleServiceType.Kilometraje;
                            dtoOrange.NextServiceKMDiff = KMForNextService;
                            dtos.Add(dtoOrange);
                        }
                        else if (KMForNextService <= 1500 && KMForNextService > 900)
                        {
                            MaintenanceSpotlightDto dtoyellow = _mapper.Map<MaintenanceSpotlightDto>(vehicle);
                            dtoyellow.StatusMessage = "El vehiculo esta proximo a requerir servicio";
                            dtoyellow.StatusName = "ATENCIÓN";
                            dtoyellow.StatusColor = "#f3d132";
                            dtoyellow.AlertType = StopLightAlert.AMARILLO;
                            dtoyellow.Type = VehicleServiceType.Kilometraje;
                            dtoyellow.NextServiceKMDiff = KMForNextService;
                            dtos.Add(dtoyellow);
                        }
                        else if(KMForNextService > 1500)
                        {
                            MaintenanceSpotlightDto dto = _mapper.Map<MaintenanceSpotlightDto>(vehicle);
                            dto.StatusMessage = "No requiere de servicio";
                            dto.StatusName = "OK";
                            dto.StatusColor = "#3ee80b";
                            dto.AlertType = StopLightAlert.VERDE;
                            dto.Type = VehicleServiceType.Kilometraje;
                            dto.NextServiceKMDiff = KMForNextService;
                            dtos.Add(dto);
                        }

                        var timespan = vehicle.CreatedDate.Value.AddMonths(vehicle.ServicePeriodMonths) - DateTime.UtcNow;

                        switch (timespan.TotalDays)
                        {
                            case double d when d > 180:
                                MaintenanceSpotlightDto dto = _mapper.Map<MaintenanceSpotlightDto>(vehicle);
                                dto.Type = VehicleServiceType.Fecha;
                                dto.StatusMessage = "No requiere de servicio";
                                dto.StatusName = "OK";
                                dto.StatusColor = "#3ee80b";
                                dto.AlertType = StopLightAlert.VERDE;
                                dtos.Add(dto);
                                break;
                            case double d when d >= 90 && d <= 180:
                                MaintenanceSpotlightDto dtoyellow = _mapper.Map<MaintenanceSpotlightDto>(vehicle);
                                dtoyellow.Type = VehicleServiceType.Fecha;
                                dtoyellow.StatusMessage = "El vehiculo requiere de servicio pronto";
                                dtoyellow.StatusName = "ATENCIÓN";
                                dtoyellow.StatusColor = "#f3d132";
                                dtoyellow.AlertType = StopLightAlert.AMARILLO;
                                dtos.Add(dtoyellow);
                                break;
                            case double d when d >= 30 && d < 90:
                                MaintenanceSpotlightDto dtogreen = _mapper.Map<MaintenanceSpotlightDto>(vehicle);
                                dtogreen.Type = VehicleServiceType.Fecha;
                                dtogreen.StatusMessage = "El vehiculo requiere de servicio";
                                dtogreen.StatusName = "ATENCIÓN!!";
                                dtogreen.StatusColor = "#efbc38";
                                dtogreen.AlertType = StopLightAlert.NARANJA;
                                dtos.Add(dtogreen);
                                break;
                            case double d when d < 30:
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
            catch (Exception ex)
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
                var VehicleA = await _unitOfWork.VehicleReportUseRepo.Get(filter: status => status.StatusReportUse == ReportUseType.ViajeNormal || status.StatusReportUse == ReportUseType.ViajeRapido, includeProperties: "Vehicle,UserProfile,Destinations");
                if (VehicleA == null)
                {
                    response.success = false;
                    response.AddError("No existe Vehiculos Por Mostrar", "No Hay Vehiculos en Viaje actualmente", 2);
                    return response;
                }

                var dtos = _mapper.Map<List<GetVehicleActiveDto>>(VehicleA);
                response.success = true;
                response.Data = dtos;
                return response;

            }
            catch (Exception ex)
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
                var result = Rendimiento.FirstOrDefault();
                var list = new List<GraphicsPerfomanceDto>();

                if (Rendimiento == null)
                {
                    response.success = false;
                    response.AddError("No existe ", $"No existe Vehiculo con el Id {VehicleId}", 2);
                    return response;
                }

                if (Rendimiento.Count() == 0)
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
                        error = $"No existe rendimiento por reportes de Vehiculo {VehicleId} "
                    };

                    list.Add(Perfomance);

                }

                else
                {
                    foreach (var Aray in Rendimiento)
                    {
                        if (Aray.VehicleReportUses == null)
                        {
                            var Perfo = new GraphicsPerfomanceDto()
                            {
                                VehicleId = VehicleId,
                                VehicleName = "No se obtuvo informacion del documento solicitado",
                                CurrentKm = 0,
                                LastKm = 0,
                                GasolineLoadAmount = 0,
                                MileageTraveled = 0,
                                Perfomance = 0,
                                error = $"No existe rendimiento por reportes de Vehiculo {VehicleId} "
                            };

                            list.Add(Perfo);

                        }

                        else
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
                    response.AddError("No existe ", $"No existe Vehiculo con el Id {VehicleId}", 2);
                    return response;
                }

                if (Rendimiento.Count() == 0)
                {
                    var Totale = new TotalPerfomanceDto()
                    {
                        VehicleId = VehicleId,
                        VehicleName = "No se obtuvo informacion del documento solicitado",
                        TotalMileageTraveled = 0,
                        TotalPerfomance = 0,
                        error = $"No existe datos de Rendimiento para {VehicleId} "
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
                    double Dperfomance = 0;
                    foreach (var Aray in Rendimiento)
                    {
                        if (Aray.VehicleReportUses == null)
                        {
                            var Perfo = new GraphicsPerfomanceDto()
                            {
                                VehicleId = Aray.VehicleId,
                                VehicleName = "No Hay Datos Obtenidos de Este Reporte",
                                CurrentKm = 0,
                                LastKm = 0,
                                GasolineLoadAmount = 0,
                                MileageTraveled = 0,
                                Perfomance = 0
                            };

                            listt.Add(Perfo);


                        }

                        else
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
                            Dperfomance = (double)Aray.Vehicle.DesiredPerformance;

                        }

                    }
                    var Total = new TotalPerfomanceDto()
                    {
                        VehicleId = VehicleId,
                        VehicleName = Name,
                        TotalMileageTraveled = sum / Rendimiento.Count(),
                        TotalPerfomance = sum2 / Rendimiento.Count(),
                        DesiredPerfomance = Dperfomance
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

        public async Task<GenericResponse<PerformanceReviewDto>> GetListTotalPerfomance(ListTotalPerfomanceDto listTotalPerfomanceDto)
        {
            GenericResponse<PerformanceReviewDto> response = new GenericResponse<PerformanceReviewDto>();
            var review = new PerformanceReviewDto();
            var list = new List<TotalPerfomanceDto>();
            double totalCount = 0;
            double totalPerformance = 0;
            double totalMileage = 0;
            try
            {
                if (listTotalPerfomanceDto.VehicleId.Count > 0)
                {
                    foreach (var Enteros in listTotalPerfomanceDto.VehicleId)
                    {


                        var Rendimiento = await _unitOfWork.VehicleReportRepo.Get(filter: reportStatus => reportStatus.ReportType == ReportType.Carga_Gasolina && reportStatus.VehicleId == Enteros, includeProperties: "Vehicle,VehicleReportUses,Vehicle.VehicleImages");

                        var listt = new List<GraphicsPerfomanceDto>();

                        if (Rendimiento == null)
                        {
                            response.success = false;
                            response.AddError("No existe ", $"No existe Vehiculo con el Id {Enteros}", 2);
                            return response;
                        }

                        if (Rendimiento.Count() == 0)
                        {
                            var images = await _unitOfWork.VehicleImageRepo.Get(v => v.VehicleId == Enteros, includeProperties: "Vehicle");
                            var ImagesDto = _mapper.Map<List<VehicleImageDto>>(images);
                            var Totale = new TotalPerfomanceDto()
                            {
                                VehicleId = Enteros,
                                VehicleName = $"El vehiculo no contiene datos para mostrar",
                                TotalMileageTraveled = 0,
                                TotalPerfomance = 0,
                                success = false,
                                error = $"No existe datos de Rendimiento para {Enteros} "
                            };

                            list.Add(Totale);
                        }

                        else
                        {
                            var Name = Rendimiento.FirstOrDefault().Vehicle.Name;
                            double sum = 0;
                            double sum2 = 0;
                            double DPerfomance = 0;
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

                                DPerfomance = (double)Aray.Vehicle.DesiredPerformance;

                            }

                            var images = await _unitOfWork.VehicleImageRepo.Get(v => v.VehicleId == Enteros);
                            var ImagesDto = _mapper.Map<List<VehicleImageDto>>(images);
                            var Total = new TotalPerfomanceDto()
                            {
                                VehicleId = Enteros,
                                VehicleName = Name,
                                TotalMileageTraveled = sum / Rendimiento.Count(),
                                TotalPerfomance = sum2 / Rendimiento.Count(),
                                DesiredPerfomance = DPerfomance,
                                PerformanceDifference = (sum2 / Rendimiento.Count()) / DPerfomance,
                                Images = ImagesDto
                            };

                            list.Add(Total);

                        }
                    }

                    foreach (var total in list)
                    {
                        totalCount += 1;
                        totalPerformance += total.TotalPerfomance;
                        totalMileage += total.TotalMileageTraveled;
                    }
                    review.PerformanceAverage = totalPerformance / totalCount;
                    review.MileageAverage = totalMileage / totalCount;
                    review.PerformanceList = list;

                    response.success = true;
                    response.Data = review;
                    return response;
                }
                else
                {
                    var vehicles = await _unitOfWork.VehicleRepo.Get(v => v.VehicleStatus != VehicleStatus.INACTIVO);
                    foreach (var Enteros in vehicles.ToList())
                    {
                        var Rendimiento = await _unitOfWork.VehicleReportRepo.Get(filter: reportStatus => reportStatus.ReportType == ReportType.Carga_Gasolina && reportStatus.VehicleId == Enteros.Id && reportStatus.VehicleReportUses.StatusReportUse == ReportUseType.Finalizado, includeProperties: "Vehicle,VehicleReportUses,Vehicle.VehicleImages");

                        var listt = new List<GraphicsPerfomanceDto>();



                        if (Rendimiento == null)
                        {
                            response.success = false;
                            response.AddError("No existe ", $"No existe Vehiculo con el Id {Enteros.Id}", 3);
                            return response;
                        }

                        if (Rendimiento.Count() == 0)
                        {
                            var images = await _unitOfWork.VehicleImageRepo.Get(v => v.VehicleId == Enteros.Id, includeProperties: "Vehicle");
                            var ImagesDto = _mapper.Map<List<VehicleImageDto>>(images);
                            var Totale = new TotalPerfomanceDto()
                            {
                                VehicleId = Enteros.Id,
                                VehicleName = "El vehiculo no contiene datos para mostrar",
                                TotalMileageTraveled = 0,
                                TotalPerfomance = 0,
                                success = false,
                                error = $"No existe datos de Rendimiento para {Enteros.Id} "
                            };

                            list.Add(Totale);

                        }

                        else
                        {
                            var Name = Rendimiento.FirstOrDefault().Vehicle.Name;
                            double sum = 0;
                            double sum2 = 0;
                            double DPerfomance = 0;
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

                                DPerfomance = (double)Aray.Vehicle.DesiredPerformance;


                            }
                            var images = await _unitOfWork.VehicleImageRepo.Get(v => v.VehicleId == Enteros.Id);
                            var ImagesDto = _mapper.Map<List<VehicleImageDto>>(images);
                            var Total = new TotalPerfomanceDto()
                            {
                                VehicleId = Enteros.Id,
                                VehicleName = Name,
                                TotalMileageTraveled = sum / Rendimiento.Count(),
                                TotalPerfomance = sum2 / Rendimiento.Count(),
                                DesiredPerfomance = DPerfomance,
                                PerformanceDifference = (sum2 / Rendimiento.Count()) / DPerfomance,
                                Images = ImagesDto
                            };

                            list.Add(Total);

                        }

                    }

                    foreach (var total in list)
                    {
                        totalCount += 1;
                        totalPerformance += total.TotalPerfomance;
                        totalMileage += total.TotalMileageTraveled;
                    }
                    review.PerformanceAverage = totalPerformance / totalCount;
                    review.MileageAverage = totalMileage / totalCount;
                    review.PerformanceList = list;

                    response.success = true;
                    response.Data = review;
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

        public async Task<GenericResponse<List<GetUserForTravelDto>>> GetUserForTravel()
        {
            GenericResponse<List<GetUserForTravelDto>> response = new GenericResponse<List<GetUserForTravelDto>>();
            var list = new List<GetUserForTravelDto>();
            List<string> NameU = new List<string>();

            try
            {
                var travel = await _unitOfWork.VehicleReportUseRepo.Get(filter: status => status.StatusReportUse == ReportUseType.Finalizado);


                if (travel == null)
                {
                    response.success = false;
                    response.AddError("No existe ", $"No existe Reporte Solicitado", 2);
                    return response;
                }

                foreach (var usuario in travel)
                {
                    var user = await _unitOfWork.VehicleReportUseRepo.Get(filter: user => user.UserProfileId == usuario.UserProfileId && user.StatusReportUse == ReportUseType.Finalizado, includeProperties: "Vehicle,UserProfile");

                    if (usuario.UserProfileId == null)
                    {
                        var pub = new GetUserForTravelDto()
                        {

                            UserName = "No hay datos",
                            TripNumber = 0,
                            error = $"Este campo no tiene datos con el reporte {usuario.Id}"
                        };

                    }
                    else
                    {
                        var nombres = await _unitOfWork.VehicleReportUseRepo.Get(filter: user => user.UserProfileId == usuario.UserProfileId && user.StatusReportUse == ReportUseType.Finalizado, includeProperties: "Vehicle,UserProfile");
                        var resulv = nombres.Select(nom => nom.Vehicle.Name).ToList();

                        var L = list.LastOrDefault();

                        if (L == null)
                        {

                            //NameU.Add(n.Vehicle.Name);
                            var add = new GetUserForTravelDto()
                            {
                                VehicleName = resulv,
                                UserDriverId = usuario.UserProfileId ?? 0,
                                UserName = usuario.UserProfile.FullName,
                                TripNumber = user.Count(),
                                ProfileImageURL = usuario.UserProfile.ProfileImageUrl

                            };

                            list.Add(add);

                        }



                        var exist = list.Exists(x => x.UserDriverId == usuario.UserProfileId);

                        if (exist)
                        {


                        }


                        else
                        {


                            var add = new GetUserForTravelDto()
                            {
                                VehicleName = resulv,
                                UserDriverId = usuario.UserProfileId ?? 0,
                                UserName = usuario.UserProfile.FullName,
                                TripNumber = user.Count(),
                                ProfileImageURL = usuario.UserProfile.ProfileImageUrl
                            };

                            list.Add(add);

                        }

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

        public async Task<GenericResponse<List<GetServicesMaintenance>>> GetServiceMaintenance()
        {
            GenericResponse<List<GetServicesMaintenance>> response = new GenericResponse<List<GetServicesMaintenance>>();
            var list = new List<GetServicesMaintenance>();
            try
            {
                var Vehicle = await _unitOfWork.VehicleRepo.Get();
                foreach (var TotalVehicle in Vehicle)
                {
                    var Maintenance = await _unitOfWork.VehicleMaintenanceRepo.Get(filter: v => v.VehicleId == TotalVehicle.Id && v.Status == VehicleServiceStatus.FINALIZADO);
                    var Service = await _unitOfWork.VehicleServiceRepo.Get(filter: v => v.VehicleId == TotalVehicle.Id && v.Status == VehicleServiceStatus.FINALIZADO);

                    var resultT = new GetServicesMaintenance()
                    {
                        VehicleId = TotalVehicle.Id,
                        NameVehicle = TotalVehicle.Name,
                        TotalService = Service.Count(),
                        TotalMaintenance = Maintenance.Count(),

                    };

                    list.Add(resultT);

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

        public async Task<GenericResponse<UserInTravelDto>> IsUserInTravel(int userProfileId)
        {
            GenericResponse<UserInTravelDto> response = new GenericResponse<UserInTravelDto>();
            try
            {
                var dto = new UserInTravelDto();
                var useReport = await _unitOfWork.VehicleReportUseRepo.Get(u => u.UserProfileId == userProfileId && (u.StatusReportUse == ReportUseType.ViajeNormal || u.StatusReportUse == ReportUseType.ViajeRapido));
                var lastOne = useReport.LastOrDefault();

                if (lastOne != null)
                {
                    var car = await _unitOfWork.VehicleRepo.GetById(lastOne.VehicleId);

                    dto.UseReportId = lastOne.Id;
                    dto.IsInTravel = true;
                    dto.DriverId = userProfileId;
                    dto.VehicleQRId = car.VehicleQRId;
                }
                else
                {
                    dto.DriverId = userProfileId;
                    dto.IsInTravel = false;
                }

                response.success = true;
                response.Data = dto;
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
