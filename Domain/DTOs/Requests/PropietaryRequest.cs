﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Requests
{
    public class PropietaryRequest
    {
        public string DisplayName { get; set; }
        public string Name { get; set; }
        public string? SurnameP { get; set; }
        public string? SurnameM { get; set; }
        public string? CompanyName { get; set; }
        public bool IsMoralPerson { get; set; }
    }

    public class PropietaryUpdateDto
    {
        public string? DisplayName { get; set; }
        public string? Name { get; set; }
        public string? SurnameP { get; set; }
        public string? SurnameM { get; set; }
        public string? CompanyName { get; set; }
        public bool? IsMoralPerson { get; set; }
    }
}
