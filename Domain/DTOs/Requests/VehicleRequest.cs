﻿using Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Requests
{
    public class VehicleRequest
    {
        public VehicleRequest()
        {
            DepartmentsToAssign = new List<int>();
            Images = new List<IFormFile>();
            CirculationCard = new List<IFormFile?>();
        }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Serial { get; set; }
        [Required]
        public bool IsUtilitary { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public int ModelYear { get; set; }
        public int? ChargeCapacityKwH { get; set; }
        public int? CurrentChargeKwH { get; set; }
        [Required]
        public VehicleType VehicleType { get; set; }
        [Required]
        public int ServicePeriodMonths { get; set; }
        [Required]
        public int ServicePeriodKM { get; set; }
        [Required]
        public OwnershipType OwnershipType { get; set; }
        public string? OwnersName { get; set; }
        //[Required]
        public decimal? DesiredPerformance { get; set; }
        //[Required]
        public int? CurrentKM { get; set; }
        public string? VehicleObservation { get; set; } = "";
        public string? CarRegistrationPlate { get; set; }
        public string? FuelCardNumber { get; set; }
        public string? VehicleResponsibleName { get; set; }
        [Required]
        public bool IsClean { get; set; } 
        public List<int> DepartmentsToAssign { get; set; }
        public List<IFormFile> Images { get; set; }
        public List<IFormFile?> CirculationCard { get; set; }
        public string? MotorSerialNumber { get; set; }
        public string? InvoiceNumber { get; set; }
        public int? PropietaryId { get; set; }
        public int? MunicipalityId { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? IVA { get; set; }
        public decimal? Total { get; set; }
        public bool? ResponsiveLetter { get; set; }
        public bool? DuplicateKey { get; set; }
        public IFormFile? VehicleInvoiceFile { get; set; }

    }

    public class VehicleImageRequest
    {
        [Required]
        public IFormFile ImageFile { get; set; }
    }


    public class CirculationCardRequest
    {
        public CirculationCardRequest() 
        {
            ImageFile = new List<IFormFile?>();  
        }

        [Required]
        public List<IFormFile?> ImageFile { get; set; }
    }



}
