﻿namespace the80by20.App.Security.Ports;

public interface ITokenStorage
{
    void Set(JwtDto jwt);
    JwtDto Get();
}