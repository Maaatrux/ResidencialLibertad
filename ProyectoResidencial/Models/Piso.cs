using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoResidencial.Models
{
    public class Piso
    {
        private int piso_id;
        private string descripcion;
        private bool estado;

        public int Piso_id { get => piso_id; set => piso_id = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public bool Estado { get => estado; set => estado = value; }
    }
}