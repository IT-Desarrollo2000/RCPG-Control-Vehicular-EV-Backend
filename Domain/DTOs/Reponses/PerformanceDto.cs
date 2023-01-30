using Domain.Entities.Registered_Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Reponses
{
    public class PerformanceDto
    {
        public  Vehicle Vehicle { get; set; }
        public decimal PerformanceOfVehicle { get; set; }
        
    }
}
