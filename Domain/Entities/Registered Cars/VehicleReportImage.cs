namespace Domain.Entities.Registered_Cars
{
    public class VehicleReportImage : BaseEntity
    {
        public int VehicleReportId { get; set; }
        public virtual VehicleReport? VehicleReport { get; set; }
        public string FilePath { get; set; }
        public string FileUrl { get; set; }


    }
}
