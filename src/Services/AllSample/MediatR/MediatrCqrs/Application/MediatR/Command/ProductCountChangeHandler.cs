using CommonEvent.Events;
using EventBus.Abstractions;
using MediatR;
using MediatrCqrs.Domain;

namespace MediatrCqrs.Application.MediatR.Command;

public class ProductCountChangeHandler:INotificationHandler<ProductCountChangeCommand>
{
    private readonly IEventBus _eventBus;

    public ProductCountChangeHandler(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public async Task Handle(ProductCountChangeCommand notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Update Product Id {notification.Id} To Database whit count {notification.Count}");
        _eventBus.Publish(new ProductCountChangeEvent(notification.Count));
    }
}