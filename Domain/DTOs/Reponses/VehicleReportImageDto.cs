namespace Domain.DTOs.Reponses
{
    public class VehicleReportImageDto
    {
        public int Id { get; set; }
        public int VehicleReportId { get; set; }
        public string FilePath { get; set; }
        public string FileUrl { get; set; }
    }
}
