using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoResidencial.Models
{
    public class TipoUsuario
    {
        private int tipo_usu_id;
        private string descripcion;

        public int Tipo_usu_id { get => tipo_usu_id; set => tipo_usu_id = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
    }
}