using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoResidencial.Models
{
    public class Habitacion
    {
        private int habitacion_id;
        private string numero;
        private string detalle;
        private int precio;
        private string precioS;
        private bool estado;

        public int Habitacion_id { get => habitacion_id; set => habitacion_id = value; }
        public string Numero { get => numero; set => numero = value; }
        public string Detalle { get => detalle; set => detalle = value; }
        public int Precio { get => precio; set => precio = value; }
        public string PrecioS { get => precioS; set => precioS = value; }
        public Piso _piso { get; set; }
        public Categoria _categoria { get; set; }
        public Estado_habitacion _estado_habitacion { get; set; }
        public bool Estado { get => estado; set => estado = value; }
    }
}