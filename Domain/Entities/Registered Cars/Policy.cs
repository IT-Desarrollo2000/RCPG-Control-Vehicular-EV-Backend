namespace Domain.Entities.Registered_Cars
{
    public class Policy : BaseEntity
    {

        public string PolicyNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int? VehicleId { get; set; }
        public virtual Vehicle? Vehicle { get; set; }

    }
}
