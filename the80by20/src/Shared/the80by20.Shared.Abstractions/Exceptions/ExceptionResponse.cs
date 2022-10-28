using System.Net;

namespace the80by20.Shared.Abstractions.Exceptions
{
    public record ExceptionResponse(object Response, HttpStatusCode StatusCode);
}
