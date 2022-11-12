namespace ProjectTasksApi.Models.Dto;

using System.ComponentModel.DataAnnotations;

public class ProjectInputDto
{
    [Required]
    public string Name { get; set; }
}
