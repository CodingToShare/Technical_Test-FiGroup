using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models
{
    public class TaskStatus
    {
        // El identificador único del estado de la tarea.
        [Key]
        public Guid TaskStatusID { get; set; }

        // El nombre del estado de la tarea. Este es un campo obligatorio, por lo que debe especificarse al crear un nuevo estado de tarea.
        [Display(Name = "Estado de la Tarea")]
        [Column(TypeName = "varchar(200)")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public required string TaskStatusName { get; set; }

        // El usuario que creó el estado de la tarea. Este es un campo obligatorio, por lo que debe especificarse al crear un nuevo estado de tarea.
        [Display(Name = "Creado por")]
        [Column(TypeName = "varchar(100)")]
        [StringLength(200)]
        public required string CreatedBy { get; set; }

        // La fecha y hora en que se creó el estado de la tarea.
        [Display(Name = "Creado")]
        [DataType(DataType.DateTime)]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Created { get; set; }

        // El usuario que modificó el estado de la tarea por última vez. Este es un campo obligatorio, por lo que debe especificarse al modificar un estado de tarea.
        [Display(Name = "Modificado por")]
        [Column(TypeName = "varchar(100)")]
        [StringLength(200)]
        public required string ModifiedBy { get; set; }

        // La fecha y hora en que el estado de la tarea se modificó por última vez.
        [Display(Name = "Modificado")]
        [DataType(DataType.DateTime)]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Modified { get; set; }
    }
}
