namespace ThreeSixty.Data
{
    public partial class IncidentType
    {
        public IncidentType()
        {
            Incidents = new HashSet<Incident>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Incident> Incidents { get; set; }
    }
}
