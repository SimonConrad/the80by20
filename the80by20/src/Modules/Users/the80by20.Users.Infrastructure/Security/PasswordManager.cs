using Microsoft.AspNetCore.Identity;
using the80by20.Modules.Users.App.Ports;
using the80by20.Modules.Users.Domain.UserEntity;

namespace the80by20.Modules.Users.Infrastructure.Security
{
    // todo di internal sealed like in myspot-api
    //internal sealed class PasswordManager : IPasswordManager
    public class PasswordManager : IPasswordManager
    {
        private readonly IPasswordHasher<User> _passwordHasher;

        public PasswordManager(IPasswordHasher<User> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        public string HashPassword(string password) => _passwordHasher.HashPassword(default, password);

        public bool VerifyHashedPassword(string password, string securedPassword)
            => _passwordHasher.VerifyHashedPassword(default, securedPassword, password) ==
               PasswordVerificationResult.Success;
    }
}