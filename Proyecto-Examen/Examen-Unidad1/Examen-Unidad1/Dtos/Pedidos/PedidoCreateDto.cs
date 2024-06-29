using System.ComponentModel.DataAnnotations;

namespace Examen_Unidad1.Dtos.Pedidos
{
    public class PedidoCreateDto
    {
        [Required(ErrorMessage = "El ClienteId es requerido.")]
        public Guid ClienteId { get; set; }

        [Required(ErrorMessage = "La Fecha es requerida.")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "La Lista de Productos es requerida.")]
        public string ListaDeProductos { get; set; }
    }
}






