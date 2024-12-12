namespace ThreeSixty.Common;

public interface IHasDomainEvent
{
    List<DomainEvent> DomainEvents { get; set; }
}