using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class VentaDetalleNegocio
    {
        public List<VentaDetalle> ListarVentasConDetalles()
        {
            List<VentaDetalle> lista = new List<VentaDetalle>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"select V.Id, V.FechaVenta, V.ImporteTotal, FP.Descripcion as FormaDePago, 
                (P.Nombre + ' ' + P.Apellido) as ClienteNombre, 
                ((SELECT Per.Nombre FROM Persona Per
                 inner join Usuario Usu On Usu.Id = per.Id
                 where Usu.Id = V.IdVendedor) + ' ' +
                 (SELECT Per.Apellido FROM Persona Per
                 inner join Usuario Usu On Usu.Id = per.Id
                 where Usu.Id = V.IdVendedor)) as VendedorNombre 
                from Venta V 
                INNER JOIN Cliente C ON C.ID = V.IdCliente 
                INNER JOIN Persona P ON P.Id = C.IdPersona 
                INNER JOIN FormaDePago FP ON FP.Id = V.IdFormaDePago
                ");

                datos.ejecturaLectura();

                while (datos.Lector.Read())
                {
                    VentaDetalle venta = new VentaDetalle
                    {
                        Id = (int)datos.Lector["Id"],
                        FechaVenta = (DateTime)datos.Lector["FechaVenta"],
                        NombreCliente = (string)datos.Lector["ClienteNombre"],
                        NombreVendedor = (string)datos.Lector["VendedorNombre"],
                        FormaDePago = (string)datos.Lector["FormaDePago"],
                        ImporteTotal = (decimal)datos.Lector["ImporteTotal"]
                    };

                    lista.Add(venta);
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
    }
}
