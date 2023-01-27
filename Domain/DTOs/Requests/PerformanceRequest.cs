using Domain.Entities.Registered_Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Requests
{
    public class PerformanceRequest
    {
        public int VehicleId { get; set; }
        public decimal CurrentKm { get; set; }
        public  decimal PreviousKm { get; set; }


    }
}
