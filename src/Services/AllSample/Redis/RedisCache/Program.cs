using Application.Core.Extensions;
using Application.Core.Utilities;
using RedisCache;
using RedisCache.Application.Abstract;
using RedisCache.Application.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRedisCache("172.16.0.207:6379");
builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddControllers();
builder.Services.AddSingleton<CacheEventListener>();
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

app.MapControllers();

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
