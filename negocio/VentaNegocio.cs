using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class VentaNegocio
    {

        public void agregarVenta(Venta nuevaVenta)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO VENTA (FechaVenta, IdCliente, IdVendedor, IdFormaDePago, ImporteTotal) VALUES (@FechaVenta, @IdCliente, @IdVendedor, @IdFormaDePago, @ImporteTotal)");
                datos.setearParametros("@FechaVenta", nuevaVenta.FechaVenta);
                datos.setearParametros("@IdCliente", nuevaVenta.IdCliente);
                datos.setearParametros("@IdVendedor", nuevaVenta.IdVendedor);
                datos.setearParametros("@IdFormaDePago", nuevaVenta.IdFormaDePago);
                datos.setearParametros("@ImporteTotal", nuevaVenta.ImporteTotal);

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
        public int obtenerUltimoIdVenta()
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT MAX(Id) AS UltimoId FROM VENTA");
                datos.ejecturaLectura();

                if (datos.Lector.Read())
                {
                    int ultimoId = Convert.ToInt32(datos.Lector["UltimoId"]);
                    return ultimoId;
                }
                else
                {
                    throw new Exception("No se encontró el último ID de venta.");
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
        }
        public int obtenerIdClientePorIdVenta(int idVenta)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IdCliente FROM VENTA WHERE Id = @IdVenta");
                datos.setearParametros("@IdVenta", idVenta);
                datos.ejecturaLectura();

                if (datos.Lector.Read())
                {
                    int idCliente = Convert.ToInt32(datos.Lector["IdCliente"]);
                    return idCliente;
                }
                else
                {
                    throw new Exception("No se encontró el IdCliente para el IdVenta proporcionado.");
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
        }

    }
}
