using the80by20.Shared.Abstractions.Messaging;

namespace the80by20.Shared.Infrastucture.Messaging
{
    internal interface IAsyncMessageDispatcher
    {
        Task PublishAsync<TMessage>(TMessage message) where TMessage : class, IMessage;
    }
}
