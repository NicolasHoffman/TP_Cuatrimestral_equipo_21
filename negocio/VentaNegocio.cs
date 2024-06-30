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
    }
}
