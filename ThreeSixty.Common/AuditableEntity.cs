namespace ThreeSixty.Common
{
    public abstract class AuditableEntity
    {
        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int LastChangedBy { get; set; }

        public DateTime? LastChangedDate { get; set; }

    }
}
