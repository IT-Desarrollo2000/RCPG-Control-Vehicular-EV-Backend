namespace Domain.Entities.Registered_Cars
{
    public class Policy : BaseEntity
    {
        public Policy()
        {
            PhotosOfPolicies = new HashSet<PhotosOfPolicy>();
        }

        public string PolicyNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string NameCompany { get; set; }
        public ICollection<PhotosOfPolicy> PhotosOfPolicies { get; set; }
        public int? VehicleId { get; set; }
        public virtual Vehicle? Vehicle { get; set; }
        public int? ExpenseId { get; set; }
        public virtual Expenses? Expense { get; set; }
        public int? CurrentVehicleId { get; set; }
        public virtual Vehicle? CurrentVehicle { get; set; }
    }
}
