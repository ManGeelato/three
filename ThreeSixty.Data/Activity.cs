namespace ThreeSixty.Data
{
    public partial class Activity
    {
        public Activity()
        {
            ActivityAuditTrails = new HashSet<ActivityAuditTrail>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<ActivityAuditTrail> ActivityAuditTrails { get; set; }
    }
}
