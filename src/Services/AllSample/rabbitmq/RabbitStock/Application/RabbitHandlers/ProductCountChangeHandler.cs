using CommonEvent.Events;
using EventBus.Abstractions;
using Newtonsoft.Json;

namespace RabbitStock.Application.RabbitHandlers;

public class ProductCountChangeHandler : IIntegrationEventHandler<ProductCountChangeEvent>
{
    public async Task Handle(ProductCountChangeEvent @event)
    {
        Console.WriteLine(JsonConvert.SerializeObject(@event));
    }
}