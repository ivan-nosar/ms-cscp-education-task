using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjectTasksFunction.Data;
using System;

[assembly: FunctionsStartup(typeof(ProjectTasksFunction.StartUp))]

namespace ProjectTasksFunction;

internal class StartUp : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        string connectionString = Environment.GetEnvironmentVariable("CosmosDbConnection");
        builder.Services.AddDbContext<ProjectTasksCosmosContext>(options =>
        {
            var databaseName = Environment.GetEnvironmentVariable("CosmosDbDatabaseName");
            options.UseCosmos(
                connectionString,
                databaseName: databaseName
            );
        });
    }
}
