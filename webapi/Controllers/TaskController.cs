using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using webapi.Interfaces;
using webapi.ModelsDto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace webapi.Controllers
{
    // Controlador que maneja las operaciones CRUD para las tareas  
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        // Constructor del controlador que recibe una implementación del repositorio de tareas  
        public TaskController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        // Obtener todas las tareas  
        [HttpGet]
        public IActionResult Get()
        {
            if (User.Identity != null)
            {
                var userName = User.Identity.Name;
                if (userName != null)
                {
                    var tasks = _taskRepository.GetTasks(userName);
                    return new OkObjectResult(tasks);
                }
                else
                {
                    // Manejar el caso en el que userName es nulo, tal vez devolver un error o realizar alguna otra acción apropiada.
                    return new NotFoundResult();
                }
               
            }
            else
            {
                // Manejar el caso en el que User.Identity es nulo, tal vez devolver un error o realizar alguna otra acción apropiada.
                return new UnauthorizedResult();
            }
        }

        // Agregar una nueva tarea  
        [HttpPost]
        public IActionResult Post([FromBody] TaskModel task)
        {
            using (var scope = new TransactionScope())
            {
                if (User.Identity != null)
                {
                    var userName = User.Identity.Name;
                    if (userName != null)
                    {
                        _taskRepository.InsertTask(task, userName);
                        scope.Complete();
                        return new OkResult();
                    }
                    else
                    {
                        // Manejar el caso en el que userName es nulo, tal vez devolver un error o realizar alguna otra acción apropiada.
                        return new NotFoundResult();
                    }
                }
                else
                {
                    // Manejar el caso en el que User.Identity es nulo, tal vez devolver un error o realizar alguna otra acción apropiada.
                    return new UnauthorizedResult();
                }
            }
        }

        // Actualizar una tarea existente  
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] TaskModel task, Guid id)
        {
            if (task != null)
            {
                using (var scope = new TransactionScope())
                {
                    if (User.Identity != null)
                    {
                        var userName = User.Identity.Name;
                        if (userName != null)
                        {
                            _taskRepository.UpdateTask(task, userName, id);
                            scope.Complete();
                            return new OkResult();
                        }
                        else
                        {
                            // Manejar el caso en el que userName es nulo, tal vez devolver un error o realizar alguna otra acción apropiada.
                            return new NotFoundResult();
                        }
                    }
                    else
                    {
                        // Manejar el caso en el que User.Identity es nulo, tal vez devolver un error o realizar alguna otra acción apropiada.
                        return new UnauthorizedResult();
                    }
                }
            }
            return new NoContentResult();
        }

        // Eliminar una tarea por ID  
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _taskRepository.DeleteTask(id);
            return new OkResult();
        }
    }
}
