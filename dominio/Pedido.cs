using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Pedido
    {
        public int Id { get; set; }
        public Venta Venta { get; set; }
        public EstadoPedido EstadoPedido { get; set; }
        public bool EstadoP { get; set; }
        public int IdUsuario { get; set; }

        
        public string NombreUsuario { get; set; }
    }
}
