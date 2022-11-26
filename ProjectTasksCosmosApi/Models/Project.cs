namespace ProjectTasksCosmosApi.Models;

using ProjectTasksCosmosApi.Interfaces;

public class Project : IContainerEntity
{
    public int ID { get; set; }

    public string PartitionKey { get; set; }

    public string Name { get; set; }

    public ICollection<Task> Tasks { get; set; }
}
