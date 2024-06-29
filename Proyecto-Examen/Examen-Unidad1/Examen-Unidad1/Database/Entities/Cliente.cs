using System.ComponentModel.DataAnnotations;

namespace Examen_Unidad1.Database.Entities
{
    public class Cliente
    {
        public Guid Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El {0} de la categoría es requerido.")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "El {0} de la categoría es requerido.")]
        public string Email { get; set; } 
    }
}
