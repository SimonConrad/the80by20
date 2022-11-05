using Microsoft.AspNetCore.Http;
using the80by20.Shared.Abstractions.Contexts;

namespace the80by20.Shared.Infrastucture.Context
{
    internal class ContextFactory : IContextFactory
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ContextFactory(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IContext Create()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            return httpContext is null ? Context.Empty : new Context(httpContext);
        }
    }
}
