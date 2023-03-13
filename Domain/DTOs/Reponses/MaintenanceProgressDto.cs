using Domain.Entities;
using Domain.Entities.Identity;
using Domain.Entities.Profiles;
using Domain.Entities.Registered_Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Reponses
{
    public class MaintenanceProgressDto : BaseEntity
    {
        public MaintenanceProgressDto()
        {
            ProgressImages = new List<ProgressImageDto>();
        }
        public int VehicleMaintenanceId { get; set; }
        public string Comment { get; set; }
        public int? MobileUserId { get; set; }
        public string AdminUserName { get; set; }
        public int? AdminUserId { get; set; }
        public string? MobileUserName { get; set; }
        public List<ProgressImageDto> ProgressImages { get; set;}
    }

    public class ProgressImageDto : BaseEntity
    {
        public int ProgressId { get; set; }
        public string FilePath { get; set; }
        public string FileURL { get; set; }
    }
}
