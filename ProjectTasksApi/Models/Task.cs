﻿namespace ProjectTasksApi.Models;

public class Task
{
    public int ID { get; set; }

    public int ProjectID { get; set; }

    public Project Project { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }
}
