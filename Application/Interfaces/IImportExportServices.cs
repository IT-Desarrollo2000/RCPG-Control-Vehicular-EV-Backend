using Domain.CustomEntities;
using Domain.DTOs.Filters;
using Domain.DTOs.Reponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IImportExportServices
    {
        Task<PagedList<PolicyExportDto>> ExportVehiclePolicyData(PolicyExportFilter filter);
        Task<PagedList<VehicleExportDto>> ExportVehiclesData(VehicleExportFilter filter);
    }
}
