using GrpcServer.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(3100, listenOptions => listenOptions.Protocols = HttpProtocols.Http2); //grpc
    options.ListenAnyIP(3200, listenOptions => listenOptions.Protocols = HttpProtocols.Http1); //webapi
});

// Add services to the container.

builder.Services.AddGrpc();
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

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoint =>
{
    endpoint.MapControllers();
    endpoint.MapGrpcService<ProductGrpcServices>();
});
app.Run();