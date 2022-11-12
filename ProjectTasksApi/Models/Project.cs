namespace ProjectTasksApi.Models;

public class Project
{
    public int ID { get; set; }

    public string Name { get; set; }

    public ICollection<Task> Tasks { get; set; }
}
