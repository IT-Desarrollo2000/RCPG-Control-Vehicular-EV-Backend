using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Filters
{
    public class PropietaryFilter
    {
        public DateTime? CreatedAfterDate { get; set; }
        public DateTime? CreatedBeforeDate { get; set; }
        public string? DisplayName { get; set; }
        public string? Name { get; set; }
        public string? SurnameP { get; set; }
        public string? SurnameM { get; set; }
        public string? CompanyName { get; set; }
        public bool? IsMoralPerson { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
