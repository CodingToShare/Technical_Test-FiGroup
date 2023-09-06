using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace webapi.ModelsDto
{
    public class TaskModel
    {
        // La descripción de la tarea.
        [Display(Name = "Tarea")]
        [Column(TypeName = "varchar(200)")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public required string TaskDescription { get; set; }

        // El estado de la tarea.
        [Display(Name = "Estado de la Tarea")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Guid TaskStatusID { get; set; }
    }
}
