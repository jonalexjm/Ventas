using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace VentasApp.Models
{
    public class DataContext : DbContext
    {
        public DataContext() : base("ConnectionSqlServer")
        {



        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public System.Data.Entity.DbSet<VentasApp.Models.Sucursales> Sucursales { get; set; }

        public System.Data.Entity.DbSet<VentasApp.Models.Empleado> Empleadoes { get; set; }

        public System.Data.Entity.DbSet<VentasApp.Models.Cliente> Clientes { get; set; }

        public System.Data.Entity.DbSet<VentasApp.Models.Pedido> Pedidos { get; set; }

        public System.Data.Entity.DbSet<VentasApp.Models.DetallePedido> DetallePedidos { get; set; }

        public System.Data.Entity.DbSet<VentasApp.Models.Producto> Productoes { get; set; }
    }
}