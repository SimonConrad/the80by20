namespace the80by20.Shared.Abstractions.Exceptions
{
    public interface IExceptionCompositionRoot
    {
        ExceptionResponse Map(Exception exception);
    }
}
