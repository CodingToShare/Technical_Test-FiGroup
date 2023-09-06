
using webapi.ModelsDto;

namespace webapi.Interfaces
{
    public interface ITaskRepository
    {
        // Obtiene todas las tareas.
        IEnumerable<Models.Task> GetTasks(string UserName);

        // Inserta una nueva tarea.
        void InsertTask(TaskModel task, string UserName);

        // Actualiza una tarea existente.
        void UpdateTask(TaskModel task, string UserName, Guid id);

        // Elimina la tarea con el ID especificado.
        void DeleteTask(Guid id);

        // Guarda los cambios en la base de datos.
        void Save();
    }
}
