using CommonEvent.Events;
using EventBus.Abstractions;

namespace RabbitStock.Application.RabbitHandlers;

public class HelloWorldHandler:IIntegrationEventHandler<HellowordEvent>
{
    public async Task Handle(HellowordEvent @event)
    {
       Console.WriteLine(@event.message);
    }
}