using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using webapi.Data;
using webapi.Interfaces;
using System.Security.Claims;
using webapi.ModelsDto;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly DataContext _dbContext;

        public TaskRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Elimina la tarea con el ID especificado.
        public void DeleteTask(Guid id)
        {
            var task = _dbContext.Tasks.Find(id);
            if (task == null)
            {
                throw new Exception($"No se encontró ninguna tarea con el ID {id}");
            }
            _dbContext.Tasks.Remove(task);
            Save();
        }

        // Obtiene todas las tareas.
        public IEnumerable<Models.Task> GetTasks(string UserName)
        {
            return _dbContext.Tasks.Where(x => x.CreatedBy == UserName).ToList();
        }

        // Inserta una nueva tarea.
        public void InsertTask(TaskModel taskModel, string UserName)
        {
            var task = new Models.Task() {
                TaskID = Guid.NewGuid(),
                TaskDescription = taskModel.TaskDescription,
                TaskStatusID = taskModel.TaskStatusID,
                Created = DateTime.UtcNow.AddHours(-5),
                CreatedBy = UserName,
                Modified = DateTime.UtcNow.AddHours(-5),
                ModifiedBy = UserName,
            };
            _dbContext.Add(task);
            Save();
        }

        // Guarda los cambios en la base de datos.
        public void Save()
        {
            _dbContext.SaveChanges();
        }

        // Actualiza una tarea existente.
        public void UpdateTask(TaskModel taskModel, string UserName, Guid id)
        {

            var task = _dbContext.Tasks.Where(x => x.TaskID == id).FirstOrDefault();
            if (task != null)
            {
                task.TaskDescription = taskModel.TaskDescription;
                task.TaskStatusID = taskModel.TaskStatusID;
                task.Modified = DateTime.UtcNow.AddHours(-5);
                task.ModifiedBy = UserName;
                _dbContext.Entry(task).State = EntityState.Modified;
                Save();
            }
        }
    }
}
