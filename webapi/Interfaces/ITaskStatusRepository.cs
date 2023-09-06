namespace webapi.Interfaces
{
    public interface ITaskStatusRepository
    {
        // Obtiene todas las tareas.
        IEnumerable<Models.TaskStatus> GetTasksStatus();
    }
}
