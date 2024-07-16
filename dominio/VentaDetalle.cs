using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class VentaDetalle
    {
        public int Id { get; set; }
        public DateTime FechaVenta { get; set; }
        public string NombreCliente { get; set; }
        public string NombreVendedor { get; set; }
        public string FormaDePago { get; set; }
        public decimal ImporteTotal { get; set; }
    }
}
