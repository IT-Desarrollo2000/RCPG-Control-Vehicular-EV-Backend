using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Reponses
{
    public class GetExpensesDtoList
    {
        public int Id { get; set; }
        public List<GetExpensesDto> expensesDtos { get; set; }
    }
}
