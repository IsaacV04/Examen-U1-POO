using System.ComponentModel.DataAnnotations;

namespace Examen_Unidad1.Dtos.Pedidos
{
    public class PedidoCreateDto
    {
        [Display(Name = "ClienteId")]
        [Required(ErrorMessage = "El {0} de la categoría es requerido.")]
        public Guid ClienteId { get; set; }

        [Display(Name = "Fecha")]
        public DateTime Fecha { get; set; }

        [Display(Name = "Lista de Productos")]
        public string ListaDeProductos { get; set; }
    }
}


    

    

