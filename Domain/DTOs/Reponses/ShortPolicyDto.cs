using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Reponses
{
    public class ShortPolicyDto
    {
        public int Id { get; set; }
        public int? VehicleId { get; set; }
        public string PolicyNumber { get; set; }
        public string NameCompany { get; set; }
        public List<PhotosOfPolicyDto> PhotosOfPolicies { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
