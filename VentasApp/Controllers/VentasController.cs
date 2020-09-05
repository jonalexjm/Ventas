using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VentasApp.Models;
using VentasApp.ViewModels;

namespace VentasApp.Controllers
{
    public class VentasController : Controller
    {

        DataContext db = new DataContext();

        
        public ActionResult NewPedido()
        {

            ViewBag.Mensaje = "Hola mundo";

            var pedidoView = new PedidoViewModel();
            pedidoView.Cliente = new Cliente();
            pedidoView.Productos = new List<Producto>();

            Session["pedidoView"] = pedidoView; //almacenar en session

            #region
            //clientes para el select 
            var list = db.Clientes.Where(c => c.Estado == true).ToList();
            list.Add(new Cliente { Id = 0, Nombre = "Escribir el nombre" });
            list = list.OrderBy(c => c.Id).ToList();
            ViewBag.clientesSelect = new SelectList(list, "Id", "Nombre");

            //Sucursal para el select 
            var lista = db.Sucursales.ToList();
            lista.Add(new Sucursales { Id = 0, Nombre = "Escribir el nombre" });
            lista = lista.OrderBy(c => c.Id).ToList();
            ViewBag.sucursalSelect = new SelectList(lista, "Id", "Nombre");

            //Empleado para el select 
            var listb = db.Empleadoes.ToList();
            listb.Add(new Empleado { Id = 0, Nombre = "Escribir el nombre" });
            listb = listb.OrderBy(c => c.Id).ToList();
            ViewBag.empleadoSelect = new SelectList(listb, "Id", "Nombre");
            #endregion




            return View(pedidoView);
        }


        [HttpPost]
        public ActionResult NewPedido(PedidoViewModel pedidoViewModel)
        {
            #region
            //clientes para el select 
            var list = db.Clientes.Where(c => c.Estado == true).ToList();
            list.Add(new Cliente { Id = 0, Nombre = "Escribir el nombre" });
            list = list.OrderBy(c => c.Id).ToList();
            ViewBag.clientesSelect = new SelectList(list, "Id", "Nombre");

            //Sucursal para el select 
            var lista = db.Sucursales.ToList();
            lista.Add(new Sucursales { Id = 0, Nombre = "Escribir el nombre" });
            lista = lista.OrderBy(c => c.Id).ToList();
            ViewBag.sucursalSelect = new SelectList(lista, "Id", "Nombre");

            //Empleado para el select 
            var listb = db.Empleadoes.ToList();
            listb.Add(new Empleado { Id = 0, Nombre = "Escribir el nombre" });
            listb = listb.OrderBy(c => c.Id).ToList();
            ViewBag.empleadoSelect = new SelectList(listb, "Id", "Nombre");
            #endregion


            // recuperamos la variable de session
            pedidoViewModel = Session["pedidoView"] as PedidoViewModel;


            //capturar la informacion del combobox
            var clienteId =  int.Parse(Request["clientesSelect"]);
            var sucursalId = int.Parse(Request["sucursalSelect"]);
            var empleadoId = int.Parse(Request["empleadoSelect"]);

            if (clienteId == 0 || sucursalId == 0 || empleadoId == 0)
            {
                //clientes para el select 
                var listz = db.Clientes.Where(c => c.Estado == true).ToList();
                listz.Add(new Cliente { Id = 0, Nombre = "Escribir el nombre" });
                listz = listz.OrderBy(c => c.Id).ToList();
                ViewBag.clientesSelect = new SelectList(listz, "Id", "Nombre");

                //Sucursal para el select 
                var listax = db.Sucursales.ToList();
                listax.Add(new Sucursales { Id = 0, Nombre = "Escribir el nombre" });
                listax = listax.OrderBy(c => c.Id).ToList();
                ViewBag.sucursalSelect = new SelectList(listax, "Id", "Nombre");

                //Empleado para el select 
                var listh = db.Empleadoes.ToList();
                listh.Add(new Empleado { Id = 0, Nombre = "Escribir el nombre" });
                listh = listb.OrderBy(c => c.Id).ToList();
                ViewBag.empleadoSelect = new SelectList(listh, "Id", "Nombre");

                ViewBag.errorCliente = "Los campos del formulario pedido deben ser diligenciados";

                return View(pedidoViewModel);
            }

            if (pedidoViewModel.Productos.Count == 0)
            {
                //clientes para el select 
                var listz = db.Clientes.Where(c => c.Estado == true).ToList();
                listz.Add(new Cliente { Id = 0, Nombre = "Escribir el nombre" });
                listz = listz.OrderBy(c => c.Id).ToList();
                ViewBag.clientesSelect = new SelectList(listz, "Id", "Nombre");

                //Sucursal para el select 
                var listax = db.Sucursales.ToList();
                listax.Add(new Sucursales { Id = 0, Nombre = "Escribir el nombre" });
                listax = listax.OrderBy(c => c.Id).ToList();
                ViewBag.sucursalSelect = new SelectList(listax, "Id", "Nombre");

                //Empleado para el select 
                var listh = db.Empleadoes.ToList();
                listh.Add(new Empleado { Id = 0, Nombre = "Escribir el nombre" });
                listh = listb.OrderBy(c => c.Id).ToList();
                ViewBag.empleadoSelect = new SelectList(listh, "Id", "Nombre");

                ViewBag.errorCliente = "Debe adicionar un producto";

                return View(pedidoViewModel);
            }


            int pedidoId = 0;

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var pedido = new Pedido
                    {
                        ClienteId = clienteId,
                        SucursalId = sucursalId,
                        EmpleadoId = empleadoId
                    };

                    //grabar en la base de datos

                    db.Pedidos.Add(pedido);
                    db.SaveChanges();

                    // guardar detalle
                    pedidoId = db.Pedidos.ToList().Select(p => p.Id).Max();

                    foreach (var item in pedidoViewModel.Productos)
                    {

                        var detallePedido = new DetallePedido
                        {
                            ProductoId = item.Id,
                            PedidoId = pedidoId
                        };

                        //grabar en la base de datos
                        db.DetallePedidos.Add(detallePedido);
                        db.SaveChanges();
                    }

                    transaction.Commit();


                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    ViewBag.errorCliente = "Error" + ex.Message;

                    return View(pedidoViewModel);
                }
            
            
            }

