using Application.Core.Extensions;
using Autofac.Extensions.DependencyInjection;
using CommonEvent.Events;
using RabbitProduct.Application.RabbitHandlers;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
// Add services to the container.
builder.Services.AddRabbitMQEventBus("172.16.0.207",5672,"product");
builder.Services.AddScoped<ProductPriceChangeHandler>();

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
    bus.Subscribe<ProductPriceChangeEvent, ProductPriceChangeHandler>();
});

app.Run();