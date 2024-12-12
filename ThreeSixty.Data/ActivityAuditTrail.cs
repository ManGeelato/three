namespace ThreeSixty.Data
{
    public partial class ActivityAuditTrail
    {
        public long Id { get; set; }
        public int ActivityId { get; set; }
        public string? Details { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }

        public virtual Activity Activity { get; set; } = null!;
    }
}
