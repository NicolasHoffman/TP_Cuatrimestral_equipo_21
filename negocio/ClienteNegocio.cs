using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class ClienteNegocio
    {
        public List<Cliente> listar()
        {
            List<Cliente> lista = new List<Cliente>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"SELECT C.Id, P.Nombre, P.Apellido, P.Dni, P.IdDireccion, P.Email, P.Telefono, P.Estado AS PersonaEstado,
                                       C.FechaAlta, C.Estado AS ClienteEstado,
                                       Dire.Calle, Dire.Numero, Dire.Departamento, Dire.Piso, Dire.Localidad, Dire.Provincia, Dire.CodigoPostal
                                       FROM Cliente C
                                       INNER JOIN Persona P ON C.Id = P.Id
                                       INNER JOIN Direccion Dire ON Dire.Id = P.IdDireccion
                                       WHERE P.Estado = 0");
                datos.ejecturaLectura();

                while (datos.Lector.Read())
                {
                    Cliente aux = new Cliente
                    {
                        Id = (int)datos.Lector["Id"],
                        Nombre = (string)datos.Lector["Nombre"],
                        Apellido = (string)datos.Lector["Apellido"],
                        Dni = (string)datos.Lector["Dni"],
                        Email = (string)datos.Lector["Email"],
                        Telefono = (string)datos.Lector["Telefono"],
                        FechaAlta = (DateTime)datos.Lector["FechaAlta"],
                        Estado = Convert.ToBoolean(datos.Lector["ClienteEstado"]),
                        Direccion = new Direccion
                        {
                            Calle = (string)datos.Lector["Calle"],
                            Numero = (int)datos.Lector["Numero"],
                            Departamento = datos.Lector["Departamento"] as string,
                            Piso = datos.Lector["Piso"] as int? ?? 0,
                            Localidad = (string)datos.Lector["Localidad"],
                            Provincia = (string)datos.Lector["Provincia"],
                            CodigoPostal = (string)datos.Lector["CodigoPostal"]
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

        public void eliminar(int Dni)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Persona SET Estado = 1 WHERE Dni = @Dni; " +
                                     "UPDATE Cliente SET Estado = 1 WHERE IdPersona = (SELECT Id FROM Persona WHERE Dni = @Dni)");
                datos.setearParametros("@Dni", Dni);
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

        public void agregar(Cliente nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Insertar en la tabla Persona
                datos.setearConsulta("INSERT INTO Persona (Nombre, Apellido, Dni, IdDireccion, Email, Telefono, Estado) OUTPUT INSERTED.Id VALUES (@Nombre, @Apellido, @Dni, @IdDireccion, @Email, @Telefono, @Estado)");
                datos.setearParametros("@Nombre", nuevo.Nombre);
                datos.setearParametros("@Apellido", nuevo.Apellido);
                datos.setearParametros("@Dni", nuevo.Dni);
                datos.setearParametros("@IdDireccion", nuevo.Direccion.Id);
                datos.setearParametros("@Email", nuevo.Email);
                datos.setearParametros("@Telefono", nuevo.Telefono);
                datos.setearParametros("@Estado", nuevo.Estado ? 1 : 0);

                int personaId = (int)datos.ejecutarEscalar(); // Obtener el ID generado

                // Insertar en la tabla Cliente
                datos.setearConsulta("INSERT INTO Cliente (IdPersona, FechaAlta, Estado) VALUES (@Idpersona, @FechaAlta, @EstadoCliente)");
                datos.setearParametros("@IdPersona", personaId); // Usar el ID de la entidad Persona
                datos.setearParametros("@FechaAlta", DateTime.Now); // Usar la fecha del sistema
                datos.setearParametros("@EstadoCliente", nuevo.Estado ? 1 : 0);
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

        public void modificar(int id, Cliente cliente)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"UPDATE Persona SET Nombre = @Nombre, Apellido = @Apellido, Dni = @Dni, IdDireccion = @IdDireccion, 
                              Email = @Email, Telefono = @Telefono, Estado = @Estado WHERE Id = @Id");
                datos.setearParametros("@Nombre", cliente.Nombre);
                datos.setearParametros("@Apellido", cliente.Apellido);
                datos.setearParametros("@Dni", cliente.Dni);
                datos.setearParametros("@IdDireccion", cliente.Direccion.Id);
                datos.setearParametros("@Email", cliente.Email);
                datos.setearParametros("@Telefono", cliente.Telefono);
                datos.setearParametros("@Estado", cliente.Estado ? 1 : 0);
                datos.setearParametros("@Id", id);
                datos.ejecutarAccion();

                datos.setearConsulta(@"UPDATE Cliente SET FechaAlta = @FechaAlta, Estado = @Estado 
                              WHERE IdPersona = @IdPersona");
                datos.setearParametros("@FechaAlta", cliente.FechaAlta);
                datos.setearParametros("@Estado", cliente.Estado ? 1 : 0);
                datos.setearParametros("@IdPersona", id);
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

        public Cliente obtenerClientePorId(int id)
        {
            Cliente cliente = null;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"SELECT C.Id, P.Nombre, P.Apellido, P.Dni, P.IdDireccion, P.Email, P.Telefono, P.Estado AS PersonaEstado,
                                      C.FechaAlta, C.Estado AS ClienteEstado,
                                      Dire.Calle, Dire.Numero, Dire.Departamento, Dire.Piso, Dire.Localidad, Dire.Provincia, Dire.CodigoPostal
                                      FROM Cliente C
                                      INNER JOIN Persona P ON C.Id = P.Id
                                      INNER JOIN Direccion Dire ON Dire.Id = P.IdDireccion
                                      WHERE C.Id = @Id");
                datos.setearParametros("@Id", id);
                datos.ejecturaLectura();

                if (datos.Lector.Read())
                {
                    cliente = new Cliente
                    {
                        Id = (int)datos.Lector["Id"],
                        Nombre = (string)datos.Lector["Nombre"],
                        Apellido = (string)datos.Lector["Apellido"],
                        Dni = (string)datos.Lector["Dni"],
                        Email = (string)datos.Lector["Email"],
                        Telefono = (string)datos.Lector["Telefono"],
                        FechaAlta = (DateTime)datos.Lector["FechaAlta"],
                        Estado = Convert.ToBoolean(datos.Lector["ClienteEstado"]),
                        Direccion = new Direccion
                        {
                            Id = (int)datos.Lector["IdDireccion"],
                            Calle = (string)datos.Lector["Calle"],
                            Numero = (int)datos.Lector["Numero"],
                            Departamento = datos.Lector["Departamento"] as string,
                            Piso = datos.Lector["Piso"] as int? ?? 0,
                            Localidad = (string)datos.Lector["Localidad"],
                            Provincia = (string)datos.Lector["Provincia"],
                            CodigoPostal = (string)datos.Lector["CodigoPostal"]
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener cliente por ID: " + ex.Message);
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

            return cliente;
        }

        public Cliente obtenerPorDni(string dni)
        {
            Cliente cliente = null;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"SELECT C.Id, P.Nombre, P.Apellido, P.Dni, P.IdDireccion, P.Email, P.Telefono, P.Estado AS PersonaEstado,
                               C.FechaAlta, C.Estado AS ClienteEstado,
                               Dire.Calle, Dire.Numero, Dire.Departamento, Dire.Piso, Dire.Localidad, Dire.Provincia, Dire.CodigoPostal
                               FROM Cliente C
                               INNER JOIN Persona P ON C.Id = P.Id
                               INNER JOIN Direccion Dire ON Dire.Id = P.IdDireccion
                               WHERE P.Dni = @Dni");
                datos.setearParametros("@Dni", dni);
                datos.ejecturaLectura();

                if (datos.Lector.Read())
                {
                    cliente = new Cliente
                    {
                        Id = (int)datos.Lector["Id"],
                        Nombre = (string)datos.Lector["Nombre"],
                        Apellido = (string)datos.Lector["Apellido"],
                        Dni = (string)datos.Lector["Dni"],
                        Email = (string)datos.Lector["Email"],
                        Telefono = (string)datos.Lector["Telefono"],
                        FechaAlta = (DateTime)datos.Lector["FechaAlta"],
                        Estado = Convert.ToBoolean(datos.Lector["ClienteEstado"]),
                        Direccion = new Direccion
                        {
                            Calle = (string)datos.Lector["Calle"],
                            Numero = (int)datos.Lector["Numero"],
                            Departamento = datos.Lector["Departamento"] as string,
                            Piso = datos.Lector["Piso"] as int? ?? 0,
                            Localidad = (string)datos.Lector["Localidad"],
                            Provincia = (string)datos.Lector["Provincia"],
                            CodigoPostal = (string)datos.Lector["CodigoPostal"]
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

            return cliente;
        }
    }
}