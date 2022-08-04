﻿using MediatR;
using Serilog;
using the80by20.App;
using the80by20.Common.ArchitectureBuildingBlocks.MarkerAttributes;

namespace the80by20.Infrastructure.HandlersDecorators;

[HandlerDecorator]
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

        TResponse response = await next();

        _logger.Debug($"after: {request.GetType().Name}");

        return response;
    }
}