using MediatR;
using MediatrCqrs.Domain;

namespace MediatrCqrs.Application.MediatR.Query;

public class GetAllProductHandler : IRequestHandler<GetAllProductQuery, List<Product>>
{
    [Tags("Loggable")]
    public async Task<List<Product>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
    {
        return new List<Product>()
        {
            new Product()
            {
                Id = 1, Name = "Camera", Price = 100
            },
            new Product()
            {
                Id = 2, Name = "Laptop", Price = 1000
            },
            new Product()
            {
                Id = 3, Name = "Monitor", Price = 250
            }
        };
    }
}