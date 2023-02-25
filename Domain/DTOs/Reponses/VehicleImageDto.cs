namespace Domain.DTOs.Reponses
{
    public class VehicleImageDto
    {
        public string FilePath { get; set; }
        public string FileURL { get; set; }
        public int Id { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; } = DateTime.UtcNow;
    }
}
