// TODO 
// make this test run on ci/cd pipeline
// run test contenrized so that test db server exists... https://hub.docker.com/_/microsoft-mssql-server

using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Identity;
using Shouldly;
using the80by20.Modules.Solution.App.Problem.Commands;
using the80by20.Modules.Solution.Domain.Shared;
using the80by20.Modules.Users.Domain.UserEntity;
using the80by20.Modules.Users.Infrastructure.Security;
using the80by20.Shared.Infrastucture.Time;
using the80by20.Tests.Integration.Common;
using the80by20.Tests.Shared;
using Xunit;

namespace the80by20.Tests.Integration.Solution.Controllers;

// INFO
// to run tests sequentially - otherwise database problems when it is accedd in parralel fromt tests
[Collection(nameof(SystemTestCollectionDefinition))]
public class ProblemsControllerTests : IClassFixture<TestApplicationFactory>,
    IClassFixture<TestUsersDbContext>, //created once 
                                       // INFO
                                       // in each testDbContext.Dispose():  DbContext?.Database.EnsureDeleted() and DbContext?.Dispose(); is called once,
                                       // after all tests in ProblemsControllerTests.cs
    IClassFixture<TestSolutionDbContext>,
    IClassFixture<TestMasterDataDbContext>
{
    private const string Path = "solution-to-problem/problems";

    private HttpClient _client;
    private TestUsersDbContext _usersDbContext;
    private TestSolutionDbContext _solutionDbContext;
    private TestMasterDataDbContext _masterdataDbContext;
    private User _user;

    public ProblemsControllerTests(TestApplicationFactory factory,
        TestUsersDbContext usersDbContext,
        TestSolutionDbContext solutionDbContext,
        TestMasterDataDbContext masterdataDbContext)
    {
        _client = factory
            .WithWebHostBuilder(builder => builder.ConfigureServices(services =>
            {
                // INFO
                // place to configure services, settings, ioc, environemnt, ...
                // like services.AddSingleton(_apiClient); to override existing registraion - last registraion wins
            }))
            .CreateClient();

        _usersDbContext = usersDbContext;
        _solutionDbContext = solutionDbContext;
        _masterdataDbContext = masterdataDbContext;

        _user = SetupUser();
    }


    //[Fact]
    public async Task post_problem_should_return_201_problem_created_and_data_is_persisted()
    {
        // arrange
        Authenticate(_user);

        // INFO
        // Bogus package can be use for creating test-data

        string description = "I need help with creating model of the system, based on which I will do implementation " +
                             "I would like to divide system into domains, subbdomains and then into bounded context which can be implemented as modules. " +
                             "I would like to create model by utilising technique of event storming big picture, process and design level" +
                             "I would like to include in model models of aggregates which secure invariants, " +
                             "to achive small aggregates I would like to use domain service to coordinate them" +
                             "I would like to use cqrs approach so that my event storming commands and readmodels map to command and queries in the implmentation" +
                             "Based on events ( which reflects state chnage of aggregate), module/application/domain events should be published to corresponding handlers" +
                             "I would like to present on model what architecture styles to use in each module and what kind of messaging is between them by using c4, adrs and data-flow diagrams";

        var command = new RequestProblemCommand(description,
            Guid.Parse("00000000-0000-0000-0000-000000000004"),
            _user.Id,
            new SolutionType[] { SolutionType.TheoryOfConceptWithExample });

        // act
        var response = await _client.PostAsJsonAsync($"{Path}", command);

        // assert
        response.StatusCode.ShouldBe(HttpStatusCode.Created);

        // INFO
        // response.Content.ReadFromJsonAsync<>()

        var problemAggregateId = Guid.Parse(response.Headers.Location.Query.Split("=").Last());
        _solutionDbContext.DbContext.ProblemsAggregates.Count(x => x.Id == problemAggregateId).ShouldBe(1);
        _solutionDbContext.DbContext.ProblemsCrudData.Count(x => x.AggregateId == problemAggregateId).ShouldBe(1);
        _solutionDbContext.DbContext.SolutionsToProblemsReadModel.Count(x => x.Id == problemAggregateId).ShouldBe(1);
    }

    // INFO 
    // samee to test running tests in parallel
    //[Fact]
    public async Task post_problem_should_return_201_problem_created_and_data_is_persisted2()
    {
        // arrange
        Authenticate(_user);

        // INFO
        // Bogus package can be use for creating test-data

        string description = "I need help with creating model of the system, based on which I will do implementation " +
                             "I would like to divide system into domains, subbdomains and then into bounded context which can be implemented as modules. " +
                             "I would like to create model by utilising technique of event storming big picture, process and design level" +
                             "I would like to include in model models of aggregates which secure invariants, " +
                             "to achive small aggregates I would like to use domain service to coordinate them" +
                             "I would like to use cqrs approach so that my event storming commands and readmodels map to command and queries in the implmentation" +
                             "Based on events ( which reflects state chnage of aggregate), module/application/domain events should be published to corresponding handlers" +
                             "I would like to present on model what architecture styles to use in each module and what kind of messaging is between them by using c4, adrs and data-flow diagrams";

        var command = new RequestProblemCommand(description,
            Guid.Parse("00000000-0000-0000-0000-000000000004"),
            _user.Id,
            new SolutionType[] { SolutionType.TheoryOfConceptWithExample });

        // act
        var response = await _client.PostAsJsonAsync($"{Path}", command);

        // assert
        response.StatusCode.ShouldBe(HttpStatusCode.Created);

        // INFO
        // response.Content.ReadFromJsonAsync<>()

        var problemAggregateId = Guid.Parse(response.Headers.Location.Query.Split("=").Last());
        _solutionDbContext.DbContext.ProblemsAggregates.Count(x => x.Id == problemAggregateId).ShouldBe(1);
        _solutionDbContext.DbContext.ProblemsCrudData.Count(x => x.AggregateId == problemAggregateId).ShouldBe(1);
        _solutionDbContext.DbContext.SolutionsToProblemsReadModel.Count(x => x.Id == problemAggregateId).ShouldBe(1);
    }

    private User SetupUser()
    {
        var passwordManager = new PasswordManager(new PasswordHasher<User>());
        var clock = new Clock();
        const string password = "secret";

        var user = new User(Guid.NewGuid(), "test-user1@wp.pl",
            "test-user1",
            passwordManager.HashPassword(password),
            "Test Jon",
            Role.User(),
            clock.CurrentDate(),
            claims: new() { ["permissions"] = new[] { "masterdata", "solution", "users" } },
            true);

        _usersDbContext.DbContext.Users.Add(user);
        _usersDbContext.DbContext.SaveChanges();
        return user;
    }

    private void Authenticate(User user)
    {
        var jwt = AuthHelper.GenerateJwt(userId: user.Id.Value.ToString(),
            role: user.Role, claims: user.Claims, email: user.Email);

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
    }
}
