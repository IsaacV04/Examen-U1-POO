using System.ComponentModel.DataAnnotations;

namespace Examen_Unidad1.Database.Entities
{
    public class Producto
    {
        public Guid Id { get; set; }

        [Display(Name = "Nombre")]
        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
