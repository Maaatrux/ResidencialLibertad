using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoResidencial.Models
{
    public class Venta
    {
        private int venta_id;
        private int total;
        private string estado;

        public int Venta_id { get => venta_id; set => venta_id = value; }
        public Recepcion _recepcion { get; set; }
        public int Total { get => total; set => total = value; }
        public string Estado { get => estado; set => estado = value; }
        public List<Detalle_venta> detalles { get; set; }
    }
}