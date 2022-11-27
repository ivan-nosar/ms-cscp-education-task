namespace ProjectTasksFunction.Models.CosmosDb;

using ProjectTasksFunction.Interfaces;

public class Task : IContainerEntity
{
    public int ID { get; set; }

    public string PartitionKey { get; set; }

    public int ProjectID { get; set; }

    public Project Project { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }
}
