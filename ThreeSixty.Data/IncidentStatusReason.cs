namespace ThreeSixty.Data
{
    public partial class IncidentStatusReason
    {
        public IncidentStatusReason()
        {
            IncidentHistories = new HashSet<IncidentHistory>();
            Incidents = new HashSet<Incident>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<IncidentHistory> IncidentHistories { get; set; }
        public virtual ICollection<Incident> Incidents { get; set; }
    }
}
