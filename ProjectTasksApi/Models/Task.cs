namespace ProjectTasksApi.Models;

public class Task
{
    public int Id { get; set; }

    public int ProjectId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; }
    
    public DateTime UpdatedDate { get; set;}
}
