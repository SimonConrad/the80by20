﻿using Common;
using Core.App.SolutionToProblem.ReadModel;
using Core.Domain.SolutionToProblem.Operations;
using Core.Infrastructure.DAL.Administration;
using Core.Infrastructure.DAL.SolutionToProblem;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure.DAL;

// todo internal-visible like in my-spot
public static class Extensions
{
    private const string OptionsDataBaseName = "dataBase";

    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseOptions>(configuration.GetRequiredSection(OptionsDataBaseName));
        var dataBaseOptions = configuration.GetOptions<DatabaseOptions>(OptionsDataBaseName);

        if (dataBaseOptions.SqlLiteEnabled)
        {
            CoreDbContextFactory.OpenInMemorySqliteDatabaseConnection();
            services.AddDbContext<CoreDbContext>((serviceProvider, dbContextOptionsBuilder) =>
            {
                dbContextOptionsBuilder.UseSqlite(CoreDbContextFactory.Connection);
            });
        }
        else
        {
            services.AddDbContext<CoreDbContext>(x => x.UseSqlServer(dataBaseOptions.ConnectionString));
        }

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.AddScoped<ISolutionToProblemAggregateRepository, EfSolutionToProblemAggregateRepository>();
        services.AddScoped<ISolutionToProblemReadModelRepository, EfSolutionToProblemReadModelRepository>();

        services.AddHostedService<DatabaseInitializer>();

        return services;
    }
}
