using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VentasApp.Models
{
    public class Pedido
    {
        [Key]
        [Required]
        public int Id { set; get; }

        [Required]
        public int SucursalId { get; set; }
        public virtual Sucursales Sucursales { get; set; }

        [Required]
        public int EmpleadoId { get; set; }
        public virtual Empleado Empleado { get; set; }

        [Required]
        public int ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }

        public virtual ICollection<DetallePedido> DetallePedidos { get; set; }
    }
}