            ViewBag.Mensaje = string.Format("EL pedido ya esta hecho");

            pedidoViewModel = new PedidoViewModel();
            pedidoViewModel.Cliente = new Cliente();
            pedidoViewModel.Sucursales = new Sucursales();
            pedidoViewModel.Empleado = new Empleado();
            pedidoViewModel.Productos = new List<Producto>();


            Session["pedidoView"] = pedidoViewModel;

            return View(pedidoViewModel);
        }


        public ActionResult AddProduct()
        {

            //ViewBag Productos para el select 
            var list = db.Productoes.ToList();
            list.Add(new Producto { Id = 0, Nombre = "Seleccione un producto" });
            list = list.OrderBy(p => p.Id).ToList();
            ViewBag.ProductoSelect = new SelectList(list, "Id", "Nombre");

            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(Producto producto)
        {
            #region select
            //ViewBag Productos para el select 
            var list = db.Productoes.ToList();
            list.Add(new Producto { Id = 0, Nombre = "Seleccione un producto" });
            list = list.OrderBy(p => p.Id).ToList();
            ViewBag.ProductoSelect = new SelectList(list, "Id", "Nombre");
            #endregion

            var pedidoView = Session["pedidoView"] as PedidoViewModel;
            var productoId = int.Parse(Request["ProductoSelect"]); //recuperamos lo que trae del combo box

            if(productoId == 0)
            {
                //ViewBag Productos para el select 
                var listd = db.Productoes.ToList();
                listd.Add(new Producto { Id = 0, Nombre = "Seleccione un producto" });
                listd = listd.OrderBy(p => p.Id).ToList();
                ViewBag.ProductoSelect = new SelectList(listd, "Id", "Nombre");
                return View();
            }

            var product = db.Productoes.Find(productoId);

            if(product == null)
            {
                //ViewBag Productos para el select 
                var listh = db.Productoes.ToList();
                listh.Add(new Producto { Id = 0, Nombre = "Seleccione un producto" });
                listh = listh.OrderBy(p => p.Id).ToList();
                ViewBag.ProductoSelect = new SelectList(listh, "Id", "Nombre");
                return View();

            }

            //creamos el objeto que vamos almacenar

            var productPedido = new Producto
            {
                Nombre = product.Nombre,
                Id = product.Id,
                Precio = product.Precio,
                Stock = product.Stock

            };

            //agregamos a una lista dentro de la view model

            pedidoView.Productos.Add(productPedido);

            #region combox

            //clientes para el select 
            var listc = db.Clientes.Where(c => c.Estado == true).ToList();
            listc.Add(new Cliente { Id = 0, Nombre = "Escribir el nombre" });
            listc = listc.OrderBy(c => c.Id).ToList();
            ViewBag.clientesSelect = new SelectList(listc, "Id", "Nombre");

            //Sucursal para el select 
            var lista = db.Sucursales.ToList();
            lista.Add(new Sucursales { Id = 0, Nombre = "Escribir el nombre" });
            lista = lista.OrderBy(c => c.Id).ToList();
            ViewBag.sucursalSelect = new SelectList(lista, "Id", "Nombre");

            //Empleado para el select 
            var listb = db.Empleadoes.ToList();
            listb.Add(new Empleado { Id = 0, Nombre = "Escribir el nombre" });
            listb = listb.OrderBy(c => c.Id).ToList();
            ViewBag.empleadoSelect = new SelectList(listb, "Id", "Nombre");

            #endregion




            return View("NewPedido", pedidoView);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }


}