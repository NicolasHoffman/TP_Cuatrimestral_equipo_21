using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;
using negocio;


namespace negocio
{
    public class RecuperarContraNego
    {
        public int ObtenerUserIdPorEmail(string email)
        {
            AccesoDatos datos = new AccesoDatos();
            int id;
            try
            {
                datos.setearConsulta("SELECT U.Id FROM Usuario U INNER JOIN Persona P ON U.Id = P.Id WHERE P.Email = @Email AND U.EstadoUsu = 0");
                datos.setearParametros("@Email", email);
                datos.ejecturaLectura();

                if (datos.Lector.Read())
                {
                    id = Convert.ToInt32(datos.Lector["Id"]);
                }
                else
                {
                    id = 0;
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
            return id;   
        }
        public string GenerarCodigoRecuperacion(int userId)
        {
            AccesoDatos datos = new AccesoDatos();

            string codigoRecuperacion = Guid.NewGuid().ToString().Substring(0, 8); // me genera un codigo random de 8 carc
            try
            {
                datos.setearConsulta("INSERT INTO RecuperacionContrasena (UsuarioId, Codigo, Fecha) VALUES (@UsuarioId, @Codigo, @Fecha)");
                datos.setearParametros("@UsuarioId", userId);
                datos.setearParametros("@Codigo", codigoRecuperacion);
                datos.setearParametros("@Fecha", DateTime.Now);
                datos.ejecutarAccion();
                return codigoRecuperacion;
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
        public bool ValidarCodigoRecuperacion(int userId, string codigoRecuperacion)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT Codigo, Fecha FROM RecuperacionContrasena WHERE UsuarioId = @UsuarioId AND Codigo = @Codigo");
                datos.setearParametros("@UsuarioId", userId);
                datos.setearParametros("@Codigo", codigoRecuperacion);
                datos.ejecturaLectura();

                if (datos.Lector.Read())
                {
                    string codigoBD = datos.Lector["Codigo"].ToString();
                    DateTime fechaCodigo = Convert.ToDateTime(datos.Lector["Fecha"]);

                    // Verificar que el código no haya expirado (lo pongo para una hor, 1 hora de validez)
                    if (codigoBD == codigoRecuperacion && (DateTime.Now - fechaCodigo).TotalHours < 1)
                    {
                        return true;
                    }
                }
                return false;
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

        public void ActualizarContrasena(int userId, string nuevaContrasena)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE USUARIO SET Contra = @Contra WHERE Id = @Id");
                datos.setearParametros("@Contra", nuevaContrasena);
                datos.setearParametros("@Id", userId);
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
        public int ObtenerUserIdDesdeCodigo(string codigo)
        {
            
            AccesoDatos datos = new AccesoDatos();
            
            try
            {
                datos.setearConsulta("SELECT UsuarioId FROM RecuperacionContrasena WHERE Codigo = @Codigo ORDER BY Fecha DESC");
                datos.setearParametros("@Codigo", codigo);
                datos.ejecturaLectura();

                if (datos.Lector.Read())
                {
                    return Convert.ToInt32(datos.Lector["UsuarioId"]);
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

