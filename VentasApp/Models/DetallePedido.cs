using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VentasApp.Models
{
    public class DetallePedido
    {
        [Key]
        [Required]
        public int Id { set; get; }

        [Required]
        public int ProductoId { get; set; }
        public virtual Producto Producto { get; set; }

        [Required]
        public int PedidoId { get; set; }
        public virtual Pedido Pedido { get; set; }

       
    }
}