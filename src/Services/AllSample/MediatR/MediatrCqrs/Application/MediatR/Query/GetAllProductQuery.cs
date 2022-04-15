using MediatR;
using MediatrCqrs.Domain;

namespace MediatrCqrs.Application.MediatR.Query;

public class GetAllProductQuery:IRequest<List<Product>>
{
    
}