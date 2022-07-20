using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoResidencial.Models;

namespace ProyectoResidencial.Permisos
{
    public class PermisoRolAttribute : ActionFilterAttribute
    {
        private Tipo_usuario tipo_usu_id;

        public PermisoRolAttribute(Tipo_usuario _tipo_usu_id)
        {
            tipo_usu_id = _tipo_usu_id;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["Usuario"] != null)
            {
                Usuario usuario = HttpContext.Current.Session["Usuario"] as Usuario;

                if (usuario.Tipo_Usuario != this.tipo_usu_id)
                {
                    filterContext.Result = new RedirectResult("~/Login/Index");
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}