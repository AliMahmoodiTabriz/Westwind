using Application.Core.Extensions;
using Autofac.Extensions.DependencyInjection;
using MediatR;
using MediatrCqrs.Application.MediatR.Pipeline;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Services.AddRabbitMQEventBus("172.16.0.207",5672,"MediatrCqrs");
var assembly = AppDomain.CurrentDomain.Load("MediatrCqrs");
builder.Services.AddMediatR(assembly);
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LogPipelineBehavior<,>));
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ExceptionPipelineBehavior<,>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapControllers();

app.Run();