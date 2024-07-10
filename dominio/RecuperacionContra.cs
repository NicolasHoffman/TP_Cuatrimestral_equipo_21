using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class RecuperacionContrasena
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Codigo { get; set; }
        public DateTime Fecha { get; set; }
    }
}
