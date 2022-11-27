namespace ProjectTasksFunction;

using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using ProjectTasksFunction.Data;
using Microsoft.EntityFrameworkCore;

public class PopulateCosmosDbFunction
{
    private readonly ProjectTasksCosmosContext cosmosContext;

    public PopulateCosmosDbFunction(ProjectTasksCosmosContext context)
    {
        this.cosmosContext = context;
    }

    [FunctionName("PopulateCosmosDbFunction")]
    public async System.Threading.Tasks.Task Run(
        [TimerTrigger("*/30 * * * * *")]TimerInfo myTimer,
        [
            Sql("select [ID], [Name] from dbo.Projects",
            CommandType = System.Data.CommandType.Text,
            ConnectionStringSetting = "AzureSqlConnection")
        ] IEnumerable<Models.AzureSql.Project> sqlProjects,
        [
            Sql("select [ID], [ProjectID], [Name], [Description] from dbo.Tasks",
            CommandType = System.Data.CommandType.Text,
            ConnectionStringSetting = "AzureSqlConnection")
        ] IEnumerable<Models.AzureSql.Task> sqlTasks,
        ILogger log
    )
    {
        log.LogInformation($"Starting syncing projects. Total count: {sqlProjects.Count()}");

        cosmosContext.Projects.RemoveRange
        (
            await cosmosContext.Projects.ToListAsync()
        );

        await cosmosContext.Projects.AddRangeAsync
        (
            sqlProjects.Select(sqlProject =>
            {
                var cosmosProject = new Models.CosmosDb.Project()
                {
                    ID = sqlProject.ID,
                    Name = sqlProject.Name,
                };
                cosmosContext.SetPartitionKey(cosmosProject);

                return cosmosProject;
            })
        );

        log.LogInformation($"Done syncing projects");


        log.LogInformation($"Starting syncing tasks. Total count: {sqlTasks.Count()}");

        cosmosContext.Tasks.RemoveRange
        (
            await cosmosContext.Tasks.ToListAsync()
        );

        await cosmosContext.Tasks.AddRangeAsync
        (
            sqlTasks.Select(sqlTask =>
            {
                var cosmosTask = new Models.CosmosDb.Task()
                {
                    ID = sqlTask.ID,
                    ProjectID = sqlTask.ProjectID,
                    Name = sqlTask.Name,
                    Description = sqlTask.Description,
                };
                cosmosContext.SetPartitionKey(cosmosTask);

                return cosmosTask;
            })
        );

        log.LogInformation($"Done syncing tasks");


        log.LogInformation($"Applying changes");

        await cosmosContext.SaveChangesAsync();

        log.LogInformation("Done applying changes");
    }
}
