using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoResidencial.Models
{
    public class Detalle_venta
    {
        private int detalle_venta_id;
        private int venta_id;
        private int cantidad;
        private int subTotal;

        public int Detalle_venta_id { get => detalle_venta_id; set => detalle_venta_id = value; }
        public Producto _producto { get; set; }
        public int Venta_id { get => venta_id; set => venta_id = value; }
        public int Cantidad { get => cantidad; set => cantidad = value; }
        public int SubTotal { get => subTotal; set => subTotal = value; }
    }
}