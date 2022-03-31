using EventBus.Events;

namespace CommonEvent.Events;

public record ProductPriceChangeEvent:IntegrationEvent
{
    public ProductPriceChangeEvent(decimal price)
    {
        Price = price;
    }

    public decimal Price { get;private set; }
}