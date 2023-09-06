using webapi.Data;
using webapi.Interfaces;

namespace webapi.Repositories
{
    public class TaskStatusRepository : ITaskStatusRepository
    {
        private readonly DataContext _dbContext;

        public TaskStatusRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Obtiene todos los estados de las tareas.
        public IEnumerable<Models.TaskStatus> GetTasksStatus()
        {
            return _dbContext.TaskStatus.ToList();
        }
    }
}
