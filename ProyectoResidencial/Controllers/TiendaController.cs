using ProyectoResidencial.Logica;
using ProyectoResidencial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoResidencial.Controllers
{
    [Authorize]
    public class TiendaController : Controller
    {
        // GET: Tienda
        public ActionResult Vender()
        {
            return View();
        }

        public ActionResult VenderRecepcionista()
        {
            return View();
        }

        public ActionResult Producto()
        {
            return View();
        }

        public ActionResult ProductoRecepcionista()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarProducto()
        {
            List<Producto> lista = new List<Producto>();
            lista = ProductoLogica.Instancia.Listar().OrderBy(o => o.Nombre).ToList();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RegistrarProducto(Producto producto)
        {
            bool respuesta = false;
            respuesta = (producto.Producto_id == 0) ? ProductoLogica.Instancia.RegistrarProducto(producto) : ProductoLogica.Instancia.ActualizarProducto(producto);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarProducto(int id)
        {
            bool respuesta = false;
            respuesta = ProductoLogica.Instancia.Eliminar(id);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RegistrarVenta(Venta venta)
        {
            bool respuesta = false;
            respuesta = VentaLogica.Instancia.RegistrarVenta(venta);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
    }
}