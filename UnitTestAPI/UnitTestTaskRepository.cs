using Microsoft.EntityFrameworkCore;


namespace UnitTestAPI
{
    [TestFixture]
    public class UnitTestTaskRepository
    {
        private DataContext _dbContext;
        private TaskRepository _taskManager;

        [SetUp]
        public void Setup()
        {
            // Configura una instancia de ApplicationDbContext en memoria para las pruebas.  
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "API-Technical-Test-Unit-Test-DB")
                .Options;
            _dbContext = new DataContext(options);
            _taskManager = new TaskRepository(_dbContext);
        }

        [Test]
        public void DeleteTask_Should_Remove_Task_From_Database()
        {
            // Crea una tarea y la agrega a la base de datos.
            var taskStatusID = "d9b6271b-bccc-4e43-a8eb-f2435e511062";
            var task = new webapi.Models.Task {
                TaskID = Guid.NewGuid(),
                TaskDescription = "Tarea de prueba",
                TaskStatusID = Guid.Parse(taskStatusID),
                Created = DateTime.UtcNow.AddHours(-5),
                CreatedBy = "UnitTest",
                Modified = DateTime.UtcNow.AddHours(-5),
                ModifiedBy = "UnitTest"
            };
            _dbContext.Tasks.Add(task);
            _dbContext.SaveChanges();

            // Elimina la tarea recién creada.  
            _taskManager.DeleteTask(task.TaskID);

            // Verifica que la tarea ya no exista en la base de datos.  
            var deletedTask = _dbContext.Tasks.Find(task.TaskID);
            Assert.IsNull(deletedTask);
        }

        [Test]
        public void DeleteTask_Should_Throw_Exception_If_Task_Does_Not_Exist()
        {
            // Intenta eliminar una tarea que no existe.  
            var nonExistentTaskId = Guid.NewGuid();
            Assert.Throws<Exception>(() => _taskManager.DeleteTask(nonExistentTaskId));
        }

        [Test]
        public void GetTasks_Should_Return_All_Tasks_For_User()
        {
            var taskStatusID = "d9b6271b-bccc-4e43-a8eb-f2435e511062";
            // Crea algunas tareas para el usuario "Alice".  
            var aliceTask1 = new webapi.Models.Task {
                TaskID = Guid.NewGuid(),
                TaskDescription = "Tarea de Alice 1",
                TaskStatusID = Guid.Parse(taskStatusID),
                Created = DateTime.UtcNow.AddHours(-5),
                CreatedBy = "Alice",
                Modified = DateTime.UtcNow.AddHours(-5),
                ModifiedBy = "Alice"
            };
            var aliceTask2 = new webapi.Models.Task {
                TaskID = Guid.NewGuid(),
                TaskDescription = "Tarea de Alice 2",
                TaskStatusID = Guid.Parse(taskStatusID),
                Created = DateTime.UtcNow.AddHours(-5),
                CreatedBy = "Alice",
                Modified = DateTime.UtcNow.AddHours(-5),
                ModifiedBy = "Alice"
            };
            _dbContext.Tasks.Add(aliceTask1);
            _dbContext.Tasks.Add(aliceTask2);

            // Crea una tarea para el usuario "Bob".  
            var bobTask1 = new webapi.Models.Task {
                TaskID = Guid.NewGuid(),
                TaskDescription = "Tarea de Bob 1",
                TaskStatusID = Guid.Parse(taskStatusID),
                Created = DateTime.UtcNow.AddHours(-5),
                CreatedBy = "Bob",
                Modified = DateTime.UtcNow.AddHours(-5),
                ModifiedBy = "Bob"
            };
            _dbContext.Tasks.Add(bobTask1);

            _dbContext.SaveChanges();

            // Obtiene todas las tareas para el usuario "Alice".  
            var tasks = _taskManager.GetTasks("Alice");

            // Verifica que se devuelvan las dos tareas creadas para el usuario "Alice".  
            Assert.AreEqual(2, tasks.Count());
            Assert.IsTrue(tasks.Any(x => x.TaskID == aliceTask1.TaskID));
            Assert.IsTrue(tasks.Any(x => x.TaskID == aliceTask2.TaskID));
        }

        [Test]
        public void GetTasks_Should_Return_Empty_List_If_No_Tasks_Found_For_User()
        {
            // Obtiene todas las tareas para un usuario que no tiene tareas.  
            var tasks = _taskManager.GetTasks("Charlie");

            // Verifica que se devuelva una lista vacía.  
            Assert.IsEmpty(tasks);
        }

