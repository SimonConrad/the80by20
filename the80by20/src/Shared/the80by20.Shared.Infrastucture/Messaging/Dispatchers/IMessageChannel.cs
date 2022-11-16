using System.Threading.Channels;
using the80by20.Shared.Abstractions.Messaging;

namespace the80by20.Shared.Infrastucture.Messaging.Dispatchers
{
    public interface IMessageChannel
    {
        ChannelReader<IMessage> Reader { get; }
        ChannelWriter<IMessage> Writer { get; }
    }
}
