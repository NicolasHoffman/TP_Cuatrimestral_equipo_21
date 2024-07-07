using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Articulo
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Marca Marca { get; set; }
        public Categori Categoria { get; set; }
        public decimal Precio { get; set; }
        public string ImagenArt { get; set; }
        public int Estado { get; set; }
        public Proveedor Proveedor { get; set; }

        public int Stock { get; set; }
    }
}
