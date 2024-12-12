namespace ThreeSixty.Data
{
    public partial class IncidentHistory
    {
        public long Id { get; set; }
        public long IncidentId { get; set; }
        public int IncidentStatusId { get; set; }
        public int IncidentStatusReasonId { get; set; }
        public string? Notes { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; } = null!;
        public DateTime LastModifiedDate { get; set; }

        public virtual Incident Incident { get; set; } = null!;
        public virtual IncidentStatus IncidentStatus { get; set; } = null!;
        public virtual IncidentStatusReason IncidentStatusReason { get; set; } = null!;
    }
}
