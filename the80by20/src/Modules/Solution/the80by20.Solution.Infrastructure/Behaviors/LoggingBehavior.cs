//using MediatR;
//using Microsoft.Extensions.Logging;
//using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;

//namespace the80by20.Modules.Solution.Infrastructure.Behaviors
//{
//    [HandlerDecorator]
//    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
//        where TRequest : IRequest<TResponse>
//    {
//        private readonly ILogger _logger;

//        public LoggingBehavior(ILogger<TRequest> logger)
//        {
//            _logger = logger;
//        }
//        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
//        {
//            _logger.LogDebug($"before: {request.GetType().Name}");

//            TResponse response = await next();

//            _logger.LogDebug($"after: {request.GetType().Name}");

//            return response;
//        }
//    }
//}