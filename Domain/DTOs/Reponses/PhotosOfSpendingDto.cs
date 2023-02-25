using Domain.Entities.Registered_Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Reponses
{
    public class PhotosOfSpendingDto
    {
        public string FilePath { get; set; }
        public string FileURL { get; set; }
        public int ExpensesId { get; set; }
    }
}
