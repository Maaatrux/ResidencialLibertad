using ClosedXML.Excel;
using ProyectoResidencial.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoResidencial.Controllers
{
    [Authorize]
    public class ReporteController : Controller
    {
        // GET: Reporte
        public ActionResult Recepcion()
        {
            return View();
        }

        public ActionResult Producto()
        {
            return View();
        }

        [HttpPost]
        public FileResult ExportarProductos(string estado, string fechaInicio, string fechaSalida)
        {
            DataTable dt = ReporteLogica.Instancia.Producto(estado, fechaInicio, fechaSalida);

            dt.TableName = "Datos";
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Reporte Productos " + DateTime.Now.ToString() + ".xlsx");
                }
            }
        }

        [HttpPost]
        public FileResult ExportarRecepciones(string idhabitacion, string fechaInicio, string fechaSalida)
        {
            DataTable dt = ReporteLogica.Instancia.Recepciones(idhabitacion, fechaInicio, fechaSalida);

            dt.TableName = "Datos";
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Reporte Recepciones " + DateTime.Now.ToString() + ".xlsx");
                }
            }
        }
    }
}
