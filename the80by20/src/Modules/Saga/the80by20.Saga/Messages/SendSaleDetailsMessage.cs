using the80by20.Shared.Abstractions.Commands;

namespace the80by20.Saga.Messages;

internal record SendSaleDetailsMessage(string Email, string FullName) : ICommand;