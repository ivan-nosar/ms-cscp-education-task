namespace ProjectTasksApi.Models;

public class Project
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public List<Task> Tasks { get; set; } = new List<Task>();
}
