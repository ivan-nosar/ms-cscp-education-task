namespace ProjectTasksCosmosApi.Models.Dto;

using Newtonsoft.Json;

public class ProjectOutputDto
{
    public int ID { get; set; }

    public string PartitionKey { get; set; }

    public string Name { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public ICollection<TaskOutputDto>? Tasks { get; set; }
}
