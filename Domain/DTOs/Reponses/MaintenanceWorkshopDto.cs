namespace Domain.DTOs.Reponses
{
    public class MaintenanceWorkshopDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ubication { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Telephone { get; set; }
        public virtual ICollection<VehicleMaintenanceDto> VehicleMaintenances { get; set; }
        public ICollection<ExpensesDto> Expenses { get; set; }
    }

    public class MaintenanceWorkShopSlimDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ubication { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Telephone { get; set; }
    }
}
