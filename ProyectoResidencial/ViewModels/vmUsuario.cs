using ProyectoResidencial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoResidencial.ViewModels
{
    public class vmUsuario
    {
        public Usuario Usuario { get; set; }

        public Habitacion Habitacion { get; set; }
    }
}