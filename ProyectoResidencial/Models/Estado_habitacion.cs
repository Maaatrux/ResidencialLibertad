using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoResidencial.Models
{
    public class Estado_habitacion
    {
        private int estado_habi_id;
        private string descripcion;
        private bool estado;

        public int Estado_habi_id { get => estado_habi_id; set => estado_habi_id = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public bool Estado { get => estado; set => estado = value; }
    }
}