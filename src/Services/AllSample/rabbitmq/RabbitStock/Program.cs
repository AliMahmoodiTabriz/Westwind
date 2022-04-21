using Application.Core.Extensions;
using Application.Core.Utilities;
using Autofac.Extensions.DependencyInjection;
using CommonEvent.Events;
using RabbitStock.Application.RabbitHandlers;
using RedisCache;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
// Add services to the container.
builder.Services.AddRabbitMQEventBus("172.16.0.207",5672,"stock");
builder.Services.AddScoped<ProductCountChangeHandler>();
builder.Services.AddScoped<HelloWorldHandler>();
builder.Services.AddRedisCache("172.16.0.207:6379");
builder.Services.AddSingleton<CacheEventListener>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();
app.UseRabbitMQEventBus(bus =>
{
    bus.Subscribe<ProductCountChangeEvent, ProductCountChangeHandler>();
    bus.Subscribe<HellowordEvent,HelloWorldHandler>();
});
await app.UseRedisCacheListener("");
app.Run();


namespace RedisCache
{
    public static class Listener
    {
        public static async Task<IApplicationBuilder> UseRedisCacheListener(this IApplicationBuilder app, string prefix)
        {
            var listener = app.ApplicationServices.GetRequiredService<CacheEventListener>();
            await listener.ListenExpiredByPrefix(prefix);
            return app;
        }
    }
}