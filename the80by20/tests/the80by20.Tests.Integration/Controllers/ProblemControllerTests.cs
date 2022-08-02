using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using the80by20.App.Core.SolutionToProblem.Commands;
using the80by20.Domain.Security.UserEntity;
using the80by20.Domain.SharedKernel.Capabilities;
using the80by20.Infrastructure.Security.Adapters.Security;
using the80by20.Infrastructure.Time;
using Xunit;

namespace the80by20.Tests.Integration.Controllers;

public class ProblemControllerTests : ControllerTests, IDisposable
{
    [Fact]
    public async Task post_problem_should_return_201_problem_created_and_data_is_persisted_in_write_and_read_store()
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

        string description = "I need help with creating model of the system, based on which I will do implmentation " +
                             "I would like to divide into domains, subbdomains and then into bounded context which can be implemented as modules. " +
                             "I would like to create model by utylising technique of event storming big picture, process and design level" +
                             "I would like to include in model models of aggregates which secure invariants, " +
                             "to achive small aggregates I would like to us edomain service to coordinate" +
                             "based on event of persisted state of aggregate  corresponding readmodels should be updated" +
                             "I would like to present on model waht architecture styles to use in each module and what kind of  messaging is between them";

        var command = new CreateProblemCommand(description,
            Guid.Parse("00000000-0000-0000-0000-000000000004"),
            user.Id,
            new SolutionType[] { SolutionType.TheoryOfConceptWithExample });

        // todo dont know how to test due to in CreateProblemCommandHandler which creates new scope _ = Task.Run(async () =>
        var response = await Client.PostAsJsonAsync("solution-to-problem/problem", command);
        response.StatusCode.ShouldBe(HttpStatusCode.Created);

        var problemAggregateId = Guid.Parse(response.Headers.Location.Segments.Last());

        _testDatabase.Context.ProblemsAggregates.ShouldHaveSingleItem();
        _testDatabase.Context.ProblemsCrudData.ShouldHaveSingleItem();
        _testDatabase.Context.SolutionsToProblemsReadModel.ShouldHaveSingleItem();
    }

    private async Task ApplyMigrations()
    {
        if (!_testDatabase.Context.Database.GetPendingMigrations().Any())
        {
            await _testDatabase.Context.Database.MigrateAsync();
        }
    }

    private IWithCoreDbContext _testDatabase;

    public ProblemControllerTests(OptionsProvider optionsProvider) : base(optionsProvider)
    {
        _testDatabase = new TestDatabase();
    }

    protected override void ConfigureServices(IServiceCollection services)
    {
    }
    public void Dispose()
    {
        _testDatabase.Dispose();
    }
}