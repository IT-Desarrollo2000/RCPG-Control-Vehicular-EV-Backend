namespace Domain.DTOs.Reponses
{
    public class ProfileDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Name { get; set; }
        public string SurnameP { get; set; }
        public string SurnameM { get; set; }
        public string? ProfileImageUrl { get; set; }
        public DateTime? ProfileImageUploadDate { get; set; }
    }
}
