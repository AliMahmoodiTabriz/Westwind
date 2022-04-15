using MediatR;
using MediatrCqrs.Domain;

namespace MediatrCqrs.Application.MediatR.Command;

public class ProductPriceChangeHandler : IRequestHandler<ProductPriceChangeCommand, Product>
{
    public async Task<Product> Handle(ProductPriceChangeCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Update Product Id {request.Id} To Database whit price {request.Price}");
        return new Product() {Id = request.Id, Name = "Test", Price = request.Price};
    }
}