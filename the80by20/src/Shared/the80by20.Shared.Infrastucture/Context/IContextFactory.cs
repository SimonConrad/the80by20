using the80by20.Shared.Abstractions.Contexts;

namespace the80by20.Shared.Infrastucture.Context
{
    internal interface IContextFactory
    {
        IContext Create();
    }
}
