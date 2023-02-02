using Application.Interfaces;
using AutoMapper;
using Domain.CustomEntities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        //public async Task<object> GetLicencesExpirations(LicenceExpStopLight request )
        //{
        //    GenericResponse<object> response = new GenericResponse<object>();

        //    try
        //    {
        //        //Consultar la lista de licencias
        //        //
        //        var licences = await _unitOfWork.UserProfileRepo.Get(l => l.LicenceExpirationDate != null);

        //        return null;
        //    } 
        //    catch(Exception ex)
        //    {
        //        response.success = true;

        //    }
        //}
    }
}
