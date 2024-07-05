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
                datos.setearConsulta(@"SELECT P.Id, P.EstadoP, P.IdUsuario, V.Id AS VentaId, V.FechaVenta, V.IdCliente, V.IdVendedor, V.IdFormaDePago, V.ImporteTotal, EP.Id AS EstadoPedidoId, EP.Descripcion AS EstadoPedidoDescripcion 
                                       FROM Pedido P 
                                       INNER JOIN Venta V ON V.Id = P.IdVenta 
                                       INNER JOIN EstadoPedido EP ON EP.Id = P.IdEstadoPedido");
                datos.ejecturaLectura();

                while (datos.Lector.Read())
                {
                    Pedido aux = new Pedido
                    {
                        Id = (int)datos.Lector["Id"],
                        EstadoP = (bool)datos.Lector["EstadoP"],
                        IdUsuario = (int)datos.Lector["IdUsuario"],
                        Venta = new Venta
                        {
                            Id = (int)datos.Lector["VentaId"],
                            FechaVenta = (DateTime)datos.Lector["FechaVenta"],
                            IdCliente = (int)datos.Lector["IdCliente"],
                            IdVendedor = (int)datos.Lector["IdVendedor"],
                            IdFormaDePago = (int)datos.Lector["IdFormaDePago"],
                            ImporteTotal = (decimal)datos.Lector["ImporteTotal"]
                        },
                        EstadoPedido = new EstadoPedido
                        {
                            Id = (int)datos.Lector["EstadoPedidoId"],
                            Descripcion = (string)datos.Lector["EstadoPedidoDescripcion"]
                        }
                    };

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
                datos.setearConsulta("INSERT INTO Pedido (IdVenta, IdUsuario, IdEstadoPedido, EstadoP) VALUES (@IdVenta, @IdUsuario, @IdEstadoPedido, @EstadoP)");

                datos.setearParametros("@IdVenta", nuevo.Venta.Id);
                datos.setearParametros("@IdUsuario", nuevo.IdUsuario);
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
                datos.setearConsulta(@"SELECT P.Id, P.IdUsuario, V.Id AS IdVenta, V.FechaVenta, V.IdCliente, V.IdVendedor, V.IdFormaDePago, V.ImporteTotal, EP.Id AS IdEstadoPedido, EP.Descripcion AS EstadoPedido, P.EstadoP 
                                       FROM Pedido P 
                                       INNER JOIN Venta V ON P.IdVenta = V.Id 
                                       INNER JOIN EstadoPedido EP ON P.IdEstadoPedido = EP.Id 
                                       WHERE P.Id = @IdPedido");
                datos.setearParametros("@IdPedido", idPedido);
                datos.ejecturaLectura();

                if (datos.Lector.Read())
                {
                    pedido = new Pedido
                    {
                        Id = (int)datos.Lector["Id"],
                        IdUsuario = (int)datos.Lector["IdUsuario"],
                        Venta = new Venta
                        {
                            Id = (int)datos.Lector["IdVenta"],
                            FechaVenta = Convert.ToDateTime(datos.Lector["FechaVenta"]),
                            IdCliente = (int)datos.Lector["IdCliente"],
                            IdVendedor = (int)datos.Lector["IdVendedor"],
                            IdFormaDePago = (int)datos.Lector["IdFormaDePago"],
                            ImporteTotal = (decimal)datos.Lector["ImporteTotal"]
                        },
                        EstadoPedido = new EstadoPedido
                        {
                            Id = (int)datos.Lector["IdEstadoPedido"],
                            Descripcion = (string)datos.Lector["EstadoPedido"]
                        },
                        EstadoP = (bool)datos.Lector["EstadoP"]
                    };
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
            }
            finally
            {
                datos.cerrarConexion();
            }

            return descripcionEstadoPedido;
        }
    }
}
