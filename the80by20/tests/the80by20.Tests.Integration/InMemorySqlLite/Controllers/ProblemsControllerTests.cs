// TODO make tests working with real database work, also on ci/cd sqllite ine memory has problems with applying migrations if multiple contexts and schemas

using System.Data;
using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using the80by20.Modules.Masterdata.App.Entities;
using the80by20.Modules.Solution.App.Problem.Commands;
using the80by20.Modules.Solution.Domain.Shared;
using the80by20.Modules.Users.Domain.UserEntity;
using the80by20.Modules.Users.Infrastructure.Security;
using the80by20.Shared.Infrastucture.Time;
using the80by20.Tests.Integration.Common;
using the80by20.Tests.Integration.InMemorySqlLite.Setup;
using Xunit;

namespace the80by20.Tests.Integration.InMemorySqlLite.Controllers;

// INFO to run tests sequentially - otherwise database problems
[Collection(nameof(SystemTestCollectionDefinition))]
public class ProblemsControllerTests : ControllerTests, IDisposable
{
    public SqliteConnection Connection { get; private set; }

    public ProblemsControllerTests(OptionsProvider optionsProvider) : base(optionsProvider)
    {
    }

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
    public async Task post_problem_should_return_201_problem_created_and_data_is_persisted_in_write_and_read_store()
    {
        SqlLiteIneMemoryManager.RecreateDbs(Connection);

        // Arrange
        var passwordManager = new PasswordManager(new PasswordHasher<User>());
        var clock = new Clock();
        const string password = "secret";

        var user = new User(Guid.NewGuid(), "test-user1@wp.pl",
            "test-user1", passwordManager.HashPassword(password), "Test Jon", Role.User(), clock.CurrentDate(), null, true);

        await SqlLiteIneMemoryManager.UsersDbContext.Users.AddAsync(user);
        await SqlLiteIneMemoryManager.UsersDbContext.SaveChangesAsync();

        // cannot find table name categories, for users it is working, maybe due to sqllite in memory schema name problems
        // // solution?- in dbcontext based on test-app-config set or not schema name?
        // read about other constarinats of inmemoemory sqlite, chnage to normal db and run integration test in container
        await SqlLiteIneMemoryManager.MasterDataDbContext.Categories.AddRangeAsync(GetCategories());
        await SqlLiteIneMemoryManager.MasterDataDbContext.SaveChangesAsync();


        // Act
        Authorize(user.Id, user.Role);

        string description = "I need help with creating model of the system, based on which I will do implmentation " +
                             "I would like to divide into domains, subbdomains and then into bounded context which can be implemented as modules. " +
                             "I would like to create model by utylising technique of event storming big picture, process and design level" +
                             "I would like to include in model models of aggregates which secure invariants, " +
                             "to achive small aggregates I would like to us edomain service to coordinate" +
                             "based on event of persisted state of aggregate  corresponding readmodels should be updated" +
                             "I would like to present on model waht architecture styles to use in each module and what kind of  messaging is between them";

        var command = new RequestProblemCommand(description,
            Guid.Parse("00000000-0000-0000-0000-000000000004"),
            user.Id,
            new SolutionType[] { SolutionType.TheoryOfConceptWithExample });

        // todo dont know how to test due to in CreateProblemCommandHandler which creates new scope _ = Task.Run(async () =>
        var response = await Client.PostAsJsonAsync("solution-to-problem/problems", command);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Created);

        var problemAggregateId = Guid.Parse(response.Headers.Location.Segments.Last());

        SqlLiteIneMemoryManager.SolutionDbContext.ProblemsAggregates.ShouldHaveSingleItem();
        SqlLiteIneMemoryManager.SolutionDbContext.ProblemsCrudData.ShouldHaveSingleItem();
        SqlLiteIneMemoryManager.SolutionDbContext.SolutionsToProblemsReadModel.ShouldHaveSingleItem();
    }

    public List<Category> GetCategories()
    {
        var categories = new List<Category>
        {
            Category.WithCustomId(Guid.Parse("00000000-0000-0000-0000-000000000001"), "typescript and angular"),
            Category.WithCustomId(Guid.Parse("00000000-0000-0000-0000-000000000002"), "css and html"),
            Category.WithCustomId(Guid.Parse("00000000-0000-0000-0000-000000000003"), "sql server"),
            Category.WithCustomId(Guid.Parse("00000000-0000-0000-0000-000000000004"), "system analysis"),
            Category.WithCustomId(Guid.Parse("00000000-0000-0000-0000-000000000005"), "buisness analysis"),
            Category.WithCustomId(Guid.Parse("00000000-0000-0000-0000-000000000006"), "architecture"),
            Category.WithCustomId(Guid.Parse("00000000-0000-0000-0000-000000000007"), "messaging"),
            Category.WithCustomId(Guid.Parse("00000000-0000-0000-0000-000000000008"), "docker"),
            Category.WithCustomId(Guid.Parse("00000000-0000-0000-0000-000000000009"), "craftsmanship"),
            Category.WithCustomId(Guid.Parse("00000000-0000-0000-0000-000000000010"), "tests"),
            Category.WithCustomId(Guid.Parse("00000000-0000-0000-0000-000000000011"), "ci / cd"),
            Category.WithCustomId(Guid.Parse("00000000-0000-0000-0000-000000000012"), "deployment"),
            Category.WithCustomId(Guid.Parse("00000000-0000-0000-0000-000000000013"), "azure"),
            Category.WithCustomId(Guid.Parse("00000000-0000-0000-0000-000000000014"), "aws"),
            Category.WithCustomId(Guid.Parse("00000000-0000-0000-0000-000000000015"), "monitoring"),
            Category.WithCustomId(Guid.Parse("00000000-0000-0000-0000-000000000016"), "support"),
            Category.WithCustomId(Guid.Parse("00000000-0000-0000-0000-000000000017"), ".net and c#")
        };

        return categories;
    }
}
