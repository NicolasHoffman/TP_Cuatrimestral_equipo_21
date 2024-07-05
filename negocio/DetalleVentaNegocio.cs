using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class DetalleVentaNegocio
    {


        public List<DetalleVenta> listar()
        {
            List<DetalleVenta> lista = new List<DetalleVenta>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT Id, IdVenta, IdArticulo, Cantidad, PrecioUnitario FROM DETALLEVENTA");
                datos.ejecturaLectura();

                while (datos.Lector.Read())
                {
                    DetalleVenta aux = new DetalleVenta();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.IdVenta = (int)datos.Lector["IdVenta"];
                    aux.IdArticulo = (int)datos.Lector["IdArticulo"];
                    aux.Cantidad = (int)datos.Lector["Cantidad"];
                    aux.PrecioUnitario = (decimal)datos.Lector["PrecioUnitario"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public List<DetalleVenta> listarPorIdVenta(int idVenta)
        {
            List<DetalleVenta> lista = new List<DetalleVenta>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                
                datos.setearConsulta(@"SELECT dv.Id, dv.IdVenta, dv.IdArticulo, dv.Cantidad, dv.PrecioUnitario, a.Nombre AS NombreArticulo
                       FROM DETALLEVENTA dv
                       INNER JOIN ARTICULOS a ON a.Id = dv.IdArticulo
                       WHERE dv.IdVenta = @IdVenta");
                datos.setearParametros("@IdVenta", idVenta);
                datos.ejecturaLectura();

                while (datos.Lector.Read())
                {
                    DetalleVenta aux = new DetalleVenta();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.IdVenta = (int)datos.Lector["IdVenta"];
                    aux.IdArticulo = (int)datos.Lector["IdArticulo"];
                    aux.Cantidad = (int)datos.Lector["Cantidad"];
                    aux.PrecioUnitario = (decimal)datos.Lector["PrecioUnitario"];
                    aux.NombreArticulo = (string)datos.Lector["NombreArticulo"];
                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void agregarDetalleVenta(DetalleVenta detalleVenta)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO DETALLEVENTA (IdVenta, IdArticulo, Cantidad, PrecioUnitario) VALUES (@IdVenta, @IdArticulo, @Cantidad, @PrecioUnitario)");
                datos.setearParametros("@IdVenta", detalleVenta.IdVenta);
                datos.setearParametros("@IdArticulo", detalleVenta.IdArticulo);
                datos.setearParametros("@Cantidad", detalleVenta.Cantidad);
                datos.setearParametros("@PrecioUnitario", detalleVenta.PrecioUnitario);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
