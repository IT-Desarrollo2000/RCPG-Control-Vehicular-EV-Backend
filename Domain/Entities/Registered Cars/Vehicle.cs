namespace Domain.Entities.Registered_Cars
{
    public class Vehicle : BaseEntity
    {
        public string Name { get; set; }
        public string Serial { get; set; }
        public bool IsUtilitary { get; set; }
        public string Color { get; set; }
        public string Brand { get; set; }
        public virtual ICollection<VehicleService> VehicleServices { get; set; }
    }
}
