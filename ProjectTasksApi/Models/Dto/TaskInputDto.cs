namespace ProjectTasksApi.Models.Dto;

using System.ComponentModel.DataAnnotations;

public class TaskInputDto
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public int? ProjectID { get; set; }
}
