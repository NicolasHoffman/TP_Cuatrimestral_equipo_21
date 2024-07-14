using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Mensaje
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Cuerpo { get; set; }
        public string Tipo { get; set; }
        public string RedireccionUrl { get; set; }
    }
}
