using System.ComponentModel.DataAnnotations;

namespace Examen_Unidad1.Dtos.Pedidos
{
    public class ProductsCreateDto
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El {0} de la categoría es requerido.")]
        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
