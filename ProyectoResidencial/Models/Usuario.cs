using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoResidencial.Models
{
    public class Usuario
    {
        private int usuario_id;
        private string tipo_documento;
        private string rut;
        private string nombre;
        private string apellido;
        private string correo;
        private string clave;
        private string confirmarClave;
        private bool estado;
        private Tipo_usuario tipo_Usuario;

        public int Usuario_id { get => usuario_id; set => usuario_id = value; }
        public string Tipo_documento { get => tipo_documento; set => tipo_documento = value; }
        public string Rut { get => rut; set => rut = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellido { get => apellido; set => apellido = value; }
        public string Correo { get => correo; set => correo = value; }
        public string Clave { get => clave; set => clave = value; }
        public string ConfirmarClave { get => confirmarClave; set => confirmarClave = value; }
        public bool Estado { get => estado; set => estado = value; }
        public Tipo_usuario Tipo_Usuario { get => tipo_Usuario; set => tipo_Usuario = value; }
        public TipoUsuario TipoUsuario { get; set; }
    }
}