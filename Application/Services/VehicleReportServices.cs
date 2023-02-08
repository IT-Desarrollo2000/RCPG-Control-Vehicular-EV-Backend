using Application.Extensions;
using Application.Interfaces;
using AutoMapper;
using Domain.CustomEntities;
using Domain.DTOs.Filters;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Identity;
using Domain.Entities.Registered_Cars;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;
using System.Runtime.Intrinsics.Arm;

namespace Application.Services
{
    public class VehicleReportServices : IVehicleReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly PaginationOptions _paginationOptions;
        private readonly UserManager<AppUser> _userManager;
        private readonly IOptions<BlobContainers> _azureBlobContainers;
        private readonly IBlobStorageService _blobStorageService;

        public VehicleReportServices( IUnitOfWork unitOfWork, IMapper mapper, IOptions<PaginationOptions> options, UserManager<AppUser> userManager, IOptions<BlobContainers> azureBlobContainers, IBlobStorageService blobStorageService) 
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            _paginationOptions = options.Value;
            _userManager = userManager;
            _azureBlobContainers = azureBlobContainers;
            _blobStorageService = blobStorageService;
        }

        //GetALL
        public async Task<PagedList<VehicleReport>> GetVehicleReportAll(VehicleReportFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            string properties = "";
            IEnumerable<VehicleReport> userApprovals = null;
            Expression<Func<VehicleReport, bool>> Query = null;

            if(filter.ReportType.HasValue)
            {
                if(Query != null )
                {
                    Query = Query.And(p => p.ReportType >= filter.ReportType.Value);
                }
                else { Query = p => p.ReportType >= filter.ReportType.Value;}
            }

            if (filter.VehicleId.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.VehicleId >= filter.VehicleId.Value);
                }
                else { Query = p => p.VehicleId >= filter.VehicleId.Value; }

            }


            if (filter.UserProfileId.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.UserProfileId >= filter.UserProfileId.Value);
                }
                else { Query = p => p.UserProfileId >= filter.UserProfileId.Value; }
            }

            if (filter.AppUserId.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.AppUserId >= filter.AppUserId.Value);
                }
                else { Query = p => p.AppUserId >= filter.AppUserId.Value; }
            }

            if (filter.ReportDate.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.ReportDate >= filter.ReportDate.Value);
                }
                else { Query = p => p.ReportDate >= filter.ReportDate.Value; }
            }

            if (filter.IsResolved.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.IsResolved == filter.IsResolved.Value);
                }
                else { Query = p => p.AppUserId == filter.AppUserId.Value; }
            }

            if (filter.GasolineLoad.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.GasolineLoad >= filter.GasolineLoad.Value);
                }
                else { Query = p => p.GasolineLoad >= filter.GasolineLoad.Value; }
            }

            if (filter.ReportStatus.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.ReportStatus >= filter.ReportStatus.Value);
                }
                else { Query = p => p.ReportStatus >= filter.ReportStatus.Value; }
            }

            if (filter.VehicleReportUseId.HasValue)
            {
                if(Query != null)
                {
                    Query = Query.And(p => p.VehicleReportUseId >= filter.VehicleReportUseId.Value);
                }
                else { Query = p => p.VehicleReportUseId >= filter.VehicleReportUseId.Value; }
            }


            if (Query != null)
            {
                userApprovals = await _unitOfWork.VehicleReportRepo.Get(filter: Query, includeProperties: "Vehicle,UserProfile,AppUser,Expenses,VehicleReportImages");
            }
            else
            {
                userApprovals = await _unitOfWork.VehicleReportRepo.Get(includeProperties: "Vehicle,UserProfile,AppUser,Expenses,VehicleReportImages");
            }

            var pagedApprovals = PagedList<VehicleReport>.Create(userApprovals, filter.PageNumber, filter.PageSize);

            return pagedApprovals;
        }

        //GETBYID
        public async Task<GenericResponse<VehicleReportDto>> GetVehicleReportById(int Id)
        {
            GenericResponse<VehicleReportDto> response = new GenericResponse<VehicleReportDto>();
            var profile = await _unitOfWork.VehicleReportRepo.Get(filter: p => p.Id == Id, includeProperties: "Vehicle,UserProfile,AppUser,Expenses,VehicleReportImages");
            var result = profile.FirstOrDefault();
            var VehicleReportDto = _mapper.Map<VehicleReportDto>(result);
            response.success = true;
            response.Data = VehicleReportDto;
            return response;
        }

        //Post 
        public async Task<GenericResponse<VehicleReportDto>> PostVehicleReport(VehicleReportRequest vehicleReportRequest)
        {
            GenericResponse<VehicleReportDto> response = new GenericResponse<VehicleReportDto>();
            if (vehicleReportRequest.ReportType == Domain.Enums.ReportType.Carga_Gasolina)
            {
                if (vehicleReportRequest.GasolineLoad == null)
                {
                    response.success = false;
                    response.AddError("Es necesario un valor existente, Kilometraje o Litros", $"Para el tipo de Carga de Gasolina, es necesario un valor existe de la carga de gasolina {vehicleReportRequest.GasolineLoad} solicitado", 1);
                    return response;
                }

                else if (vehicleReportRequest.GasolineLoad == Domain.Enums.GasolineLoadType.Kilometraje || vehicleReportRequest.GasolineLoad == Domain.Enums.GasolineLoadType.Litros)
                {
                    var entidadR = _mapper.Map<VehicleReport>(vehicleReportRequest);
                    await _unitOfWork.VehicleReportRepo.Add(entidadR);
                    await _unitOfWork.SaveChangesAsync();
                    response.success = true;
                    var VehicleReportDTOCG = _mapper.Map<VehicleReportDto>(entidadR);
                    response.Data = VehicleReportDTOCG;
                    return response;

                }
                else
                {
                    response.success = false;
                    response.AddError("No existe GasoLineLoad", $"No existe Gasolinead con el GasoLinead {vehicleReportRequest.GasolineLoad} solicitado", 1);
                    return response;
                }

            }
            else { vehicleReportRequest.GasolineLoad = null; }


            var existeVehicleMaintenance = await _unitOfWork.VehicleRepo.Get(c => c.Id == vehicleReportRequest.VehicleId);
            var resultVehicleMaintenance = existeVehicleMaintenance.FirstOrDefault();
            if (resultVehicleMaintenance == null)
            {
                response.success = false;
                response.AddError("No existe Vehicle", $"No existe Vehiculo con el VehicleId {vehicleReportRequest.VehicleId} solicitado", 1);
                return response;

            }

            if (vehicleReportRequest.UserProfileId.HasValue)
            {
                var existeUserProfile = await _unitOfWork.UserProfileRepo.Get(c => c.Id == vehicleReportRequest.UserProfileId.Value);
                var resultUserProfile = existeUserProfile.FirstOrDefault();

                if (resultUserProfile == null)
                {
                    response.success = false;
                    response.AddError("No existe UserProfile", $"No existe UserProfileId {vehicleReportRequest.UserProfileId} para cargar", 1);
                    return response;
                }

            } 
            if (vehicleReportRequest.AppUserId.HasValue)
            {
                var existeAppUser = await _userManager.Users.SingleOrDefaultAsync(c => c.Id == vehicleReportRequest.AppUserId.Value);
                if(existeAppUser == null)
                {
                    response.success = false;
                    response.AddError("No existe AppUser", $"No existe AppUserId {vehicleReportRequest.AppUserId} para cargar", 1);
                    return response;
                }
            
            }

           if ( vehicleReportRequest.VehicleReportUseId.HasValue)
            {
                var profileD = await _unitOfWork.VehicleReportUseRepo.Get(p => p.Id == vehicleReportRequest.VehicleReportUseId);
                var resultD = profileD.FirstOrDefault();

                if (resultD == null)
                {
                    response.success = true;
                    response.AddError("No existe VehicleReportUse", $"No existe ReportUse con el Id {vehicleReportRequest.VehicleReportUseId} solicitado ");
                    return response;
                }
            }
            var entidad = _mapper.Map<VehicleReport>(vehicleReportRequest);
            await _unitOfWork.VehicleReportRepo.Add(entidad);

            //Agregar imagenes al reporte
            //Guardar las fotos
            var images = new List<VehicleReportImage>();

            foreach (var image in vehicleReportRequest.ReportImages)
            {
                //Validar imagenes y Guardar las imagenes en el blobstorage
                if (image.ContentType.Contains("image"))
                {
                    //Manipular el nombre de archivo
                    var uploadDate = DateTime.UtcNow;
                    Random rndm = new Random();
                    string FileExtn = System.IO.Path.GetExtension(image.FileName);
                    var filePath = $"{entidad.Id}/{uploadDate.Day}{uploadDate.Month}{uploadDate.Year}_{uploadDate.Hour}{uploadDate.Minute}{rndm.Next(1, 1000)}{FileExtn}";
                    var uploadedUrl = await _blobStorageService.UploadFileToBlobAsync(image, _azureBlobContainers.Value.VehicleReports, filePath);

                    //Agregar la imagen en BD
                    var newImage = new VehicleReportImage()
                    {
                        FilePath = filePath,
                        FileUrl = await _blobStorageService.GetFileUrl(_azureBlobContainers.Value.VehicleReports, filePath),
                        VehicleReport = entidad
                    };

                    await _unitOfWork.VehicleReportImage.Add(newImage);
                    images.Add(newImage);
                }
                else
                {
                    response.success = false;
                    response.AddError("Archivo de Imagen Invalido", "Uno o mas archivos no corresponden a un archivo de Imagen");

                    return response;
                }
            }

            await _unitOfWork.SaveChangesAsync();
            response.success = true;
            var VehicleReportDTO = _mapper.Map<VehicleReportDto>(entidad);
            response.Data = VehicleReportDTO;
            return response;

        }

        //Put
        public async Task<GenericResponse<VehicleReportDto>> PutVehicleReport(int Id, VehicleReportRequest vehicleReportRequest)
        {
            GenericResponse<VehicleReportDto> response = new GenericResponse<VehicleReportDto>();
            var profile = await _unitOfWork.VehicleReportRepo.Get(p => p.Id == Id);
            var result = profile.FirstOrDefault();

            if (result == null)
            {
                response.success = true;
                response.AddError("No existe VehicleReport",$"No existe Report con el Id { Id } solicitado ");
                return response;
            }

            if (vehicleReportRequest.ReportType == Domain.Enums.ReportType.Carga_Gasolina)
            {
                if (vehicleReportRequest.GasolineLoad == null)
                {
                    response.success = false;
                    response.AddError("Es necesario un valor existente, Kilometraje o Litros", $"Para el tipo de Carga de Gasolina, es necesario un valor existe de la carga de gasolina {vehicleReportRequest.GasolineLoad} solicitado", 1);
                    return response;
                }

                else if (vehicleReportRequest.GasolineLoad == Domain.Enums.GasolineLoadType.Kilometraje || vehicleReportRequest.GasolineLoad == Domain.Enums.GasolineLoadType.Litros)
                {
                    result.ReportType = vehicleReportRequest.ReportType;
                    result.VehicleId = vehicleReportRequest.VehicleId;
                    result.Commentary = vehicleReportRequest.Commentary;
                    result.UserProfileId = vehicleReportRequest.UserProfileId;
                    result.AppUserId = vehicleReportRequest.AppUserId;
                    result.ReportDate = vehicleReportRequest.ReportDate;
                    result.IsResolved = vehicleReportRequest.IsResolved;
                    result.GasolineLoad = vehicleReportRequest.GasolineLoad;
                    result.ReportSolutionComment = vehicleReportRequest.ReportSolutionComment;
                    result.ReportStatus = vehicleReportRequest.ReportStatus;
                    result.VehicleReportUseId = vehicleReportRequest.VehicleReportUseId;

                    await _unitOfWork.VehicleReportRepo.Update(result);
                    await _unitOfWork.SaveChangesAsync();

                    var VehicleReportDto = _mapper.Map<VehicleReportDto>(result);
                    response.success = true;
                    response.Data = VehicleReportDto;

                    return response;


                }
                else
                {
                    response.success = false;
                    response.AddError("No existe GasoLineLoad", $"No existe Gasolinead con el GasoLinead {vehicleReportRequest.GasolineLoad} solicitado", 1);
                    return response;
                }

            }
            else { vehicleReportRequest.GasolineLoad = null; }

            var existeVehicleMaintenance = await _unitOfWork.VehicleRepo.Get(c => c.Id == vehicleReportRequest.VehicleId);
            var resultVehicleMaintenance = existeVehicleMaintenance.FirstOrDefault();
            if (resultVehicleMaintenance == null)
            {
                response.success = false;
                response.AddError("No existe Vehicle", $"No existe Vehiculo con el VehicleId {vehicleReportRequest.VehicleId} solicitado", 1);
                return response;

            }

            if (vehicleReportRequest.UserProfileId.HasValue)
            {
                var existeUserProfile = await _unitOfWork.UserProfileRepo.Get(c => c.Id == vehicleReportRequest.UserProfileId.Value);
                var resultUserProfile = existeUserProfile.FirstOrDefault();

                if (resultUserProfile == null)
                {
                    response.success = false;
                    response.AddError("No existe UserProfile", $"No existe UserProfileId {vehicleReportRequest.UserProfileId} para cargar", 1);
                    return response;
                }

            }
            if (vehicleReportRequest.AppUserId.HasValue)
            {
                var existeAppUser = await _userManager.Users.SingleOrDefaultAsync(c => c.Id == vehicleReportRequest.AppUserId.Value);
                if (existeAppUser == null)
                {
                    response.success = false;
                    response.AddError("No existe AppUser", $"No existe AppUserId {vehicleReportRequest.UserProfileId} para cargar", 1);
                    return response;
                }

            }
            if (vehicleReportRequest.VehicleReportUseId.HasValue)
            {
                var profileD = await _unitOfWork.VehicleReportUseRepo.Get(p => p.Id == vehicleReportRequest.VehicleReportUseId);
                var resultD = profileD.FirstOrDefault();

                if (resultD == null)
                {
                    response.success = true;
                    response.AddError("No existe VehicleReportUse", $"No existe ReportUse con el Id {vehicleReportRequest.VehicleReportUseId} solicitado ");
                    return response;
                }


                result.ReportType = vehicleReportRequest.ReportType;
                result.VehicleId = vehicleReportRequest.VehicleId;
                result.Commentary = vehicleReportRequest.Commentary;
                result.UserProfileId = vehicleReportRequest.UserProfileId;
                result.AppUserId = vehicleReportRequest.AppUserId;
                result.ReportDate = vehicleReportRequest.ReportDate;
                result.IsResolved = vehicleReportRequest.IsResolved;
                result.GasolineLoad = vehicleReportRequest.GasolineLoad;
                result.ReportSolutionComment = vehicleReportRequest.ReportSolutionComment;
                result.ReportStatus = vehicleReportRequest.ReportStatus;
                result.VehicleReportUseId = vehicleReportRequest.VehicleReportUseId;

                await _unitOfWork.VehicleReportRepo.Update(result);
                await _unitOfWork.SaveChangesAsync();

                var VehicleReportDto = _mapper.Map<VehicleReportDto>(result);
                response.success = true;
                response.Data = VehicleReportDto;

                return response;

            }
            else
            {
                result.ReportType = vehicleReportRequest.ReportType;
                result.VehicleId = vehicleReportRequest.VehicleId;
                result.Commentary = vehicleReportRequest.Commentary;
                result.UserProfileId = vehicleReportRequest.UserProfileId;
                result.AppUserId = vehicleReportRequest.AppUserId;
                result.ReportDate = vehicleReportRequest.ReportDate;
                result.IsResolved = vehicleReportRequest.IsResolved;
                result.GasolineLoad = vehicleReportRequest.GasolineLoad;
                result.ReportSolutionComment = vehicleReportRequest.ReportSolutionComment;
                result.ReportStatus = vehicleReportRequest.ReportStatus;
                result.VehicleReportUseId = vehicleReportRequest.VehicleReportUseId;

                await _unitOfWork.VehicleReportRepo.Update(result);
                await _unitOfWork.SaveChangesAsync();

                var VehicleReportDto = _mapper.Map<VehicleReportDto>(result);
                response.success = true;
                response.Data = VehicleReportDto;

                return response;
            }

        }

        //Delete
        public async Task<GenericResponse<VehicleReportDto>> DeleteVehicleReport(int Id)
        {
            GenericResponse<VehicleReportDto> response = new GenericResponse<VehicleReportDto>();
            var entidad = await _unitOfWork.VehicleReportRepo.Get(filter: p => p.Id == Id);
            var result = entidad.FirstOrDefault();
            if (result == null)
            {
                return null;
            }

            var existe = await _unitOfWork.VehicleReportRepo.Delete(Id);
            await _unitOfWork.SaveChangesAsync();

            var VehicleReportDTO = _mapper.Map<VehicleReportDto>(result);
            response.success = true;
            response.Data = VehicleReportDTO;

            return response;
        }

        //Agregar imagen al reporte invidividualmente
        public async Task<GenericResponse<VehicleReportImage>> AddReportImage(VehicleImageRequest request, int reportId)
        {
            GenericResponse<VehicleReportImage> response = new GenericResponse<VehicleReportImage>();

            try
            {
                //Verificar que exista el vehiculo
                var report = await _unitOfWork.VehicleReportRepo.GetById(reportId);
                if (report == null) return null;

                if (request.ImageFile.ContentType.Contains("image"))
                {
                    //Manipular el nombre de archivo
                    var uploadDate = DateTime.UtcNow;
                    Random rndm = new Random();
                    string FileExtn = System.IO.Path.GetExtension(request.ImageFile.FileName);
                    var filePath = $"{report.Id}/{uploadDate.Day}{uploadDate.Month}{uploadDate.Year}_{uploadDate.Hour}{uploadDate.Minute}{rndm.Next(1, 1000)}{FileExtn}";
                    var uploadedUrl = await _blobStorageService.UploadFileToBlobAsync(request.ImageFile, _azureBlobContainers.Value.VehicleReports, filePath);

                    //Agregar la imagen en BD
                    var newImage = new VehicleReportImage()
                    {
                        FilePath = filePath,
                        FileUrl = await _blobStorageService.GetFileUrl(_azureBlobContainers.Value.VehicleReports, filePath),
                        VehicleReport = report
                    };

                    await _unitOfWork.VehicleReportImage.Add(newImage);
                    await _unitOfWork.SaveChangesAsync();

                    response.success = true;
                    response.Data = newImage;

                    return response;
                }
                else
                {
                    response.success = false;
                    response.AddError("Archivo de Imagen Invalido", "Uno o mas archivos no corresponden a un archivo de Imagen");

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

        //Eliminar imagen del reporte individualmente
        public async Task<GenericResponse<bool>> DeleteReportImage(int reportImageId)
        {
            GenericResponse<bool> response = new GenericResponse<bool>();

            try
            {
                //Borrar las fotos del blob
                var photos = await _unitOfWork.VehicleImageRepo.GetById(reportImageId);
                if (photos == null) return null;

                await _blobStorageService.DeleteFileFromBlobAsync(_azureBlobContainers.Value.VehicleReports, photos.FilePath);
                await _unitOfWork.VehicleReportImage.Delete(photos.Id);
                await _unitOfWork.SaveChangesAsync();

                response.success = true;
                response.Data = true;
                return response;
            }
            catch (Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);

                response.AddError("Error", ex.Message, 1);
                return response;

            }
        }
    }
}
