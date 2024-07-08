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
    }
}

