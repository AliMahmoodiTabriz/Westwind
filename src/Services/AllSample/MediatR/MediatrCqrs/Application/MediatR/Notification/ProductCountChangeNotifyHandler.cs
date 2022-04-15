using CommonEvent.Events;
using EventBus.Abstractions;
using MediatR;
using MediatrCqrs.Application.MediatR.Command;

namespace MediatrCqrs.Application.MediatR.Notification;

public class ProductCountChangeNotifyHandler:INotificationHandler<ProductCountChangeCommand>
{
    private readonly IEventBus _eventBus;

    public ProductCountChangeNotifyHandler(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public async Task Handle(ProductCountChangeCommand notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Send data to event bus: Product Id {notification.Id} whit count {notification.Count}");
        _eventBus.Publish(new ProductCountChangeEvent(notification.Count));
    }
}