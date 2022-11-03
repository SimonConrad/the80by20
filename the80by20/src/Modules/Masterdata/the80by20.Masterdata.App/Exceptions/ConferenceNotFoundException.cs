using the80by20.Shared.Abstractions.Exceptions;

namespace the80by20.Modules.Masterdata.App.Exceptions
{
    public class ConferenceNotFoundException : The80by20Exception
    {
        public Guid Id { get; }

        public ConferenceNotFoundException(Guid id) : base($"conference with ID: '{id}' was not found.")
        {
            Id = id;
        }
    }
}
