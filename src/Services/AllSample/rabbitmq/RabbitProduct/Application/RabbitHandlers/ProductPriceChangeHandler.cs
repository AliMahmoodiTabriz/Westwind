using CommonEvent.Events;
using EventBus.Abstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace RabbitProduct.Application.RabbitHandlers;

public class ProductPriceChangeHandler : IIntegrationEventHandler<ProductPriceChangeEvent>
{
    public async Task Handle(ProductPriceChangeEvent @event)
    {
        Console.WriteLine(JsonConvert.SerializeObject(@event));
    }
}