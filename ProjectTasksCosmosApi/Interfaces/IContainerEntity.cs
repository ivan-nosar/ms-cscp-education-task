namespace ProjectTasksCosmosApi.Interfaces;

public interface IContainerEntity
{
    public int ID { get; set; }

    public string PartitionKey { get; set; }
}
