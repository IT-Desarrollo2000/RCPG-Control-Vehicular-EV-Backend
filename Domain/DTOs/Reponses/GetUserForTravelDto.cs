using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Reponses
{
    public class GetUserForTravelDto
    {
        public string VehicleName { get; set; }
        public string UserName { get; set; }
        public int TripNumber { get; set; }
    }
}
