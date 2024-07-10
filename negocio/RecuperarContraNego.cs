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
        public void GenerarCodigoRecuperacion(int userId)
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

                // correo del usuario
                datos.setearConsulta("SELECT Email FROM Cliente C INNER JOIN Persona P ON C.Id = P.Id WHERE C.Estado = 0 and C.Id = @Id");
                datos.setearParametros("@Id", userId);
                datos.ejecturaLectura();

                if (datos.Lector.Read())
                {
                    string emailDestino = datos.Lector["Email"].ToString();
                    EmailService emailService = new EmailService();
                    emailService.armarCorreo(emailDestino, "Recuperación de contraseña", $"Su código de recuperación es: {codigoRecuperacion}");
                    emailService.enviarEmail();
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

        public bool ValidarCodigoRecuperacion(int userId, string codigoRecuperacion)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT Codigo, Fecha FROM RecuperacionContrasena WHERE UsuarioId = @UsuarioId AND Codigo = @Codigo");
                datos.setearParametros("@UsuarioId", userId);
                datos.ejecturaLectura();

                if (datos.Lector.Read())
                {
                    string codigoBD = datos.Lector["Codigo"].ToString();
                    DateTime fechaCodigo = Convert.ToDateTime(datos.Lector["Fecha"]);

                    // Verificar que el código no haya expirado (por ejemplo, 1 hora de validez)
                    if (codigoBD == codigo && (DateTime.Now - fechaCodigo).TotalHours < 1)
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
                datos.setearConsulta("UPDATE USUARIOS SET Contra = @Contra WHERE Id = @Id");
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
    }
}

