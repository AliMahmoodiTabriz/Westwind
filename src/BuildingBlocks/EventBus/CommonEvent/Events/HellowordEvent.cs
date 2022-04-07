using EventBus.Events;

namespace CommonEvent.Events;

public record HellowordEvent:IntegrationEvent
{
    public HellowordEvent(string message)
    {
        this.message = message;
    }

    public string message { get;private set; }
}