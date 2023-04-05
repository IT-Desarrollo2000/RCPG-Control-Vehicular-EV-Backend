namespace Domain.DTOs.Reponses
{
    public class GetExpensesDtoList
    {
        public int Id { get; set; }
        public int? DepartmentId { get; set; }
        public List<GetExpensesDto> expensesDtos { get; set; }
    }
}
