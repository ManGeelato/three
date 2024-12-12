namespace ThreeSixty.Common
{
    public class DomainEvent
    {
        protected DomainEvent()
        {
            DateOccurred = DateTimeOffset.UtcNow;
        }

        public DateTimeOffset DateOccurred { get; protected set; } = DateTime.UtcNow;

        public bool IsPublished { get; set; }
    }
}
