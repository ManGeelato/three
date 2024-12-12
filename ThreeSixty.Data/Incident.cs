namespace ThreeSixty.Data
{
    public partial class Incident
    {
        public Incident()
        {
            IncidentHistories = new HashSet<IncidentHistory>();
        }

        public long Id { get; set; }
        public int IncidentTypeId { get; set; }
        public int? IncidentStatusId { get; set; }
        public int? IncidentStatusReasonId { get; set; }
        public long EntityId { get; set; }
        public string? ShortDescription { get; set; }
        public string LongDescription { get; set; } = null!;
        public string? LocationDescription { get; set; }
        public decimal? LocationXcoordinate { get; set; }
        public decimal? LocationYcoordinate { get; set; }
        public string? LocationLatLng { get; set; }
        public DateTime IncidentDate { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; } = null!;
        public DateTime LastModifiedDate { get; set; }

        public virtual Entity Entity { get; set; } = null!;
        public virtual IncidentStatus? IncidentStatus { get; set; }
        public virtual IncidentStatusReason? IncidentStatusReason { get; set; }
        public virtual IncidentType IncidentType { get; set; }
        public virtual ICollection<IncidentHistory> IncidentHistories { get; set; }
    }
}
