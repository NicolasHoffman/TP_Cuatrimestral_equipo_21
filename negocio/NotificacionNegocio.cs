using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class NotificacionNegocio
    {
        public void Agregar(Notificacion notificacion)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO Notificaciones (UsuarioDestinoId, Mensaje, Fecha, Leida) VALUES (@UsuarioDestinoId, @Mensaje, @Fecha, @Leida)");
                datos.setearParametros("@UsuarioDestinoId", notificacion.IdUsuarioDestinatario);
                datos.setearParametros("@Mensaje", notificacion.Mensaje);
                datos.setearParametros("@Fecha", notificacion.Fecha);
                datos.setearParametros("@Leida", notificacion.Leido);
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

        public List<Notificacion> Listar(int idUsuarioDestinatario)
        {
            AccesoDatos datos = new AccesoDatos();

            List<Notificacion> lista = new List<Notificacion>();
            try
            {
                datos.setearConsulta("SELECT Id, UsuarioDestionoId, Mensaje, Fecha, Leida FROM Notificaciones WHERE UsuarioDestionoId = @IdUsuarioDestinatario");
                datos.setearParametros("@IdUsuarioDestinatario", idUsuarioDestinatario);
                datos.ejecturaLectura();

                while (datos.Lector.Read())
                {
                    Notificacion notificacion = new Notificacion
                    {
                        Id = (int)datos.Lector["Id"],
                        IdUsuarioDestinatario = (int)datos.Lector["IdUsuarioDestinatario"],
                        Mensaje = (string)datos.Lector["Mensaje"],
                        Fecha = (DateTime)datos.Lector["Fecha"],
                        Leido = (bool)datos.Lector["Leida"]
                    };
                    lista.Add(notificacion);
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

        public void MarcarComoLeida(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Notificaciones SET Leida = 1 WHERE Id = @Id");
                datos.setearParametros("@Id", id);
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

        public int ContarNoLeidas(int idUsuarioDestinatario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM Notificaciones WHERE UsuarioDestinoId = @IdUsuarioDestinatario AND Leida = 0");
                datos.setearParametros("@IdUsuarioDestinatario", idUsuarioDestinatario);
                datos.ejecturaLectura();

                if (datos.Lector.Read())
                {
                    return (int)datos.Lector[0];
                }

                return 0;
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
