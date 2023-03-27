using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Reponses
{
    public class AdminUserDto
    {
        public int Id { get; set; }
        //public string? Name { get; set; }
        //public string? LastNameP { get; set; }
        //public string? LastNameM { get; set; }
        public string? FullName { get; set; }
    }
}
