using MediatR;

namespace MediatrCqrs.Application.MediatR.Command;

public class ProductCountChangeHandler:INotificationHandler<ProductCountChangeCommand>
{
    public async Task Handle(ProductCountChangeCommand notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Update Product Id {notification.Id} To Database whit count {notification.Count}");
    }
}