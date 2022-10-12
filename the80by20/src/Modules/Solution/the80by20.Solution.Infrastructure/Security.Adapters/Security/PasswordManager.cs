using Microsoft.AspNetCore.Identity;
using the80by20.Solution.App.Security.Ports;
using the80by20.Solution.Domain.Security.UserEntity;

namespace the80by20.Solution.Infrastructure.Security.Adapters.Security
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

        public string Secure(string password) => _passwordHasher.HashPassword(default, password);

        public bool Validate(string password, string securedPassword)
            => _passwordHasher.VerifyHashedPassword(default, securedPassword, password) ==
               PasswordVerificationResult.Success;
    }
}