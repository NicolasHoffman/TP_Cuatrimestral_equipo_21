using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class UsuarioNegocio
    {
        public List<Usuario> listar()
        {
            List<Usuario> lista = new List<Usuario>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT U.Id, P.Nombre, P.Apellido FROM Usuario U INNER JOIN Persona P ON P.Id = U.Id");
                datos.ejecturaLectura();

                while (datos.Lector.Read())
                {
                    Usuario usuario = new Usuario
                    {
                        Id = (int)datos.Lector["Id"],
                        Nombre = (string)datos.Lector["Nombre"],
                        Apellido = (string)datos.Lector["Apellido"]
                    };

                    lista.Add(usuario);
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
        public Persona obtenerNombreApellidoPorId(int id)
        {
            Persona persona = null;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"SELECT p.Nombre, p.Apellido 
                                       FROM Persona p 
                                       INNER JOIN Usuario u ON u.Id = p.Id 
                                       WHERE u.Id = @id");
                datos.setearParametros("@id", id);
                datos.ejecturaLectura();

                if (datos.Lector.Read())
                {
                    persona = new Persona
                    {
                        Nombre = (string)datos.Lector["Nombre"],
                        Apellido = (string)datos.Lector["Apellido"]
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

            return persona;
        }

        public bool Loguear (Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT Id, Legajo,  NombreUsuario, Contra, Cargo, IdTipoUsuario FROM Usuario WHERE NombreUsuario = @NombreUsuario and Contra = @Contra and EstadoUsu = 0");
                datos.setearParametros("@NombreUsuario", usuario.NombreUsuario);
                datos.setearParametros("@Contra", usuario.Contra);
 
                datos.ejecturaLectura();

                while (datos.Lector.Read())
                {
                    usuario.Id = (int)datos.Lector["Id"];
                    usuario.Legajo = (int)datos.Lector["Legajo"];
                    usuario.Cargo = (string)datos.Lector["Cargo"];
                    usuario.tipoUsuario = new TipoUsuario();
                    usuario.tipoUsuario.TipoUsuarioId = (int)datos.Lector["IdTipoUsuario"];
                    return true;
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

        public string obtenerTipodeUsuario(int idUsuario)
        {

            AccesoDatos datos = new AccesoDatos();
            string descripcion = "";

            try
            {
                datos.setearConsulta(@"SELECT tp.Descripcion 
                               FROM Usuario U 
                               INNER JOIN TipoUsuario tp ON tp.Id = U.IdTipoUsuario 
                               WHERE U.Id = @IdUsuario");
                datos.setearParametros("@IdUsuario", idUsuario);
                datos.ejecturaLectura();

                if (datos.Lector.Read())
                {
                    descripcion = (string)datos.Lector["Descripcion"];
                }

                return descripcion;
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

