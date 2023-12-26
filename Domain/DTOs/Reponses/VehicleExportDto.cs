using Domain.Entities.Propietary;
using Domain.Entities.Registered_Cars;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.DTOs.Reponses
{
    public class VehicleExportDto
    {
        public VehicleExportDto() 
        { 

        }
        [DisplayName("N° VEHICULO")]
        public int Id { get; set; }
        [DisplayName("MARCA")]
        public string Brand { get; set; }
        [DisplayName("DESCRIPCIÓN")]
        public string Name { get; set; }
        [DisplayName("AÑO")]
        public int ModelYear { get; set; }
        [DisplayName("COLOR")]
        public string Color { get; set; }
        [DisplayName("MÚMERO DE SERIE")]
        public string Serial { get; set; }
        [DisplayName("MÚMERO DE MOTOR")]
        public string? MotorSerialNumber { get; set; }
        [DisplayName("TIPO DE VEHÍCULO")]
        public VehicleType VehicleType { get; set; }
        [DisplayName("PLACAS")]
        public string? CarRegistrationPlate { get; set; }
        [DisplayName("ESTATUS DEL VEHÍCULO")]
        public VehicleStatus VehicleStatus { get; set; }
        [DisplayName("CAPACIDAD DE COMBUSTIBLE")]
        public int ChargeCapacityKwH { get; set; }
        [DisplayName("KILOMETRAJE ACTUAL")]
        public int CurrentKM { get; set; }
        [DisplayName("PERIODO DE SERVICIO (MESES)")]
        public int ServicePeriodMonths { get; set; }
        [DisplayName("PERIODO DE SERVICIO (KM)")]
        public int ServicePeriodKM { get; set; }
        [DisplayName("TIPO DE PROPIEDAD")]
        public OwnershipType OwnershipType { get; set; }
        [DisplayName("PROPIETARIO - RAZÓN SOCIAL")]
        public string? OwnersName { get; set; }
        [DisplayName("PROPIETARIO - NOMBRE")]
        public string? PropietaryName { get; set; }
        [DisplayName("RESPONSABLE DEL VEHÍCULO")]
        public string? VehicleResponsibleName { get; set; }
        [DisplayName("COMPAÑIA - DEPARTAMENTOS ASIGNADO")]
        public string? Departments { get; set; }
        [DisplayName("UBICACIÓN")]
        public string? Location { get; set; }
        [DisplayName("CARTA RESPONSIVA")]
        public bool? ResponsiveLetter { get; set; }
        [DisplayName("DUPLICADO DE LLAVE")]
        public bool? DuplicateKey { get; set; }
        [DisplayName("PÓLIZA ACTUAL")]
        public string? CurrentPolicy { get; set; }
        [DisplayName("FECHA DE VENCIMIENTO DE PÓLIZA ACTUAL")]
        public string? ExpirationDate { get; set; }
        [DisplayName("ASEGURADORA ACTUAL")]
        public string NameCompany { get; set; }
        [DisplayName("COSTO DE PÓLIZA ACTUAL")]
        public decimal? PolicyCostValue { get; set; }
        [DisplayName("FECHA ULTIMO SERVICIO")]
        public string? LastServiceDate { get; set; }
        [DisplayName("FECHA PROXIMO SERVICIO")]
        public string? NextServiceDate { get; set; }
        [DisplayName("SUBTOTAL")]
        public decimal? SubTotal { get; set; }
        [DisplayName("IVA")]
        public decimal? IVA { get; set; }
        [DisplayName("TOTAL")]
        public decimal? Total { get; set; }
        [DisplayName("NÚMERO DE FACTURA")]
        public string? InvoiceNumber { get; set; }
        
        [DisplayName("OBSERVACIONES")]
        public string? VehicleObservation { get; set; }
        [DisplayName("ES UTILITARIO")]
        public bool IsUtilitary { get; set; }
    }

    public class ExportPolicyDto
    {
        public int Id { get; set; }
        public string PolicyNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string NameCompany { get; set; }
        public List<PhotosOfPolicyDto> PhotosOfPolicies { get; set; }
        public decimal? PolicyCostValue { get; set; }
    }
}
