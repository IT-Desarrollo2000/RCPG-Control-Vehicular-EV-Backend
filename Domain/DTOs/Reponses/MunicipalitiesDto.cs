using Domain.Entities.Registered_Cars;
using Domain.Entities.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.DTOs.Reponses
{
    public class MunicipalitiesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StateId { get; set; }
        [JsonIgnore]
        public virtual States States { get; set; }
        [JsonIgnore]
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
