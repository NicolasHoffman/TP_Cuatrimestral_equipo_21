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
