using EventBus.Extensions;
using MediatR;
using Newtonsoft.Json;

namespace MediatrCqrs.Application.MediatR.Pipeline;

public class ExceptionPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<ExceptionPipelineBehavior<TRequest, TResponse>> _logger;

    public ExceptionPipelineBehavior(ILogger<ExceptionPipelineBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            _logger.LogInformation("----- Exception : {Message} {InnerException}", ex.Message, ex.InnerException??new object());
            throw;
        }
    }
}