using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;


namespace negocio
{
    public class PedidoNegocio
    {
        public List<Pedido> listar()
        {
            List<Pedido> lista = new List<Pedido>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"SELECT P.Id, P.EstadoP,V.Id AS VentaId, V.FechaVenta, V.IdCliente, V.IdVendedor, V.IdFormaDePago, V.ImporteTotal, EP.Id AS EstadoPedidoId, EP.Descripcion AS EstadoPedidoDescripcion FROM PEDIDO P INNER JOIN VENTA V ON V.Id = P.IdVenta INNER JOIN ESTADO_PEDIDO EP ON EP.Id = P.IdEstadoPedido");
                datos.ejecturaLectura();

                while (datos.Lector.Read())
                {
                    Pedido aux = new Pedido();

                    aux.Id = (int)datos.Lector["Id"];
                    aux.EstadoP = (bool)datos.Lector["EstadoP"];

                    aux.Venta = new Venta();
                    aux.Venta.Id = (int)datos.Lector["VentaId"];
                    aux.Venta.FechaVenta = (DateTime)datos.Lector["FechaVenta"];
                    aux.Venta.IdCliente = (int)datos.Lector["IdCliente"];
                    aux.Venta.IdVendedor = (int)datos.Lector["IdVendedor"];
                    aux.Venta.IdFormaDePago = (int)datos.Lector["IdFormaDePago"];
                    aux.Venta.ImporteTotal = (decimal)datos.Lector["ImporteTotal"];

                    aux.EstadoPedido = new EstadoPedido();
                    aux.EstadoPedido.Id = (int)datos.Lector["EstadoPedidoId"];
                    aux.EstadoPedido.Descripcion = (string)datos.Lector["EstadoPedidoDescripcion"];

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

        public void agregarPedido(Pedido nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            
            try
            {
                datos.setearConsulta("INSERT INTO Pedido (IdVenta, IdEstadoPedido, EstadoP) VALUES (@IdVenta, @IdEstadoPedido, @EstadoP)");

                datos.setearParametros("@IdVenta", nuevo.Venta.Id);
                datos.setearParametros("@IdEstadoPedido", nuevo.EstadoPedido.Id);
                datos.setearParametros("@EstadoP", nuevo.EstadoP);
              
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
