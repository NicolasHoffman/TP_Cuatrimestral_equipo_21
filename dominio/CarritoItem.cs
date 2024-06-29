using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    [Serializable]
    public class CarritoItem
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public string ImagenUrl { get; set; }

        public CarritoItem(int id, string nombre, string descripcion, int cantidad, decimal precio, string imagenUrl)
        {
            Id = id;
            Nombre = nombre;
            Descripcion = descripcion;
            Cantidad = cantidad;
            Precio = precio;
            ImagenUrl = imagenUrl;
        }
        public decimal Total => Cantidad * Precio;
    }
}
