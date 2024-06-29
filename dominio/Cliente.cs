using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Cliente : Persona
    {
        public int Id { get; set; }
        public DateTime FechaAlta { get; set; }
        public string Cuit { get; set; }
        public new bool Estado { get; set; }

    }
}
