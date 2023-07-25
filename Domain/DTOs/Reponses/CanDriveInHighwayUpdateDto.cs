using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Reponses
{
    public class CanDriveInHighwayUpdateDto
    {
        public int ProfileId { get; set; }
        public bool? CanDriveInHighway { get; set; }
    }
}
