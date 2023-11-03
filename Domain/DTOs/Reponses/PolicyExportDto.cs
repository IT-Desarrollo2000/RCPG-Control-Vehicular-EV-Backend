using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Reponses
{
    public class PolicyExportDto
    {
        [DisplayName("N° DE VEHICULO")]
        public int Id { get; set; }
        [DisplayName("NOMBRE")]
        public string Name { get; set; }
        [DisplayName("COLOR")]
        public string Color { get; set; }
        [DisplayName("MARCA")]
        public string Brand { get; set; }
        [DisplayName("N° DE SERIE")]
        public string Serial { get; set; }
        [DisplayName("PLACAS")]
        public string? CarRegistrationPlate { get; set; }
        [DisplayName("N° DE MOTOR")]
        public string? MotorSerialNumber { get; set; }
        [DisplayName("N° DE POLIZA ACTUAL")]
        public string PolicyNumber { get; set; }
        [DisplayName("FECHA DE VENCIMIENTO - POLIZA ACTUAL")]
        public string ExpirationDate { get; set; }
        [DisplayName("ASEGURADORA - POLIZA ACTUAL")]
        public string NameCompany { get; set; }
        [DisplayName("COSTO - POLIZA ACTUAL")]
        public decimal? PolicyCostValue { get; set; }
        [DisplayName("N° DE POLIZA ANTERIOR")]
        public string OldPolicyNumber { get; set; }
        [DisplayName("FECHA DE VENCIMIENTO - POLIZA ANTERIOR")]
        public string OldExpirationDate { get; set; }
        [DisplayName("ASEGURADORA - POLIZA ANTERIOR")]
        public string OldNameCompany { get; set; }
        [DisplayName("COSTO - POLIZA ANTERIOR")]
        public decimal? OldPolicyCostValue { get; set; }
        [DisplayName("RESPONSABLE DEL VEHICULO")]
        public string? VehicleResponsibleName { get; set; }
    }
}
