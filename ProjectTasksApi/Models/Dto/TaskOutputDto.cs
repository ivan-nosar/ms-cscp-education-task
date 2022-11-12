namespace ProjectTasksApi.Models.Dto;

public class TaskOutputDto
{
    public int ID { get; set; }

    public int ProjectID { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }
}
