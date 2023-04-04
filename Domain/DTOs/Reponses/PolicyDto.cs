using Domain.Entities.Registered_Cars;

namespace Domain.DTOs.Reponses
{
    public class PolicyDto
    {
        public int Id { get; set; }
        public string PolicyNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string NameCompany { get; set; }
        public ICollection<PhotosOfPolicy> PhotosOfPolicies { get; set; }
        public int? VehicleId { get; set; }
        public UnrelatedVehiclesDto Vehicle { get; set; }
    }
}
