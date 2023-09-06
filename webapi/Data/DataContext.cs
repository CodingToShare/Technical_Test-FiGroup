using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using webapi.Models;

namespace webapi.Data
{
    public class DataContext : IdentityDbContext
    {
        // El constructor de la clase `DataContext`.
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        // Las colecciones de tareas y estados de tareas.
        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<Models.TaskStatus> TaskStatus { get; set; }

        // El método `OnModelCreating` se utiliza para inicializar el modelo de datos.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Agrega dos registros de estado de tarea al modelo de datos.
            modelBuilder.Entity<Models.TaskStatus>().HasData(
                new Models.TaskStatus
                {
                    TaskStatusID = Guid.NewGuid(),
                    TaskStatusName = "Completada",
                    CreatedBy = "WebAPITask",
                    Created = DateTime.UtcNow.AddHours(-5),
                    ModifiedBy = "WebAPITask",
                    Modified = DateTime.UtcNow.AddHours(-5)

                },
                new Models.TaskStatus
                {
                    TaskStatusID = Guid.NewGuid(),
                    TaskStatusName = "No completada",
                    CreatedBy = "WebAPITask",
                    Created = DateTime.UtcNow.AddHours(-5),
                    ModifiedBy = "WebAPITask",
                    Modified = DateTime.UtcNow.AddHours(-5)
                }

            );
        }
    }
}
