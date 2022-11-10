namespace ProjectTasksApi.Servcies;

using ProjectTasksApi.Models;

public static class ProjectService
{
    static List<Project> ProjectStorage = new List<Project>();
    static int NextId = 1;

    public static IEnumerable<Project> GetAll() => ProjectStorage;

    public static Project? Get(int id) => ProjectStorage.FirstOrDefault(project => project.Id == id);

    public static void Add(Project project)
    {
        project.Id = NextId++;
        ProjectStorage.Add(project);
    }

    public static bool Update(int id, Project project)
    {
        int existingProjectIndex = ProjectStorage.FindIndex(project => project.Id == id);

        if (existingProjectIndex == -1)
        {
            return false;
        }

        ProjectStorage[existingProjectIndex] = project;

        return true;
    }

    public static bool Delete(int id)
    {
        Project? existingProject = Get(id);

        if (existingProject == null)
        {
            return false;
        }

        ProjectStorage.Remove(existingProject);

        return true;
    }

    public static bool AddTask(int projectId, Task task)
    {
        Project? existingProject = Get(projectId);

        if (existingProject == null)
        {
            return false;
        }

        existingProject.Tasks.Add(task);

        return true;
    }


    public static bool UpdateTask(int projectId, Task task)
    {
        Project? existingProject = Get(projectId);

        if (existingProject == null)
        {
            return false;
        }

        int existingTask = existingProject.Tasks.FindIndex(t => t.Id == task.Id);
        existingProject.Tasks[existingTask] = task;

        return true;
    }

    public static bool RemoveTask(int projectId, Task task)
    {
        Project? existingProject = Get(projectId);

        if (existingProject == null)
        {
            return false;
        }

        existingProject.Tasks.Remove(task);

        return true;
    }
}
