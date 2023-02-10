using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Requests
{
    public class ReportUseTypeRequest
    {
        [Required]
        public ReportUseType StatusReportUse { get; set; }
        public double? FinalMileage { get; set; }
        public DateTime UseDate { get; set; }
        public CurrentFuel? CurrentFuelLoad { get; set; }
        public checklistdto? Checklist { get; set; }

        public class checklistdto
        {
            public bool CirculationCard { get; set; }
            public bool CarInsurancePolicy { get; set; }
            public bool HydraulicTires { get; set; }
            public bool TireRefurmishment { get; set; }
            public bool JumperCable { get; set; }
            public bool SecurityDice { get; set; }
            public bool Extinguisher { get; set; }
            public bool CarJack { get; set; }
            public bool CarJackKey { get; set; }
            public bool ToolBag { get; set; }
            public bool SafetyTriangle { get; set; }

        }
   
    }
}
