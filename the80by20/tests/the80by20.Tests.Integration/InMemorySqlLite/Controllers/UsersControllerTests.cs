// TODO make tests working with real database work, also on ci/cd sqllite ine memory has problems with applying migrations if multiple contexts and schemas

using System.Data;
using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using the80by20.Modules.Users.App.Commands;
using the80by20.Modules.Users.App.DTO;
using the80by20.Modules.Users.Domain.UserEntity;
using the80by20.Modules.Users.Infrastructure.Security;
using the80by20.Shared.Abstractions.Auth;
using the80by20.Shared.Infrastucture.Time;
using the80by20.Tests.Integration.Common;
using the80by20.Tests.Integration.InMemorySqlLite.Setup;
using Xunit;

namespace the80by20.Tests.Integration.InMemorySqlLite.Controllers;

// INFO to run tests sequentially - otherwise database problems
[Collection(nameof(SystemTestCollectionDefinition))]
public class UsersControllerTests : ControllerTests, IDisposable
{
    public UsersControllerTests(OptionsProvider optionsProvider) : base(optionsProvider)
    {
    }

    public SqliteConnection Connection { get; private set; }

    // INFO run before each tests - before UsersControllerTests as this method is inoked in ControllerTests
    protected override void ConfigureServices(IServiceCollection services)
    {
        Connection = new SqliteConnection("Filename=:memory:");
        Connection.Open();

        SqlLiteIneMemoryManager.SetupIoCContainer(services, Connection);
    }

    // INFO run after each test
    public void Dispose()
    {
        if (Connection.State == ConnectionState.Open)
        {
            Connection.Close();
            Connection.Dispose();
        }
    }

    //[Fact]
    public async Task post_users_should_return_created_201_status_code()
    {
        SqlLiteIneMemoryManager.RecreateDbs(Connection);

        var command = new SignUp(Guid.Empty, "test-user1@wp.pl", "test-user1", "secret",
            "Test Jon", "user", new Dictionary<string, IEnumerable<string>>());
        var response = await Client.PostAsJsonAsync("users/users", command);
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
    }

    //[Fact]
    public async Task post_sign_in_should_return_ok_200_status_code_and_jwt()
    {
        SqlLiteIneMemoryManager.RecreateDbs(Connection);

        // Arrange
        var passwordManager = new PasswordManager(new PasswordHasher<User>());
        var clock = new Clock();
        const string password = "secret";

        var user = new User(Guid.NewGuid(), "test-user1@wp.pl",
            "test-user1", passwordManager.HashPassword(password), "Test Jon", Role.User(), clock.CurrentDate(),
            new Dictionary<string, IEnumerable<string>>(), true);

        await SqlLiteIneMemoryManager.UsersDbContext.AddAsync(user);
        await SqlLiteIneMemoryManager.UsersDbContext.SaveChangesAsync();

        // Act
        var command = new SignIn(user.Email, password);
        var response = await Client.PostAsJsonAsync("users/users/sign-in", command);
        var jwt = await response.Content.ReadFromJsonAsync<JsonWebToken>();

        // Assert
        jwt.ShouldNotBeNull();
        jwt.AccessToken.ShouldNotBeNullOrWhiteSpace();
    }

    //[Fact]
    public async Task get_users_me_should_return_ok_200_status_code_and_user()
    {
        SqlLiteIneMemoryManager.RecreateDbs(Connection);

        // Arrange
        var passwordManager = new PasswordManager(new PasswordHasher<User>());
        var clock = new Clock();
        const string password = "secret";


        var claims = new Dictionary<string, IEnumerable<string>>()
        {
            ["permissions"] = new[] { "users" }
        };

        var user = new User(Guid.NewGuid(), "test-user1@wp.pl",
            "test-user1", passwordManager.HashPassword(password), "Test Jon", Role.User(), clock.CurrentDate(),
            claims, true);


        await SqlLiteIneMemoryManager.UsersDbContext.Users.AddAsync(user);
        await SqlLiteIneMemoryManager.UsersDbContext.SaveChangesAsync();

        // Act
        Authorize(user.Id, user.Role, claims: user.Claims, email: "test-user1@wp.pl");
        var userDto = await Client.GetFromJsonAsync<UserDto>("users/users/me");

        // Assert
        userDto.ShouldNotBeNull();
        userDto.Id.ShouldBe(user.Id.Value);
    }
}