using the80by20.Shared.Abstractions.Commands;

namespace the80by20.Modules.Users.App.Commands;


public record SignUp(Guid UserId,
    string Email,
    string Username,
    string Password,
    string FullName,
    string Role) : ICommand;


//public record SignUpCommand(Guid UserId, 
//    string Email, 
//    string Username, 
//    string Password, 
//    string FullName, 
//    string Role) :  IRequest;