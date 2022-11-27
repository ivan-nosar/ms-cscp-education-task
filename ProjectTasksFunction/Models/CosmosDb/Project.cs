namespace ProjectTasksFunction.Models.CosmosDb;

using ProjectTasksFunction.Interfaces;
using System.Collections.Generic;

public class Project : IContainerEntity
{
    public int ID { get; set; }

    public string PartitionKey { get; set; }

    public string Name { get; set; }

    public ICollection<Task> Tasks { get; set; }
}

