using MediatR;

namespace MediatrCqrs.Application.MediatR.Command;

public class ProductCountChangeCommand:INotification
{
    public int Id { get; set; }
    public int Count { get; set; }
    
}