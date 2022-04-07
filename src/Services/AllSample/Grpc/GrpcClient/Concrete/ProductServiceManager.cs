using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GRPCSample;

namespace GrpcClient.Concrete;

public class ProductServiceManager
{
    public readonly ProductService.ProductServiceClient _serviceClient;

    public ProductServiceManager(ProductService.ProductServiceClient serviceClient)
    {
        _serviceClient = serviceClient;
    }

    public async Task<ProductResponse> SetProductUnaryCount(ProductCount productCount)
    {
        return await _serviceClient.SetProductUnaryCountAsync(productCount);
    }
    public async Task<ProductCount> GetProductUnaryCount(ProductRequest productRequest)
    {
        return await _serviceClient.GetProductUnaryCountAsync(productRequest);
    }
    
    public async Task ServerStreamHello(int duration)
    {
        using (var caller = _serviceClient.ServerStreamHello(new DurationMessage(){Duration = duration}))
        {
            await foreach (var data in caller.ResponseStream.ReadAllAsync())
            {
                Console.WriteLine(data.Content);
            }
        }
    }
    public async Task ClientStreamHello(int duration)
    {
        using (var caller = _serviceClient.ClientStreamHello())
        {
            while (duration-- > 0)
            {
                Thread.Sleep(50);
                await caller.RequestStream.WriteAsync(
                    new Hello() {Content = "Client Stream Hello " + DateTime.Now.ToString()});
            }
            caller.RequestStream.CompleteAsync();
        }
    }

    public async Task BiDirectionalStreamHello(int duration)
    {
        using (var caller = _serviceClient.BiDirectionalStreamHello())
        {
            while (duration-- > 0)
            {
                Thread.Sleep(50);
                await caller.RequestStream.WriteAsync(
                    new Hello() {Content = "Client Stream Hello " + DateTime.Now.ToString()});
               if(await caller.ResponseStream.MoveNext())
                   Console.WriteLine(caller.ResponseStream.Current.Content);
                
            }
            caller.RequestStream.CompleteAsync();
        }
    }
}