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
    public class MantenimientoController : Controller
    {
        // GET: Mantenimiento
        public ActionResult Habitacion()
        {
            return View();
        }

        public ActionResult HabitacionRecepcionista()
        {
            return View();
        }

        public ActionResult Categoria()
        {
            return View();
        }

        public ActionResult CategoriaRecepcionista()
        {
            return View();
        }

        public ActionResult Piso()
        {
            return View();
        }

        public ActionResult PisoRecepcionista()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarCategoria()
        {
            List<Categoria> lista = new List<Categoria>();
            lista = CategoriaLogica.Instancia.Listar();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RegistrarCategoria(Categoria categoria)
        {
            bool respuesta = false;
            respuesta = (categoria.Categoria_id == 0) ? CategoriaLogica.Instancia.RegistrarCategoria(categoria) : CategoriaLogica.Instancia.ActualizarCategoria(categoria);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarCategoria(int id)
        {
            bool respuesta = false;
            respuesta = CategoriaLogica.Instancia.EliminarCategoria(id);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarPiso()
        {
            List<Piso> lista = new List<Piso>();
            lista = PisoLogica.Instancia.Listar();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RegistrarPiso(Piso piso)
        {
            bool respuesta = false;
            respuesta = (piso.Piso_id == 0) ? PisoLogica.Instancia.RegistrarPiso(piso) : PisoLogica.Instancia.ActualizarPiso(piso);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarPiso(int id)
        {
            bool respuesta = false;
            respuesta = PisoLogica.Instancia.EliminarPiso(id);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarHabitacion()
        {
            List<Habitacion> lista = new List<Habitacion>();
            lista = HabitacionLogica.Instancia.Listar();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RegistrarHabitacion(Habitacion habitacion)
        {
            bool respuesta = false;
            respuesta = (habitacion.Habitacion_id == 0) ? HabitacionLogica.Instancia.RegistrarHabitacion(habitacion) : HabitacionLogica.Instancia.ActualizarHabitacion(habitacion);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarHabitacion(int id)
        {
            bool respuesta = false;
            respuesta = HabitacionLogica.Instancia.EliminarHabitacion(id);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
    }
}