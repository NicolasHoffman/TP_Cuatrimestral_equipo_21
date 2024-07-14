using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
   public class MensajeNegocio
   {
        public Mensaje obtenerMensajePorId(int id)
        {
            Mensaje mensaje = null;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT Id, Titulo, Cuerpo, Tipo, RedireccionUrl FROM Mensajes WHERE Id = @Id");
                datos.setearParametros("@Id", id);
                datos.ejecturaLectura();

                if (datos.Lector.Read())
                {
                    mensaje = new Mensaje();
                    mensaje.Id = (int)datos.Lector["Id"];
                    mensaje.Titulo = (string)datos.Lector["Titulo"];
                    mensaje.Cuerpo = (string)datos.Lector["Cuerpo"];
                    mensaje.Tipo = (string)datos.Lector["Tipo"];
                    mensaje.RedireccionUrl = (string)datos.Lector["RedireccionUrl"];
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

            return mensaje;
        }
    }
}
