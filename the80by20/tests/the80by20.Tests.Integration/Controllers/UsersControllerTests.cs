using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shouldly;
using the80by20.App.Security.Commands;
using the80by20.App.Security.Ports;
using the80by20.App.Security.Queries;
using the80by20.Domain.Security.UserEntity;
using the80by20.Infrastructure.DAL.DbContext;
using the80by20.Infrastructure.DAL.Misc;
using the80by20.Infrastructure.Security.Adapters.Security;
using the80by20.Infrastructure.Time;
using Xunit;

namespace the80by20.Tests.Integration.Controllers;

public class UsersControllerTests : ControllerTests, IDisposable
{
    [Fact]
    public async Task post_users_should_return_created_201_status_code()
    {
        await ApplyMigrations();
        var command = new SignUpCommand(Guid.Empty, "test-user1@wp.pl", "test-user1", "secret",
            "Test Jon", "user");
        var response = await Client.PostAsJsonAsync("security/users", command);
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
    }

    [Fact]
    public async Task post_sign_in_should_return_ok_200_status_code_and_jwt()
    {
        // Arrange
        var passwordManager = new PasswordManager(new PasswordHasher<User>());
        var clock = new Clock();
        const string password = "secret";
        
        var user = new User(Guid.NewGuid(), "test-user1@wp.pl",
            "test-user1", passwordManager.Secure(password), "Test Jon", Role.User(), clock.Current());
        await _userRepository.AddAsync(user);
        // await _testDatabase.Context.Database.MigrateAsync();
        // await _testDatabase.Context.Users.AddAsync(user);
        // await _testDatabase.Context.SaveChangesAsync();

        // Act
        var command = new SignInCommand(user.Email, password);
        var response = await Client.PostAsJsonAsync("security/users/sign-in", command);
        var jwt = await response.Content.ReadFromJsonAsync<JwtDto>();

        // Assert
        jwt.ShouldNotBeNull();
        jwt.AccessToken.ShouldNotBeNullOrWhiteSpace();
    }


    [Fact]
    public async Task get_users_me_should_return_ok_200_status_code_and_user()
    {
        // Arrange
        var passwordManager = new PasswordManager(new PasswordHasher<User>());
        var clock = new Clock();
        const string password = "secret";
        
        var user = new User(Guid.NewGuid(), "test-user1@wp.pl",
            "test-user1", passwordManager.Secure(password), "Test Jon", Role.User(), clock.Current());


        await ApplyMigrations();
        await _testDatabase.Context.Users.AddAsync(user);
        await _testDatabase.Context.SaveChangesAsync();

        // Act
        Authorize(user.Id, user.Role);
        var userDto = await Client.GetFromJsonAsync<UserDto>("security/users/me");

        // Assert
        userDto.ShouldNotBeNull();
        userDto.Id.ShouldBe(user.Id.Value);
    }

    private async Task ApplyMigrations()
    {
        if (!_testDatabase.Context.Database.GetPendingMigrations().Any())
        {
            await _testDatabase.Context.Database.MigrateAsync();
        }
    }

    private IUserRepository _userRepository;
    
    private IWithCoreDbContext _testDatabase;
    private SqliteConnection _connection;

    public UsersControllerTests(OptionsProvider optionsProvider) : base(optionsProvider)
    {
        _testDatabase = new TestDatabase();

        // todo steer using appsetting - use options provider to access setting then ocmment:
        // _testDatabase = new TestDatabase();
    }

    protected override void ConfigureServices(IServiceCollection services)
    {
        _userRepository = new TestUserRepository();
        services.AddSingleton(_userRepository);

        // todo steer using appsetting - use options provider to access setting
        //ApplySqlLite(services);
    }

    private void ApplySqlLite(IServiceCollection services)
    {
        // for sqllite
        var descriptor1 = services.SingleOrDefault(
            d => d.ServiceType ==
                 typeof(DbContextOptions<CoreDbContext>));

        if (descriptor1 != null)
        {
            services.Remove(descriptor1);
        }

        //DatabaseInitializer
        var descriptor2 = services.SingleOrDefault(
            d => d.ServiceType ==
                typeof(IHostedService) && d.ImplementationType == typeof(DatabaseInitializer));
        if (descriptor2 != null)
        {
            services.Remove(descriptor2);
        }

        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();
        _testDatabase = new TestSqlLiteInMemoryDatabase(_connection);

        services.AddDbContext<CoreDbContext>(x => x.UseSqlite(_connection));
    }

    public void Dispose()
    {
        _testDatabase.Dispose();
    }
}