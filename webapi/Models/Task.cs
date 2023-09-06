using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;
using System.Threading.Tasks;

namespace webapi.Models
{
    public class Task
    {
        // El identificador único de la tarea.
        [Key]
        public Guid TaskID { get; set; }

        // La descripción de la tarea.
        [Display(Name = "Tarea")]
        [Column(TypeName = "varchar(200)")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public required string TaskDescription { get; set; }

        // El estado de la tarea.
        [Display(Name = "Estado de la Tarea")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Guid TaskStatusID { get; set; }

        // El usuario que creó la tarea.
        [Display(Name = "Creado por")]
        [Column(TypeName = "varchar(100)")]
        [StringLength(200)]
        public required string CreatedBy { get; set; }

        // La fecha y hora en que se creó la tarea.
        [Display(Name = "Creado")]
        [DataType(DataType.DateTime)]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Created { get; set; }

        // El usuario que modificó la tarea por última vez.
        [Display(Name = "Modificado por")]
        [Column(TypeName = "varchar(100)")]
        [StringLength(200)]
        public required string ModifiedBy { get; set; }

        // La fecha y hora en que la tarea se modificó por última vez.
        [Display(Name = "Modificado")]
        [DataType(DataType.DateTime)]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Modified { get; set; }

    }
}
