using Domain.Entities.Registered_Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Reponses
{
    public class ExpensesDto
    {
        public int Id { get; set; }
        public virtual TypesOfExpenses TypesOfExpenses { get; set; }
        public decimal Cost { get; set; }
        public DateTime ExpenseDate { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public string MechanicalWorkshop { get; set; }
        public string ERPFolio { get; set; }
        public List<PhotosOfSpending> PhotosOfSpending { get; set; }

    }
}
