namespace ThreeSixty.Data
{
    public partial class Entity
    {
        public Entity()
        {
            Incidents = new HashSet<Incident>();
        }

        public long Id { get; set; }
        public string? IdentityNumber { get; set; }
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? ThirdName { get; set; }
        public string? FourthName { get; set; }
        public string? FifthName { get; set; }
        public string? LastNane { get; set; }
        public string? RegistrationName { get; set; }
        public string? RegistrationNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? WorkPhoneNumber { get; set; }
        public string? MobilePhoneNumber { get; set; }
        public string? EmailAddress { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; } = null!;
        public DateTime LastModifiedDate { get; set; }

        public virtual ICollection<Incident> Incidents { get; set; }
    }
}
