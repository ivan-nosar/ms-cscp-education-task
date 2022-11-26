using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace ProjectTasksFunction
{
    public static class PopulateCosmosDbFunction
    {
        [FunctionName("PopulateCosmosDbFunction")]
        public static async Task Run([TimerTrigger("*/10 * * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
