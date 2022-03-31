using EventBus.Events;

namespace CommonEvent.Events;

public record ProductCountChangeEvent:IntegrationEvent
{
    public ProductCountChangeEvent(int count)
    {
        Count = count;
    }

    public int Count { get;private set; }
}