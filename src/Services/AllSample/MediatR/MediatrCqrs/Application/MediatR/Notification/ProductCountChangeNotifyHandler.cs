using MediatR;
using MediatrCqrs.Application.MediatR.Command;

namespace MediatrCqrs.Application.MediatR.Notification;

public class ProductCountChangeNotifyHandler:INotificationHandler<ProductCountChangeCommand>
{
    public async Task Handle(ProductCountChangeCommand notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Notify Product Id {notification.Id} whit count {notification.Count}");
    }
}