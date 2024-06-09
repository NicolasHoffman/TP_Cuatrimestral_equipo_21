using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Usuario:Persona
    {
        public int Legajo { get; set; }
        public string NombreUsuario { get; set; }
        public string Contra { get; set; }
        public string Cargo { get; set; }
        public TipoUsuario tipoUsuario { get; set; }
        public bool Estado { get; set; }

    }
}
