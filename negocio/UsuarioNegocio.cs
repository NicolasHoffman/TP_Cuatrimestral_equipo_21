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
            {   // COMENTO POR AHORA 
                //datos.setearConsulta("SELECT U.Id, P.Nombre, P.Apellido FROM Usuario U INNER JOIN Persona P ON P.Id = U.Id");
                datos.setearConsulta("SELECT U.Id,U.Legajo,U.NombreUsuario, P.Nombre, P.Apellido, TP.Descripcion FROM Usuario U INNER JOIN Persona P ON P.Id = U.Id INNER JOIN TipoUsuario TP ON TP.Id = U.IdTipoUsuario");
                datos.ejecturaLectura();

                while (datos.Lector.Read())
                {
                    Usuario usuario = new Usuario
                    {
                        Id = (int)datos.Lector["Id"],
                        Legajo = (int)datos.Lector["Legajo"],
                        NombreUsuario = (string)datos.Lector["NombreUsuario"],
                        Nombre = (string)datos.Lector["Nombre"],
                        Apellido = (string)datos.Lector["Apellido"],
                        tipoUsuario = new TipoUsuario
                        {
                            Descripcion = (string)datos.Lector["Descripcion"]
                        }
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

        public bool Loguear(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT Id, Legajo,  NombreUsuario, Contra, IdTipoUsuario FROM Usuario WHERE NombreUsuario = @NombreUsuario and Contra = @Contra and EstadoUsu = 0");
                datos.setearParametros("@NombreUsuario", usuario.NombreUsuario);
                datos.setearParametros("@Contra", usuario.Contra);

                datos.ejecturaLectura();

                while (datos.Lector.Read())
                {
                    usuario.Id = (int)datos.Lector["Id"];
                    usuario.Legajo = (int)datos.Lector["Legajo"];
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
        public void agregar(Usuario nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                int siguienteLegajo = obtenerSiguienteLegajo();
                nuevo.Legajo = siguienteLegajo;
                // Insertar en la tabla Persona
                datos.setearConsulta("INSERT INTO Persona (Nombre, Apellido, Dni, IdDireccion, Email, Telefono) OUTPUT INSERTED.Id VALUES (@Nombre, @Apellido, @Dni, @IdDireccion, @Email, @Telefono)");
                datos.setearParametros("@Nombre", nuevo.Nombre);
                datos.setearParametros("@Apellido", nuevo.Apellido);
                datos.setearParametros("@Dni", nuevo.Dni);
                datos.setearParametros("@IdDireccion", nuevo.Direccion.Id);
                datos.setearParametros("@Email", nuevo.Email);
                datos.setearParametros("@Telefono", nuevo.Telefono);
                //datos.setearParametros("@Estado", nuevo.Estado ? 1 : 0);

                int personaId = (int)datos.ejecutarEscalar(); // Obtener el ID generado

                // Insertar en la tabla Usuario
                datos.setearConsulta("INSERT INTO Usuario (Legajo, NombreUsuario, Contra, IdTipoUsuario) VALUES (@Legajo, @NombreUsuario, @Contra, @IdTipoUsuario)");
                datos.setearParametros("@NombreUsuario", nuevo.NombreUsuario);
                datos.setearParametros("@Contra", nuevo.Contra);
                datos.setearParametros("@IdTipoUsuario", nuevo.tipoUsuario.TipoUsuarioId);
                datos.setearParametros("@Legajo", nuevo.Legajo);
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
        public void eliminar(int idUsu)
        {

            AccesoDatos datos = new AccesoDatos();

            try
            {

                datos.setearConsulta("UPDATE Usuario SET EstadoUsu = 1 WHERE Id = @Id");
                datos.setearParametros("@Id", idUsu);
                datos.ejecutarAccion();

                datos.cerrarConexion();

                AccesoDatos datos2 = new AccesoDatos();
                datos2.setearConsulta("UPDATE Persona SET Estado = 1 WHERE Id = @Id");
                datos2.setearParametros("@Id", idUsu);
                datos2.ejecutarAccion();
                datos2.cerrarConexion();
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
        public Usuario obtenerPorId(int idUsu)
        {
            Usuario usuario = null;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT U.Id, U.Legajo, U.NombreUsuario, U.Contra, U.IdTipoUsuario, P.Nombre, P.Apellido, P.Dni, P.Email, P.Telefono, D.Id AS IdDireccion, D.Calle, D.Departamento, D.Numero, D.Piso, D.Provincia, D.Localidad, D.CodigoPostal, TP.Descripcion AS TipoUsuarioDescripcion FROM Usuario U INNER JOIN Persona P ON P.Id = U.Id INNER JOIN Direccion D ON P.IdDireccion = D.Id INNER JOIN TipoUsuario TP ON TP.Id = U.IdTipoUsuario WHERE U.Id = @Id");
                datos.setearParametros("@Id", idUsu);
                datos.ejecturaLectura();

                if (datos.Lector.Read())
                {
                    usuario = new Usuario
                    {
                        Id = (int)datos.Lector["Id"],
                        Legajo = (int)datos.Lector["Legajo"],
                        NombreUsuario = (string)datos.Lector["NombreUsuario"],
                        Contra = (string)datos.Lector["Contra"],
                        Nombre = (string)datos.Lector["Nombre"],
                        Apellido = (string)datos.Lector["Apellido"],
                        Dni = (string)datos.Lector["Dni"],
                        Email = (string)datos.Lector["Email"],
                        Telefono = (string)datos.Lector["Telefono"],
                        Direccion = new Direccion
                        {
                            Id = (int)datos.Lector["IdDireccion"],
                            Calle = (string)datos.Lector["Calle"],
                            Departamento = datos.Lector["Departamento"] != DBNull.Value ? (string)datos.Lector["Departamento"] : "",
                            Numero = (int)datos.Lector["Numero"],
                            Piso = datos.Lector["Piso"] != DBNull.Value ? (string)datos.Lector["Piso"] : "",
                            Provincia = (string)datos.Lector["Provincia"],
                            Localidad = (string)datos.Lector["Localidad"],
                            CodigoPostal = (string)datos.Lector["CodigoPostal"]
                        },
                        tipoUsuario = new TipoUsuario
                        {
                            TipoUsuarioId = (int)datos.Lector["IdTipoUsuario"],
                            Descripcion = (string)datos.Lector["TipoUsuarioDescripcion"]
                        }
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

            return usuario;
        }

        public void modificar(Usuario usuario)
        {


            try
            {
                AccesoDatos datos = new AccesoDatos();

                // Modificar en la tabla Persona
                datos.setearConsulta("UPDATE Persona SET Nombre = @Nombre, Apellido = @Apellido, Dni = @Dni, Email = @Email, Telefono = @Telefono WHERE Id = @Id");
                datos.setearParametros("@Nombre", usuario.Nombre);
                datos.setearParametros("@Apellido", usuario.Apellido);
                datos.setearParametros("@Dni", usuario.Dni);
                datos.setearParametros("@Email", usuario.Email);
                datos.setearParametros("@Telefono", usuario.Telefono);
                datos.setearParametros("@Id", usuario.Id);
                datos.ejecutarAccion();
                datos.cerrarConexion();

                AccesoDatos datos1 = new AccesoDatos();
                // Modificar en la tabla Usuario
                datos1.setearConsulta("UPDATE Usuario SET NombreUsuario = @NombreUsuario, Contra = @Contra, IdTipoUsuario = @IdTipoUsuario WHERE Id = @Id");
                datos1.setearParametros("@NombreUsuario", usuario.NombreUsuario);
                datos1.setearParametros("@Contra", usuario.Contra);
                datos1.setearParametros("@IdTipoUsuario", usuario.tipoUsuario.TipoUsuarioId);
                datos1.setearParametros("@Id", usuario.Id);
                datos1.ejecutarAccion();
                datos1.cerrarConexion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //datos.cerrarConexion();
            }
        }
        public int obtenerSiguienteLegajo()
        {
            AccesoDatos datos = new AccesoDatos();
            int siguienteLegajo = 0;

            try
            {
                datos.setearConsulta("SELECT ISNULL(MAX(Legajo), 0) AS UltimoLegajo FROM Usuario");
                datos.ejecturaLectura();

                if (datos.Lector.Read())
                {
                    siguienteLegajo = ((int)datos.Lector["UltimoLegajo"]) + 1;
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

            return siguienteLegajo;
        }
    }
}

