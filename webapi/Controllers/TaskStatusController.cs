using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Interfaces;

namespace webapi.Controllers
{
    // Controlador que maneja traer los datos de los estados de las tareas  
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskStatusController : ControllerBase
    {
        private readonly ITaskStatusRepository _taskStatusRepository;

        // Constructor del controlador que recibe una implementación del repositorio de los estados de las tareas  
        public TaskStatusController(ITaskStatusRepository taskStatusRepository)
        {
            _taskStatusRepository = taskStatusRepository;
        }

        // Obtener todas los estados de las tareas  
        [HttpGet]
        public IActionResult Get()
        {
            var tasks = _taskStatusRepository.GetTasksStatus();
            return new OkObjectResult(tasks);
        }
    }
}
