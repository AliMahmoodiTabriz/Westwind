using EventBus.Extensions;
using MediatR;
using Newtonsoft.Json;

namespace MediatrCqrs.Application.MediatR.Pipeline;

public class LogPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LogPipelineBehavior<TRequest, TResponse>> _logger;
    public LogPipelineBehavior(ILogger<LogPipelineBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        _logger.LogInformation("----- Request : {Name} {@event}", request.GetGenericTypeName(), JsonConvert.SerializeObject(request));
        var respose = await next();
        _logger.LogInformation("----- Response : {Name} {@event}", respose.GetGenericTypeName(), JsonConvert.SerializeObject(respose));
        return respose;
    }


}