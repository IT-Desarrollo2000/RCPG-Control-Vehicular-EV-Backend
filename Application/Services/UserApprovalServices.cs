using Application.Extensions;
using Application.Interfaces;
using AutoMapper;
using Domain.CustomEntities;
using Domain.DTOs.Filters;
using Domain.DTOs.Requests;
using Domain.Entities.Departament;
using Domain.Entities.User_Approvals;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;

namespace Application.Services
{
    public class UserApprovalServices : IUserApprovalServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOptions<BlobContainers> _azureBlobContainers;
        private readonly PaginationOptions _paginationOptions;
        private readonly IBlobStorageService _blobStorageService;
        private readonly IMapper _mapper;

        public UserApprovalServices(IUnitOfWork unitOfWork, IOptions<BlobContainers> azureBlobContainers, IBlobStorageService blobStorageService, IMapper mapper, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _azureBlobContainers = azureBlobContainers;
            _blobStorageService = blobStorageService;
            _mapper = mapper;
            _paginationOptions = options.Value;
        }

        public async Task<PagedList<UserApproval>> GetApprovals(UserApprovalFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            string properties = "Profile";
            IEnumerable<UserApproval> userApprovals = null;
            Expression<Func<UserApproval, bool>> Query = null;

            if (filter.ApprovalAfterDate.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.ApprovalDate >= filter.ApprovalAfterDate.Value);
                }
                else { Query = p => p.ApprovalDate >= filter.ApprovalAfterDate.Value; }
            }

            if (filter.ApprovalBeforeDate.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.ApprovalDate <= filter.ApprovalBeforeDate.Value);
                }
                else { Query = p => p.ApprovalDate <= filter.ApprovalBeforeDate.Value; }
            }

            if (filter.Status.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.Status == filter.Status.Value);
                }
                else { Query = p => p.Status == filter.Status.Value; }
            }

            if (filter.LicenceType.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.LicenceType == filter.LicenceType.Value);
                }
                else { Query = p => p.LicenceType == filter.LicenceType.Value; }
            }

            if (filter.LicenceValidityYears.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.LicenceValidityYears == filter.LicenceValidityYears.Value);
                }
                else { Query = p => p.LicenceValidityYears == filter.LicenceValidityYears.Value; }
            }

            if (filter.LicenceExpirationAfterDate.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.LicenceExpirationDate >= filter.LicenceExpirationAfterDate.Value);
                }
                else { Query = p => p.LicenceExpirationDate >= filter.LicenceExpirationAfterDate.Value; }
            }

            if (filter.LicenceExpirationBeforeDate.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.LicenceExpirationDate <= filter.LicenceExpirationBeforeDate.Value);
                }
                else { Query = p => p.LicenceExpirationDate <= filter.LicenceExpirationBeforeDate.Value; }
            }

            if (filter.LicenceExpeditionAfterDate.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.LicenceExpeditionDate >= filter.LicenceExpeditionAfterDate.Value);
                }
                else { Query = p => p.LicenceExpeditionDate >= filter.LicenceExpeditionAfterDate.Value; }
            }

            if (filter.LicenceExpeditionBeforeDate.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.LicenceExpeditionDate <= filter.LicenceExpeditionBeforeDate.Value);
                }
                else { Query = p => p.LicenceExpeditionDate <= filter.LicenceExpeditionBeforeDate.Value; }
            }

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

            if (Query != null)
            {
                userApprovals = await _unitOfWork.UserApprovalRepo.Get(filter: Query, includeProperties: properties);
            }
            else
            {
                userApprovals = await _unitOfWork.UserApprovalRepo.Get(includeProperties: properties);
            }

            var pagedApprovals = PagedList<UserApproval>.Create(userApprovals, filter.PageNumber, filter.PageSize);

            return pagedApprovals;
        }

        public async Task<GenericResponse<UserApproval>> GetApprovalById(int ApprovalId)
        {
            var response = new GenericResponse<UserApproval>();

            try
            {
                var query = await _unitOfWork.UserApprovalRepo.Get(filter: a => a.Id == ApprovalId, includeProperties: "Profile");
                var result = query.FirstOrDefault();
                if (result == null) return null;

                response.success = true;
                response.Data = result;

                return response;
            }
            catch (Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);
                return response;
            }
        }

        public async Task<GenericResponse<UserApproval>> CreateApproval(ApprovalCreationRequest request)
        {
            var response = new GenericResponse<UserApproval>();
            try
            {
                var userProfile = await _unitOfWork.UserProfileRepo.GetById(request.ProfileId);

                //Validar de que el perfil exista
                if (userProfile == null)
                {
                    response.success = false;
                    response.AddError("Profile not found", "No se pudo identificar el perfil de usuario solicitado", 2);

                    return response;
                }

                //Validar imagenes y Guardar las imagenes en el blobstorage
                if (request.DriversLicenceFrontFile.ContentType.Contains("image") && request.DriversLicenceBackFile.ContentType.Contains("image"))
                {
                    //Crear la entidad de aprobación
                    var newApproval = _mapper.Map<UserApproval>(request);
                    newApproval.Status = Domain.Enums.ApprovalStatus.PENDIENTE;

                    //Manipular el nombre de archivo
                    var uploadDate = DateTime.UtcNow;
                    string FileExtnFront = System.IO.Path.GetExtension(request.DriversLicenceFrontFile.FileName);
                    string FileExtnBack = System.IO.Path.GetExtension(request.DriversLicenceBackFile.FileName);
                    var filePathFront = $"{request.ProfileId}/{uploadDate.Day}{uploadDate.Month}{uploadDate.Year}_LicenceFront{FileExtnFront}";
                    var filePathBack = $"{request.ProfileId}/{uploadDate.Day}{uploadDate.Month}{uploadDate.Year}_LicenceBack{FileExtnBack}";
                    var uploadedUrlFront = await _blobStorageService.UploadFileToBlobAsync(request.DriversLicenceFrontFile, _azureBlobContainers.Value.DriverLicences, filePathFront);
                    var uploadedUrlBack = await _blobStorageService.UploadFileToBlobAsync(request.DriversLicenceBackFile, _azureBlobContainers.Value.DriverLicences, filePathBack);

                    newApproval.DriversLicenceFrontPath = filePathFront;
                    newApproval.DriversLicenceBackPath = filePathBack;
                    newApproval.DriversLicenceFrontUrl = await _blobStorageService.GetFileUrl(_azureBlobContainers.Value.DriverLicences, filePathFront);
                    newApproval.DriversLicenceBackUrl = await _blobStorageService.GetFileUrl(_azureBlobContainers.Value.DriverLicences, filePathBack);

                    await _unitOfWork.UserApprovalRepo.Add(newApproval);
                    await _unitOfWork.SaveChangesAsync();

                    response.success = true;
                    response.Data = newApproval;

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

        public async Task<GenericResponse<UserApproval>> ManageApproval(ApprovalManagementRequest request)
        {
            var response = new GenericResponse<UserApproval>();

            try
            {
                var query = await _unitOfWork.UserApprovalRepo.Get(filter: a => a.Id == request.ApprovalId, includeProperties: "Profile");
                var result = query.FirstOrDefault();
                if (result == null) return null;

                Departaments department = null;

                if (request.DepartmentId.HasValue)
                {
                    //Revisar que el departamento especificado exista
                    department = await _unitOfWork.Departaments.GetById(request.DepartmentId.Value);
                    if (department == null)
                    {
                        response.success = false;
                        response.AddError("Not found", $"El Id {request.DepartmentId} de departamento especificado no existe");

                        return response;
                    }
                }

                if (request.IsApproved)
                {
                    //obtener el perfil a aprobar
                    var profile = await _unitOfWork.UserProfileRepo.GetById(result.ProfileId);

                    //Modificar el perfil
                    _mapper.Map(result, profile);
                    profile.IsVerified = true;

                    //Modificar la solicitud
                    result.ApprovalDate = DateTime.UtcNow;
                    result.Status = Domain.Enums.ApprovalStatus.APROVADO;
                    result.Comment = request.Comment;

                    //Asignar un departamento al usuario
                    profile.Department = department;

                    //Guardar los cambios
                    await _unitOfWork.UserProfileRepo.Update(profile);
                    await _unitOfWork.UserApprovalRepo.Update(result);
                    await _unitOfWork.SaveChangesAsync();

                    response.success = true;
                    response.Data = result;

                    return response;
                }
                else
                {
                    //Modificar la solicitud
                    result.ApprovalDate = DateTime.UtcNow;
                    result.Status = Domain.Enums.ApprovalStatus.RECHAZADO;
                    result.Comment = request.Comment;

                    //Guardar los cambios
                    await _unitOfWork.UserApprovalRepo.Update(result);
                    await _unitOfWork.SaveChangesAsync();

                    response.success = true;
                    response.Data = result;

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

        public async Task<GenericResponse<bool>> DeleteApproval(int ApprovalId)
        {
            var response = new GenericResponse<bool>();

            try
            {
                var approval = await _unitOfWork.UserApprovalRepo.GetById(ApprovalId);
                if (approval == null)
                {
                    response.success = false;
                    response.Data = false;
                    response.AddError("Not Found", "No se encontro un elemento con el Id especificado", 2);

                    return response;
                }

                await _unitOfWork.UserApprovalRepo.Delete(approval.Id);
                await _unitOfWork.SaveChangesAsync();

                response.success = true;
                response.Data = true;

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
