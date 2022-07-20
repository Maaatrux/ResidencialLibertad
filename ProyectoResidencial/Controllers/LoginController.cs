using ProyectoResidencial.Logica;
using ProyectoResidencial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProyectoResidencial.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registrar()
        {
            return View();
        }
        
        public ActionResult CerrarSession()
        {
            FormsAuthentication.SignOut();
            Session["Usuario"] = null;
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult Index(Usuario usuario, string correo, string clave)
        {
            usuario = UsuarioLogica.Instancia.EncontrarUsuario().Where(u => u.Correo == correo && u.Clave == RecursoLogica.ConvertirSha256(clave)).FirstOrDefault();

            if (usuario == null)
            {
                ViewBag.Error = "Credenciales incorrectas";
                return View();
            }

            FormsAuthentication.SetAuthCookie(usuario.Correo, false);
            Session["Usuario"] = usuario;

            if (usuario.Tipo_Usuario.Equals(Tipo_usuario.Administrador))
            {
                return RedirectToAction("Index", "Home");
            }

            if (usuario.Tipo_Usuario.Equals(Tipo_usuario.Recepcionista))
            {
                return RedirectToAction("IndexRecepcionista", "Home");
            }

            if (usuario.Tipo_Usuario.Equals(Tipo_usuario.Cliente))
            {
                return RedirectToAction("IndexCliente", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Registrar(string tipo_documento, string rut, string nombre, string apellido, string correo, string clave, string confirmarClave)
        {
            Usuario usuario = new Usuario()
            {
                Tipo_documento = tipo_documento,
                Rut = rut,
                Nombre = nombre,
                Apellido = apellido,
                Correo = correo,
                Clave = clave,
                ConfirmarClave = confirmarClave
            };

            if (clave != confirmarClave)
            {
                ViewBag.Error = "Las contraseñas no coinciden";
                return View(usuario);
            }
            else
            {
                usuario.Clave = RecursoLogica.ConvertirSha256(clave);
                int idusuario_respuesta = UsuarioLogica.Instancia.RegistrarCliente(usuario);

                if (idusuario_respuesta == 0)
                {
                    ViewBag.Error = "Error al registrar";
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
        }
    }
}
