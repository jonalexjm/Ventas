using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VentasApp.Models
{
    public class Empleado
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
        public int SucursalId { get; set; }
        public virtual Sucursales Sucursales { get; set; }
    }
}