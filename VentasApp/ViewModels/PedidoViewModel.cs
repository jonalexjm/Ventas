using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VentasApp.Models;

namespace VentasApp.ViewModels
{
    public class PedidoViewModel
    {
        public Cliente Cliente { get; set; }

        public Sucursales Sucursales { get; set; }

        public Empleado Empleado { get; set; }

        //  public DetallePedido DetallePedido { get; set; }
        public Producto ProductTitulos { get; set; }
        public List<Producto> Productos { get; set; }
    }
}