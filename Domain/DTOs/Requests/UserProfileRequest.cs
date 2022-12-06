using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Requests
{
    public class UserProfileRequest
    {
        public string? UUID { get; set; }
        public string? FullName { get; set; }
        public string? Name { get; set; }
        public string? SurnameP { get; set; }
        public string? SurnameM { get; set; }
    }
}
