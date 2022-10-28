using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.Exceptions;

namespace the80by20.Shared.Infrastucture.Exceptions
{
    public sealed class ErrorHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(ILogger<ErrorHandlerMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                await HandleErrorAsync(exception, context);
            }
        }

        private async Task HandleErrorAsync(Exception exception, HttpContext context)
        {
            var (statusCode, error) = exception switch
            {
                CustomException => 
                    (StatusCodes.Status400BadRequest, new Error(exception.GetType().Name.Underscore().Replace("_exception", string.Empty), exception.Message)),
                _ => 
                    (StatusCodes.Status500InternalServerError, new Error("error", "There was an error."))
            };

            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsJsonAsync(error);
        }

        private record Error(string Code, string Reason);
    }
}