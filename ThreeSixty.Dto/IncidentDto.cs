namespace ThreeSixty.Dto
{
    public class IncidentDto
    {
        public IncidentDto()
        {
            Entities = new List<EntityDto>();
        }
        public long Id { get; set; }
        public int IncidentTypeId { get; set; }
        public string? IncidentTypeName { get; set; }
        public int IncidentStatusId { get; set; }
        public string? IncidentStatusName { get; set; }
        public long EntityId { get; set; }
        public string? FirstName { get; set; }
        public string? MobilePhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? LastNane { get; set; }
        public string? ShortDescription { get; set; }
        public string? LongDescription { get; set; }
        public decimal? LocationXcoordinate { get; set; }
        public decimal? LocationYcoordinate { get; set; }
        public string? LocationDescription { get; set; }
        public string? LocationLatLng { get; set; }
        public DateTime? IncidentDate { get; set; }       
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string LastModifiedBy { get; set; } = null!;
        public DateTime LastModifiedDate { get; set; } = DateTime.Now;
        public List<EntityDto> Entities { get; set; }
    }
}
