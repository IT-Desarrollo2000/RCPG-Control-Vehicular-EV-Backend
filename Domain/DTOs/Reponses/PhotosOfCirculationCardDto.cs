﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Reponses
{
    public class PhotosOfCirculationCardDto
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public string FileURL { get; set; }
        public int VehicleId { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; } = DateTime.UtcNow;
    }
}
