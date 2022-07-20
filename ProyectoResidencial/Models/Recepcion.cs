using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoResidencial.Models
{
    public class Recepcion
    {
        private int recepcion_id;
        private DateTime fecha_entrada;
        private string fecha_entradaS;
        private DateTime fecha_Salida;
        private string fecha_salidaS;
        private DateTime fecha_confirmacion;
        private string fecha_confirmacionS;
        private int precio_inicial;
        private string precio_inicialS;
        private int abono;
        private string abonoS;
        private int precio_restante;
        private string precio_restanteS;
        private int total_pagado;
        private string total_pagadoS;
        private int multa;
        private string multaS;
        private string observacion;
        private bool estado;
        private Usuario usuario;
        private Habitacion habitacion;

        public int Recepcion_id { get => recepcion_id; set => recepcion_id = value; }
        public Usuario Usuario { get => usuario; set => usuario = value; }
        public Habitacion Habitacion { get => habitacion; set => habitacion = value; }
        public DateTime Fecha_entrada { get => fecha_entrada; set => fecha_entrada = value; }
        public string Fecha_entradaS { get => fecha_entradaS; set => fecha_entradaS = value; }
        public DateTime Fecha_Salida { get => fecha_Salida; set => fecha_Salida = value; }
        public string Fecha_salidaS { get => fecha_salidaS; set => fecha_salidaS = value; }
        public DateTime Fecha_confirmacion { get => fecha_confirmacion; set => fecha_confirmacion = value; }
        public string Fecha_confirmacionS { get => fecha_confirmacionS; set => fecha_confirmacionS = value; }
        public int Precio_inicial { get => precio_inicial; set => precio_inicial = value; }
        public string Precio_inicialS { get => precio_inicialS; set => precio_inicialS = value; }
        public int Abono { get => abono; set => abono = value; }
        public string AbonoS { get => abonoS; set => abonoS = value; }
        public int Precio_restante { get => precio_restante; set => precio_restante = value; }
        public string Precio_restanteS { get => precio_restanteS; set => precio_restanteS = value; }
        public int Total_pagado { get => total_pagado; set => total_pagado = value; }
        public string Total_pagadoS { get => total_pagadoS; set => total_pagadoS = value; }
        public int Multa { get => multa; set => multa = value; }
        public string MultaS { get => multaS; set => multaS = value; }
        public string Observacion { get => observacion; set => observacion = value; }
        public bool Estado { get => estado; set => estado = value; }
        public List<Venta> Venta { get; set; }
    }
}