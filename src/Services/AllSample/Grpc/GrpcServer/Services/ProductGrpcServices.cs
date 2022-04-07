using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GRPCSample;

namespace GrpcServer.Services;

public class ProductGrpcServices : ProductService.ProductServiceBase
{
    public async override Task<ProductCount> GetProductUnaryCount(ProductRequest request, ServerCallContext context)
    {
        Console.WriteLine($"request => {request.Name}");
        return new ProductCount() {Count = 10, Name = request.Name};
    }

    public async override Task<ProductResponse> SetProductUnaryCount(ProductCount request, ServerCallContext context)
    {
        Console.WriteLine($"request => {request.Name} {request.Count}");
        return new ProductResponse() {Result = true};
    }

    public async override Task ServerStreamHello(DurationMessage request, IServerStreamWriter<Hello> responseStream,
        ServerCallContext context)
    {
        while (request.Duration-- > 0)
        {
            await responseStream.WriteAsync(new Hello()
            {
                Content = "Server Stream Hi " + DateTime.Now.ToString()
            });
            Thread.Sleep(50);
        }
    }

    public async override Task<Empty> ClientStreamHello(IAsyncStreamReader<Hello> requestStream,
        ServerCallContext context)
    {
        while (!context.CancellationToken.IsCancellationRequested)
        {
            if (await requestStream.MoveNext())
            {
                Console.WriteLine(requestStream.Current.Content);
            }
        }
        return new Empty();
    }

    public async override Task BiDirectionalStreamHello(IAsyncStreamReader<Hello> requestStream, IServerStreamWriter<Hello> responseStream,
        ServerCallContext context)
    {
        while (!context.CancellationToken.IsCancellationRequested)
        {
            if (await requestStream.MoveNext())
            {
                Console.WriteLine(requestStream.Current.Content);
                responseStream.WriteAsync(new Hello() {Content = "Server Stream Hi " + DateTime.Now.ToString()});
            }
        }
    }
}