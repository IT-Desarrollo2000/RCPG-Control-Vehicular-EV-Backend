using Domain.Entities.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.DTOs.Reponses
{
    public class CountriesDto
    {
        public int Id { get; set; }
        public string name { get; set; }
        [JsonIgnore]
        public virtual ICollection<States> States { get; set; }
    }
}
