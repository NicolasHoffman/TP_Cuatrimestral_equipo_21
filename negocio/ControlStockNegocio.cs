using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class ControlStockNegocio
    {
        public List<ControlStock> listar()
        {
            List<ControlStock> lista = new List<ControlStock>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
   
                datos.setearConsulta(@"SELECT CS.Id, CS.IdArticulo, CS.Stock, CS.StockMax, CS.StockMin,A.Id as IdArticulo, A.Codigo, A.Nombre, A.Descripcion, A.Precio, A.ImagenUrl, A.Estado FROM ControlStock CS  INNER JOIN ARTICULOS A ON A.Id = CS.IdArticulo");
                datos.ejecturaLectura();

                while (datos.Lector.Read())
                {
                    ControlStock aux = new ControlStock();

                    aux.Id = (int)datos.Lector["Id"];
                    aux.Stock = (int)datos.Lector["Stock"];
                    aux.StockMax = (int)datos.Lector["StockMax"];
                    aux.StockMin = (int)datos.Lector["StockMin"];

                    
                    aux.Articulo = new Articulo();
                    aux.Articulo.Id = (int)datos.Lector["IdArticulo"];
                    aux.Articulo.Codigo = (string)datos.Lector["Codigo"];
                    aux.Articulo.Nombre = (string)datos.Lector["Nombre"];
                    aux.Articulo.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Articulo.Precio = (decimal)datos.Lector["Precio"];
                    aux.Articulo.ImagenArt = (string)datos.Lector["ImagenUrl"];
                    aux.Articulo.Estado = (int)datos.Lector["Estado"];

                    lista.Add(aux);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

            return lista;
        }
    }
}
