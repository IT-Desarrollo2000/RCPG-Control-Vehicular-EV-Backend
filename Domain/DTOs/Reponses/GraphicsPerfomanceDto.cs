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
        public TotalPerfomanceDto()
        {
            Images = new List<VehicleImageDto>();
        }
        public int VehicleId { get; set;}
        public string VehicleName { get; set; }
        public List<VehicleImageDto> Images { get; set; }
        public double TotalMileageTraveled { get; set; }
        public double TotalPerfomance { get; set; }
        public double DesiredPerfomance { get; set; }
        public double? PerformanceDifference { get; set; }
        public bool success { get; set; } = true;
        public string? error { get; set; }
    }

    public class PerformanceReviewDto
    {
        public PerformanceReviewDto()
        {
            PerformanceList = new List<TotalPerfomanceDto>();
        }

        public double MileageAverage { get; set; }
        public double PerformanceAverage { get; set; }
        public List<TotalPerfomanceDto> PerformanceList { get; set; }
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
