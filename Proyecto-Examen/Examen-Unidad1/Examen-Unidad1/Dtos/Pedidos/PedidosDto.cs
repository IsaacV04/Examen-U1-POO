namespace Examen_Unidad1.Dtos.Pedidos
{
    public class PedidosDto
    {
        public Guid Id { get; set; }

        public Guid ClienteId { get; set; }

        public DateTime Fecha { get; set; }

        public string ListaDeProductos { get; set; }

        public decimal Total { get; set; }
    }
}
