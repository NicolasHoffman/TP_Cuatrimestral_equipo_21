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
        public ControlStock obtenerStock(int idarticulo)
        {
            ControlStock stock = null;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("select Id, IdArticulo, Stock, StockMax, StockMin from controlStock where IdArticulo = @IdArticulo");
                datos.setearParametros("@IdArticulo", idarticulo);
                datos.ejecturaLectura();

                if (datos.Lector.Read())
                {
                    stock = new ControlStock();
                    stock.Id = (int)datos.Lector["id"];
                    stock.Articulo = new Articulo();
                    stock.Articulo.Id = (int)datos.Lector["IdArticulo"];
                    stock.Stock = (int)datos.Lector["Stock"];
                    stock.StockMax = (int)datos.Lector["StockMax"];
                    stock.StockMin = (int)datos.Lector["StockMin"];
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

            return stock;
        }
        public int obtStock(int idarticulo)
        {
            int stock = 0;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("select Stock from controlStock where IdArticulo = @IdArticulo");
                datos.setearParametros("@IdArticulo", idarticulo);
                datos.ejecturaLectura();

                if (datos.Lector.Read())
                {
                    stock = (int)datos.Lector["Stock"];

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

            return stock;
        }


        public bool descontarStock(int cantidad, int idarticulo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Verificar el stock actual
                datos.setearConsulta("SELECT Stock FROM controlStock WHERE IdArticulo = @IdArticulo");
                datos.setearParametros("@IdArticulo", idarticulo);
                datos.ejecturaLectura();

                if (datos.Lector.Read())
                {
                    int stockActual = (int)datos.Lector["Stock"];

                    // Verificar si hay suficiente stock
                    if (stockActual >= cantidad)
                    {
                        datos.cerrarConexion();
                        AccesoDatos datosUpdate = new AccesoDatos();

                        datosUpdate.setearConsulta("UPDATE controlstock SET Stock = Stock - @Cantidad WHERE IdArticulo = @IdArticulo");
                        datosUpdate.setearParametros("@Cantidad", cantidad);
                        datosUpdate.setearParametros("@IdArticulo", idarticulo);
                        datosUpdate.ejecutarAccion();
                        return true;
                    }
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

            return false;
        }

        public int existenciastock(int idarticulo)
        {
            int registros = 0;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT count(IdArticulo) as registros FROM controlStock WHERE IdArticulo = @IdArticulo");
                datos.setearParametros("@IdArticulo", idarticulo);
                datos.ejecturaLectura();

                if (datos.Lector.Read())
                {
                    registros = (int)datos.Lector["registros"];

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

            return registros;
        }
    }
}
