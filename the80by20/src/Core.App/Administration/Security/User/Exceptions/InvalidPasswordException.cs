using Common.DDD;

namespace Core.App.Administration.Security.User.Exceptions;

public sealed class InvalidPasswordException : CustomException
{
    public InvalidPasswordException() : base("Invalid password.")
    {
    }
}