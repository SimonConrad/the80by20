using the80by20.App.Abstractions;

namespace the80by20.App.Security.Commands;


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