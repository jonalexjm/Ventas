using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VentasApp.Models
{
    public class Cliente
    {
        [Key]
        [Required]
        public int Id { set; get; }
        [Required]
        public int Documento { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        public bool Estado { get; set; }

        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}