using Domain.CustomEntities;
using Domain.DTOs.Filters;
using Domain.DTOs.Requests;
using Domain.Entities.User_Approvals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserApprovalServices
    {
        Task<GenericResponse<UserApproval>> CreateApproval(ApprovalCreationRequest request);
        Task<GenericResponse<bool>> DeleteApproval(int ApprovalId);
        Task<GenericResponse<UserApproval>> GetApprovalById(int ApprovalId);
        Task<PagedList<UserApproval>> GetApprovals(UserApprovalFilter filter);
        Task<GenericResponse<UserApproval>> ManageApproval(ApprovalManagementRequest request);
    }
}
