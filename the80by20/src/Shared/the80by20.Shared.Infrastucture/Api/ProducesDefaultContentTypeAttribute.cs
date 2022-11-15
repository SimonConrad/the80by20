using Microsoft.AspNetCore.Mvc;

namespace the80by20.Shared.Infrastucture.Api
{
    public class ProducesDefaultContentTypeAttribute : ProducesAttribute
    {
        public ProducesDefaultContentTypeAttribute(Type type) : base(type)
        {
        }

        public ProducesDefaultContentTypeAttribute(params string[] additionalContentTypes)
            : base("application/json", additionalContentTypes)
        {
        }
    }
}
