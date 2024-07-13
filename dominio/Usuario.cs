using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Usuario:Persona
    {
        public int Id { get; set; }
        public int Legajo { get; set; }
        public string NombreUsuario { get; set; }
        public string Contra { get; set; }
        public TipoUsuario tipoUsuario { get; set; }
        public bool EstadoUsu { get; set; }

    }
}
