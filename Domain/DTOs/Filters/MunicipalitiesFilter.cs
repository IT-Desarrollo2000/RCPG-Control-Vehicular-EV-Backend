using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Filters
{
    public class MunicipalitiesFilter
    {
        public int? Id { get; set; }
        public string? name { get; set; }
        public int? StateId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
