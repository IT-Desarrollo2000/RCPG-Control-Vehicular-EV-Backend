namespace Domain.DTOs.Requests
{
    public class MaintenanceWorkshopRequest
    {
        public string Name { get; set; }
        public string Ubication { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string Telephone { get; set; }

    }
}