        [Test]
        public void InsertTask_Should_Add_New_Task_To_Database()
        {
            var taskStatusID = "d9b6271b-bccc-4e43-a8eb-f2435e511062";
            // Crea una tarea de prueba.  
            var taskModel = new TaskModel
            {
                TaskDescription = "Tarea de prueba",
                TaskStatusID = Guid.Parse(taskStatusID),
            };
            var userName = "Alice";

            // Inserta la tarea de prueba en la base de datos.  
            _taskManager.InsertTask(taskModel, userName);

            // Verifica que se haya agregado la tarea de prueba a la base de datos.  
            var task = _dbContext.Tasks.FirstOrDefault(x => x.TaskDescription == taskModel.TaskDescription);
            Assert.IsNotNull(task);
            Assert.AreEqual(taskModel.TaskStatusID, task.TaskStatusID);
            Assert.AreEqual(userName, task.CreatedBy);
            Assert.AreEqual(userName, task.ModifiedBy);
        }

        [Test]
        public void Save_Should_Save_Changes_To_Database()
        {
            // Crea una tarea de prueba y la agrega a la base de datos.  
            var taskStatusID = "d9b6271b-bccc-4e43-a8eb-f2435e511062";
            var task = new webapi.Models.Task
            {
                TaskID = Guid.NewGuid(),
                TaskDescription = "Tarea de prueba",
                TaskStatusID = Guid.Parse(taskStatusID),
                Created = DateTime.UtcNow.AddHours(-5),
                CreatedBy = "UnitTest",
                Modified = DateTime.UtcNow.AddHours(-5),
                ModifiedBy = "UnitTest"
            };
            _dbContext.Tasks.Add(task);

            // Guarda los cambios en la base de datos.  
            _taskManager.Save();

            // Verifica que se haya guardado la tarea en la base de datos.  
            var savedTask = _dbContext.Tasks.Find(task.TaskID);
            Assert.IsNotNull(savedTask);
            Assert.AreEqual(task.TaskDescription, savedTask.TaskDescription);
        }

        [Test]
        public void UpdateTask_Should_Update_Existing_Task_In_Database()
        {
            // Crea una tarea de prueba y la agrega a la base de datos.  
            var taskStatusID = "d9b6271b-bccc-4e43-a8eb-f2435e511062";
            var task = new webapi.Models.Task
            {
                TaskID = Guid.NewGuid(),
                TaskDescription = "Tarea de prueba",
                TaskStatusID = Guid.Parse(taskStatusID),
                Created = DateTime.UtcNow.AddHours(-5),
                CreatedBy = "UnitTest",
                Modified = DateTime.UtcNow.AddHours(-5),
                ModifiedBy = "UnitTest"
            };
            _dbContext.Tasks.Add(task);
            _dbContext.SaveChanges();

            // Actualiza la tarea de prueba en la base de datos.  
            var updatedTaskModel = new TaskModel { TaskDescription = "Tarea actualizada", TaskStatusID = Guid.Parse(taskStatusID) };
            var userName = "Alice";
            _taskManager.UpdateTask(updatedTaskModel, userName, task.TaskID);

            // Verifica que se haya actualizado la tarea de prueba en la base de datos.  
            var updatedTask = _dbContext.Tasks.Find(task.TaskID);
            Assert.IsNotNull(updatedTask);
            Assert.AreEqual(updatedTaskModel.TaskDescription, updatedTask.TaskDescription);
            Assert.AreEqual(updatedTaskModel.TaskStatusID, updatedTask.TaskStatusID);
            Assert.AreEqual(userName, updatedTask.ModifiedBy);
        }

        [Test]
        public void UpdateTask_Should_Not_Update_Nonexistent_Task_In_Database()
        {
            // Delete all tasks in the database  
            _dbContext.Tasks.RemoveRange(_dbContext.Tasks);
            _dbContext.SaveChanges();

            // Actualiza una tarea que no existe en la base de datos.    
            var taskStatusID = "d9b6271b-bccc-4e43-a8eb-f2435e511062";
            var updatedTaskModel = new TaskModel
            {
                TaskDescription = "Tarea de prueba",
                TaskStatusID = Guid.Parse(taskStatusID),
            };
            var userName = "Alice";
            _taskManager.UpdateTask(updatedTaskModel, userName, Guid.Parse(taskStatusID));

            // Verifica que no se haya actualizado ninguna tarea en la base de datos.    
            var tasks = _dbContext.Tasks.ToList();
            Assert.IsEmpty(tasks);
        }
    }
}