﻿using MediatR;

namespace the80by20.App.Security.Commands;

public record SignUpCommand(Guid UserId, 
    string Email, 
    string Username, 
    string Password, 
    string FullName, 
    string Role) :  IRequest;