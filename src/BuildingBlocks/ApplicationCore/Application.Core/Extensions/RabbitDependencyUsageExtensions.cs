using EventBus.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Core.Extensions;

    public static class RabbitDependencyUsageExtensions
    {
        public static IApplicationBuilder UseRabbitMQEventBus(this IApplicationBuilder app, Action<IEventBus> action)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            action.Invoke(eventBus);
            return app;
        }
    }

