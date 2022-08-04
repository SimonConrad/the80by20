﻿using MediatR;
using Serilog;

namespace the80by20.Infrastructure.HandlersDecorators;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger _logger;

    public LoggingBehavior(ILogger logger)
    {
        _logger = logger;
    }
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        _logger.Debug($"before: {request.GetType().Name}");

        var response = await next();

        _logger.Debug($"after: {request.GetType().Name}");

        return response;
    }
}