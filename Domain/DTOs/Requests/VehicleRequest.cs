﻿using Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Requests
{
    public class VehicleRequest
    {
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
        [Required]
        public int FuelCapacity { get; set; }
        [Required]
        public FuelType FuelType { get; set; }
        [Required]
        public VehicleType VehicleType { get; set; }
        [Required]
        public int ServicePeriodMonths { get; set; }
        [Required]
        public int ServicePeriodKM { get; set; }
        [Required]
        public OwnershipType OwnershipType { get; set; }
        public string? OwnersName { get; set; }
        [Required]
        public decimal DesiredPerformance { get; set; }
        public List<int> AssignedDepartments { get; set; }
        public List<VehicleImageRequest> Images { get; set; }
    }

    public class VehicleImageRequest
    {
        [Required]
        public IFormFile ImageFile { get; set; }
    }
}