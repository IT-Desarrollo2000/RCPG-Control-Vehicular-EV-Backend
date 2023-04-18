namespace Domain.Entities.Registered_Cars
{
    public class Policy : BaseEntity
    {

        public string PolicyNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string NameCompany { get; set; }
        public ICollection<PhotosOfPolicy> PhotosOfPolicies { get; set; }
        public int? VehicleId { get; set; }
        public virtual Vehicle? Vehicle { get; set; }
        //public int? ExpenseId { get; set; }
        //public virtual Expenses Expense { get; set; }
    }
}
