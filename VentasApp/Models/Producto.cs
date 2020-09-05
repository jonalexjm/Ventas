using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VentasApp.Models
{
    public class Producto
    {
        [Key]
        [Required]
        public int Id { set; get; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public int Stock { get; set; }

        [Required]
        public decimal Precio { get; set; }

        public virtual ICollection<DetallePedido> DetallePedidos { get; set; }
    }
}