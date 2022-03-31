using Autofac;
using EventBus;
using EventBus.Abstractions;
using EventBusRabbitMQ;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Application.Core.Extensions;

public static class RabbitDependencyInjectionExtensions
{
    public static IServiceCollection AddRabbitMQEventBus(this IServiceCollection services, string host,int port,string serviceName,int priority=9)
    {
        services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
        {
            var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();
            var factory = new ConnectionFactory()
            {
                HostName = host,
                Port = port,
                DispatchConsumersAsync = true
            };
            var retryCount = 5;
            return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
        });

        services.RabbitMQEventBus(serviceName,priority);
        return services;
    }
    private static IServiceCollection RabbitMQEventBus(this IServiceCollection services,string serviceName,int priority)
    {
        services.AddSingleton<IEventBus, EventBusRabbitMq>(sp =>
        {
            var subscriptionClientName = serviceName;
            var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
            var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
            var logger = sp.GetRequiredService<ILogger<EventBusRabbitMq>>();
            var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

            var retryCount = 5;
            return new EventBusRabbitMq(rabbitMQPersistentConnection, logger, iLifetimeScope, eventBusSubcriptionsManager, subscriptionClientName, retryCount, new Dictionary<String, Object> { { "x-priority",priority } });
        });

        services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
        return services;
    }

}