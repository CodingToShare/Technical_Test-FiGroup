using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskStatus = webapi.Models.TaskStatus;

namespace UnitTestAPI
{
    [TestFixture]
    public class UnitTestTaskStatusRepository
    {
        private DataContext _dbContext;

        [SetUp]
        public void Setup()
        {
            // Configura una instancia de ApplicationDbContext en memoria para las pruebas.  
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "API-Technical-Test-Unit-Test-DB")
                .Options;
            _dbContext = new DataContext(options);
        }

        [Test]
        public void GetTasksStatus_ReturnsListOfTaskStatus()
        {
            // Arrange  
            var taskStatusRepository = new TaskStatusRepository(_dbContext);

            // Act  
            var taskStatusList = taskStatusRepository.GetTasksStatus();

            // Assert  
            Assert.IsInstanceOf<IEnumerable<TaskStatus>>(taskStatusList);
            Assert.AreEqual(_dbContext.TaskStatus.ToList().Count, taskStatusList.Count());
        }
    }
}
