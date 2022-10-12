using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using the80by20.Masterdata.App.CategoryCrud;
using the80by20.Shared.Abstractions.DomainLayer.SharedKernel.Capabilities;
using the80by20.Shared.Infrastucture.Time;
using Xunit;

namespace the80by20.Tests.Integration.Controllers;

public class ProblemsControllerTests : ControllerTests, IDisposable
{

    private IWithCoreDbContext _testDatabase;
    private SqliteConnection _connection;

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
        // comment when normal sql db used in tests beacouse then it is done using dtabaseinitializer class
        await _testDatabase.MasterDataDbContext.Categories.AddRangeAsync(GetCategories());
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
        var response = await Client.PostAsJsonAsync("solution-to-problem/problems", command);
        
        // Assert
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



    public ProblemsControllerTests(OptionsProvider optionsProvider) : base(optionsProvider)
    {
        // uncomment this and comment ApplySqlLite when using sql normal for tests
        //_testDatabase = new TestDatabase();
    }

    protected override void ConfigureServices(IServiceCollection services)
    {
        ApplySqlLite(services);
    }

    private void ApplySqlLite(IServiceCollection services)
    {
        var components = SqlLiteIneMemoryComponentsSetupper.Setup(services);
        _connection = components.connection;
        _testDatabase = components.ctxt;
    }

    public void Dispose()
    {
        _testDatabase.Dispose();
        _connection.Dispose();
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