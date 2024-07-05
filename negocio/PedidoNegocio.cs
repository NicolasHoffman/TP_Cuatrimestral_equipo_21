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
                datos.setearConsulta(@"SELECT P.Id, P.EstadoP,V.Id AS VentaId, V.FechaVenta, V.IdCliente, V.IdVendedor, V.IdFormaDePago, V.ImporteTotal, EP.Id AS EstadoPedidoId, EP.Descripcion AS EstadoPedidoDescripcion FROM PEDIDO P INNER JOIN VENTA V ON V.Id = P.IdVenta INNER JOIN ESTADOPEDIDO EP ON EP.Id = P.IdEstadoPedido");
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
        public void cambiarEstado(int idVenta, int idEstadoPedido)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Pedido SET IdEstadoPedido = @IdEstadoPedido WHERE IdVenta = @IdVenta");
                datos.setearParametros("@IdVenta", idVenta);
                datos.setearParametros("@IdEstadoPedido", idEstadoPedido);

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

        public Pedido obtenerPedido(int idPedido)
        {
            Pedido pedido = null;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"SELECT p.Id, v.Id as IdVenta, v.FechaVenta, v.IdCliente, v.IdVendedor, v.IdFormaDePago, v.ImporteTotal, ep.Id as IdEstadoPedido, ep.Descripcion as EstadoPedido, p.EstadoP FROM Pedido p INNER JOIN Venta v ON p.IdVenta = v.Id INNER JOIN EstadoPedido ep ON p.IdEstadoPedido = ep.Id WHERE p.Id = @IdPedido");
        datos.setearParametros("@IdPedido", idPedido);
        datos.ejecturaLectura();

        if (datos.Lector.Read())
        {
            pedido = new Pedido();
            pedido.Id = (int)datos.Lector["Id"];
            pedido.Venta = new Venta
            {
                Id = (int)datos.Lector["IdVenta"],
                FechaVenta = Convert.ToDateTime(datos.Lector["FechaVenta"]),
                IdCliente = (int)datos.Lector["IdCliente"],
                IdVendedor = (int)datos.Lector["IdVendedor"],
                IdFormaDePago = (int)datos.Lector["IdFormaDePago"],
                ImporteTotal = (decimal)datos.Lector["ImporteTotal"]
            };
            pedido.EstadoPedido = new EstadoPedido
            {
                Id = (int)datos.Lector["IdEstadoPedido"],
                Descripcion = (string)datos.Lector["EstadoPedido"]
            };
            pedido.EstadoP = (bool)datos.Lector["EstadoP"];
        }

            }
            catch (Exception ex)
            {
                throw ex;
                Console.WriteLine("Error al obtener el pedido: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }

            return pedido;
        }

        public string ObtenerEstadoPedidoDescripcion(int idVenta)
        {
            string descripcionEstadoPedido = string.Empty;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"SELECT EP.Descripcion AS EstadoPedidoDescripcion 
                               FROM Pedido P 
                               INNER JOIN EstadoPedido EP ON P.IdEstadoPedido = EP.Id 
                               WHERE P.IdVenta = @IdVenta");
                datos.setearParametros("@IdVenta", idVenta);
                datos.ejecturaLectura();

                if (datos.Lector.Read())
                {
                    descripcionEstadoPedido = Convert.ToString(datos.Lector["EstadoPedidoDescripcion"]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
                Console.WriteLine("Error al obtener la descripción del estado del pedido: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }

            return descripcionEstadoPedido;
        }

    }
}
