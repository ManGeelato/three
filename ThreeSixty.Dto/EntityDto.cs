namespace ThreeSixty.Dto
{
    public class EntityDto
    {
        public EntityDto()
        {
            Incidents = new List<IncidentDto>();
        }
        public long Id { get; set; } 
        public string? IdentityNumber { get; set; }
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? ThirdName { get; set; }
        public string? FourthName { get; set; }
        public string? FifthName { get; set; }
        public string? LastNane { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? HomePhoneNumber { get; set; }
        public string? WorkPhoneNumber { get; set; }
        public string? MobilePhoneNumber { get; set; }
        public string? EmailAddress { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; } = DateTime.Now;
        public List<IncidentDto> Incidents { get; set; }
    }
}
