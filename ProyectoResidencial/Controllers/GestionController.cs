using ProyectoResidencial.Logica;
using ProyectoResidencial.Models;
using ProyectoResidencial.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoResidencial.Controllers
{
    [Authorize]
    public class GestionController : Controller
    {
        // GET: Gestion
        public ActionResult Recepcion()
        {
            return View();
        }

        public ActionResult RecepcionRecepcionista()
        {
            return View();
        }

        public ActionResult RecepcionCliente()
        {
            return View();
        }

        public ActionResult RecepcionRegistro(int habitacion_id)
        {
            Habitacion habitacion = HabitacionLogica.Instancia.Listar().Where(h => h.Habitacion_id == habitacion_id).FirstOrDefault();
            return View(habitacion);
        }

        public ActionResult RecepcionRegistroRecepcionista(int habitacion_id)
        {
            Habitacion habitacion = HabitacionLogica.Instancia.Listar().Where(h => h.Habitacion_id == habitacion_id).FirstOrDefault();
            return View(habitacion);
        }

        public ActionResult RecepcionRegistroCliente(int habitacion_id)
        {
            ProyectoResidencial.Models.Usuario cliente = ((ProyectoResidencial.Models.Usuario)Session["Usuario"]);
            vmUsuario vm = new vmUsuario();
            Habitacion habitacion = HabitacionLogica.Instancia.Listar().Where(h => h.Habitacion_id == habitacion_id).FirstOrDefault();
            Usuario usuario = UsuarioLogica.Instancia.Listar().Where(u => u.Usuario_id == cliente.Usuario_id).FirstOrDefault();
            
            vm.Habitacion = habitacion;
            vm.Habitacion.Habitacion_id = habitacion.Habitacion_id;
            vm.Habitacion.Numero = habitacion.Numero;
            vm.Habitacion.Detalle = habitacion.Detalle;
            vm.Habitacion.Precio = habitacion.Precio;
            vm.Habitacion._categoria.Descripcion = habitacion._categoria.Descripcion;
            vm.Habitacion._piso.Descripcion = habitacion._piso.Descripcion;

            vm.Usuario = usuario;
            vm.Usuario.Usuario_id = usuario.Usuario_id;
            vm.Usuario.Tipo_documento = usuario.Tipo_documento;
            vm.Usuario.Rut = usuario.Rut;
            vm.Usuario.Nombre = usuario.Nombre;
            vm.Usuario.Apellido = usuario.Apellido;
            vm.Usuario.Correo = usuario.Correo;

            return View(vm);
        }

        public ActionResult DetalleRecepcion(int habitacion_id)
        {
            Recepcion recepcion = RecepcionLogica.Instancia.Listar().Where(h => h.Habitacion.Habitacion_id == habitacion_id && h.Estado == true).FirstOrDefault();
            if (recepcion != null)
            {
                List<Venta> venta = (from vn in VentaLogica.Instancia.Listar()
                                     where vn._recepcion.Recepcion_id == recepcion.Recepcion_id
                                     select new Venta()
                                     {
                                         Venta_id = vn.Venta_id,
                                         _recepcion = new Recepcion() { Recepcion_id = vn._recepcion.Recepcion_id },
                                         Total = vn.Total,
                                         Estado = vn.Estado,
                                         detalles = DetalleVentaLogica.Instancia.Listar().Where(dv => dv.Venta_id == vn.Venta_id).ToList()
                                     }).ToList();

                recepcion.Venta = venta;
            }
            return View(recepcion);
        }

        public ActionResult DetalleRecepcionRecepcionista(int habitacion_id)
        {
            Recepcion recepcion = RecepcionLogica.Instancia.Listar().Where(h => h.Habitacion.Habitacion_id == habitacion_id && h.Estado == true).FirstOrDefault();
            if (recepcion != null)
            {
                List<Venta> venta = (from vn in VentaLogica.Instancia.Listar()
                                     where vn._recepcion.Recepcion_id == recepcion.Recepcion_id
                                     select new Venta()
                                     {
                                         Venta_id = vn.Venta_id,
                                         _recepcion = new Recepcion() { Recepcion_id = vn._recepcion.Recepcion_id },
                                         Total = vn.Total,
                                         Estado = vn.Estado,
                                         detalles = DetalleVentaLogica.Instancia.Listar().Where(dv => dv.Venta_id == vn.Venta_id).ToList()
                                     }).ToList();

                recepcion.Venta = venta;
            }
            return View(recepcion);
        }

        public ActionResult Salida()
        {
            return View();
        }

        public ActionResult SalidaRecepcionista()
        {
            return View();
        }

        public ActionResult SalidaRegistro(int habitacion_id)
        {
            Recepcion recepcion = RecepcionLogica.Instancia.Listar().Where(h => h.Habitacion.Habitacion_id == habitacion_id && h.Estado == true).FirstOrDefault();
            if (recepcion != null)
            {
                List<Venta> venta = (from vn in VentaLogica.Instancia.Listar()
                                     where vn._recepcion.Recepcion_id == recepcion.Recepcion_id
                                     select new Venta()
                                     {
                                         Venta_id = vn.Venta_id,
                                         _recepcion = new Recepcion() { Recepcion_id = vn._recepcion.Recepcion_id },
                                         Total = vn.Total,
                                         Estado = vn.Estado,
                                         detalles = DetalleVentaLogica.Instancia.Listar().Where(dv => dv.Venta_id == vn.Venta_id).ToList()
                                     }).ToList();

                recepcion.Venta = venta;
            }
            return View(recepcion);
        }

        public ActionResult SalidaRegistroRecepcionista(int habitacion_id)
        {
            Recepcion recepcion = RecepcionLogica.Instancia.Listar().Where(h => h.Habitacion.Habitacion_id == habitacion_id && h.Estado == true).FirstOrDefault();
            if (recepcion != null)
            {
                List<Venta> venta = (from vn in VentaLogica.Instancia.Listar()
                                     where vn._recepcion.Recepcion_id == recepcion.Recepcion_id
                                     select new Venta()
                                     {
                                         Venta_id = vn.Venta_id,
                                         _recepcion = new Recepcion() { Recepcion_id = vn._recepcion.Recepcion_id },
                                         Total = vn.Total,
                                         Estado = vn.Estado,
                                         detalles = DetalleVentaLogica.Instancia.Listar().Where(dv => dv.Venta_id == vn.Venta_id).ToList()
                                     }).ToList();

                recepcion.Venta = venta;
            }
            return View(recepcion);
        }

        public ActionResult Venta(int idhabitacion)
        {
            Recepcion recepcion = RecepcionLogica.Instancia.Listar().Where(h => h.Habitacion.Habitacion_id == idhabitacion && h.Estado == true).FirstOrDefault();
            return View(recepcion);
        }

        public ActionResult VentaRecepcionista(int idhabitacion)
        {
            Recepcion recepcion = RecepcionLogica.Instancia.Listar().Where(h => h.Habitacion.Habitacion_id == idhabitacion && h.Estado == true).FirstOrDefault();
            return View(recepcion);
        }

        [HttpGet]
        public JsonResult ListarHabitacion(int idpiso)
        {
            List<Habitacion> lista = new List<Habitacion>();
            lista = HabitacionLogica.Instancia.Listar().Where(x => x._piso.Piso_id == (idpiso == 0 ? x._piso.Piso_id : idpiso)).OrderBy(o => o.Numero).ToList();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ActualizarEstadoHabitacion(int idhabitacion, int idestadohabitacion)
        {

            bool respuesta = false;
            respuesta = HabitacionLogica.Instancia.ActualizarEstado(idhabitacion, idestadohabitacion);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult RegistrarRecepcion(Recepcion recepcion)
        {
            bool respuesta = false;
            recepcion.Observacion = recepcion.Observacion == null ? "" : recepcion.Observacion;
            respuesta = RecepcionLogica.Instancia.Registrar(recepcion);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RegistrarRecepcionCliente(Recepcion recepcion)
        {
            bool respuesta = false;
            ProyectoResidencial.Models.Usuario cliente = ((ProyectoResidencial.Models.Usuario)Session["Usuario"]);
            Usuario usuario = UsuarioLogica.Instancia.Listar().Where(u => u.Usuario_id == cliente.Usuario_id).FirstOrDefault();
            string asunto = "Recepción con éxito";
            string mensaje_correo = "<h3>La recepción se ha completado con éxito</h3><br/><p>Acérquese a la Residencial Libertad Calcina Spa, ubicada en la calle Vargas #1857, para efectuar el pago de la recepción.</p><br/><p>Cualquier consulta, contáctese a este número: +56522321455.</p><br/><p>Contacto por WhatsApp: +56 9 6164 1321.</p>";

            recepcion.Observacion = recepcion.Observacion == null ? "" : recepcion.Observacion;
            if (respuesta = RecepcionLogica.Instancia.Registrar(recepcion))
            {
                RecursoLogica.EnviarCorreo(usuario.Correo, asunto, mensaje_correo);
            }
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);;;
        }

        [HttpPost]
        public JsonResult RegistrarUsuario(Recepcion recepcion)
        {
            bool respuesta = false;
            respuesta = RecepcionLogica.Instancia.Registrar(recepcion);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CerrarRecepcion(Recepcion recepcion)
        {
            bool respuesta = false;
            respuesta = RecepcionLogica.Instancia.Cerrar(recepcion);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarUsuarios()
        {
            List<Usuario> lista = new List<Usuario>();
            lista = UsuarioLogica.Instancia.Listar();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }
    }
}