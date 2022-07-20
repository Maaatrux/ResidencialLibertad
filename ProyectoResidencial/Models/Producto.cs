using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoResidencial.Models
{
    public class Producto
    {
        private int producto_id;
        private string nombre;
        private string detalle;
        private int precio;
        private int precioS;
        private int cantidad;
        private bool estado;

        public int Producto_id { get => producto_id; set => producto_id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Detalle { get => detalle; set => detalle = value; }
        public int Precio { get => precio; set => precio = value; }
        public int PrecioS { get => precioS; set => precioS = value; }
        public int Cantidad { get => cantidad; set => cantidad = value; }
        public bool Estado { get => estado; set => estado = value; }
    }
}