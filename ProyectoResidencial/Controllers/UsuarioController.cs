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
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Cliente()
        {
            return View();
        }

        public ActionResult Usuarios()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarTipoUsuario()
        {
            List<TipoUsuario> lista = new List<TipoUsuario>();
            lista = UsuarioLogica.Instancia.ListarTipoUsuario();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarUsuario()
        {
            List<Usuario> lista = new List<Usuario>();
            lista = UsuarioLogica.Instancia.Listar();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RegistrarUsuario(Usuario usuario)
        {
            bool respuesta = false;
            if (usuario.Usuario_id == 0)
            {
                usuario.Clave = RecursoLogica.ConvertirSha256(usuario.Clave);
                respuesta = UsuarioLogica.Instancia.Registrar(usuario);
            } else
            {
                usuario.Clave = RecursoLogica.ConvertirSha256(usuario.Clave);
                respuesta = UsuarioLogica.Instancia.ActualizarUsuario(usuario);
            }
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarUsuario(int id)
        {
            bool respuesta = false;
            respuesta = UsuarioLogica.Instancia.EliminarUsuario(id);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
    }
}