namespace Domain.Entities.Registered_Cars
{
    public class TypesOfExpenses : BaseEntity
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Expenses> Expenses { get; set; }

    }
}
