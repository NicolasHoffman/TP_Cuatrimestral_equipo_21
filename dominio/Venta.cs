using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Venta
    {
        public int Id { get; set; }
        public DateTime FechaVenta { get; set; }
        public int IdCliente { get; set; }
        public int IdVendedor { get; set; }
        public int IdFormaDePago { get; set; }
        public decimal ImporteTotal { get; set; }

    }
}
