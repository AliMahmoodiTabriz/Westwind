using MediatR;
using MediatrCqrs.Domain;

namespace MediatrCqrs.Application.MediatR.Command;

public class ProductPriceChangeCommand:IRequest<Product>
{
    public int Id { get; set; }
    public decimal Price { get; set; }
}