namespace Domain.Entities.Registered_Cars
{
    public class VehicleMaintenanceWorkshop : BaseEntity
    {
        public VehicleMaintenanceWorkshop()
        {
            this.VehicleMaintenances = new HashSet<VehicleMaintenance>();
        }

        public string Name { get; set; }
        public string Ubication { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string? Telephone { get; set; }
        public virtual ICollection<VehicleMaintenance> VehicleMaintenances { get; set; }
        public ICollection<Expenses> Expenses { get; set; }
    }
}
