using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Reponses
{
    public class GraphicsPerfomanceDto
    {
        public int VehicleId { get; set; }
        public string VehicleName { get; set; }
        public double CurrentKm { get; set; }
        public double LastKm { get; set; }
        public double GasolineLoadAmount { get; set; }
        public double MileageTraveled { get; set; }
        public double Perfomance { get; set; }
        public string? error { get; set; }
    }

    public class TotalPerfomanceDto
    {
        public int VehicleId { get; set;}
        public string VehicleName { get; set; }
        public double TotalMileageTraveled { get; set; }
        public double TotalPerfomance { get; set; }
        public string? error { get; set; }
    }

    public class ListTotalPerfomanceDto
    {
        public ListTotalPerfomanceDto()
        {
            VehicleId = new List<int>();
        }

        public List<int> VehicleId { get; set; }

    }
}
