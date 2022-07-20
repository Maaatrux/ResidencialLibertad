using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoResidencial.Models
{
    public class Categoria
    {
        private int categoria_id;
        private string descripcion;
        private bool estado;

        public int Categoria_id { get => categoria_id; set => categoria_id = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public bool Estado { get => estado; set => estado = value; }
    }
}