using Domain.Entities.Country;
using Domain.Entities.Municipality;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.DTOs.Reponses
{
    public class StatesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        [JsonIgnore]
        public virtual Countries Countries { get; set; }
        [JsonIgnore]
        public virtual ICollection<Municipalities> Municipalities { get; set; }
    }
}
