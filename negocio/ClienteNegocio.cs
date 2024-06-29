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
                datos.setearConsulta(@"SELECT C.Id, P.Nombre, P.Apellido, P.Dni, P.IdDireccion, P.Email, P.Telefono, P.Estado,
                                       C.FechaAlta, C.Cuit, C.Estado AS ClienteEstado,
                                       Dire.Calle, Dire.Numero, Dire.Departamento, Dire.Piso, Dire.Localidad, Dire.Provincia, Dire.CodigoPostal
                                       FROM Cliente C
                                       INNER JOIN Persona P ON C.Id = P.Id
                                       INNER JOIN Direccion Dire ON Dire.Id = P.IdDireccion
                                       WHERE P.Estado = 0");
                datos.ejecturaLectura();

                while (datos.Lector.Read())
                {
                    Cliente aux = new Cliente();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Apellido = (string)datos.Lector["Apellido"];
                    aux.Dni = (string)datos.Lector["Dni"];
                    aux.Email = (string)datos.Lector["Email"];
                    aux.Telefono = (string)datos.Lector["Telefono"];
                    aux.FechaAlta = (DateTime)datos.Lector["FechaAlta"];
                    aux.Cuit = (string)datos.Lector["Cuit"];
                    aux.Estado = Convert.ToBoolean((int)datos.Lector["ClienteEstado"]);

                    aux.Direccion = new Direccion();
                    aux.Direccion.Calle = (string)datos.Lector["Calle"];
                    aux.Direccion.Numero = (int)datos.Lector["Numero"];
                    aux.Direccion.Departamento = (string)datos.Lector["Departamento"];
                    aux.Direccion.Piso = (int)datos.Lector["Piso"];
                    aux.Direccion.Localidad = (string)datos.Lector["Localidad"];
                    aux.Direccion.Provincia = (string)datos.Lector["Provincia"];
                    aux.Direccion.CodigoPostal = (string)datos.Lector["CodigoPostal"];

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

        public void eliminar(int cliente)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Cliente SET Estado = 1 WHERE Id = @Id");
                datos.setearParametros("@Id", cliente);
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
                datos.setearConsulta("INSERT INTO Persona (Nombre, Apellido, Dni, IdDireccion, Email, Telefono, Estado) VALUES (@Nombre, @Apellido, @Dni, @IdDireccion, @Email, @Telefono, @Estado)");
                datos.setearParametros("@Nombre", nuevo.Nombre);
                datos.setearParametros("@Apellido", nuevo.Apellido);
                datos.setearParametros("@Dni", nuevo.Dni);
                datos.setearParametros("@IdDireccion", nuevo.Direccion.Id);
                datos.setearParametros("@Email", nuevo.Email);
                datos.setearParametros("@Telefono", nuevo.Telefono);
                datos.setearParametros("@Estado", nuevo.Estado ? 1 : 0);
                datos.ejecutarAccion();

                datos.setearConsulta("INSERT INTO Cliente (Id, FechaAlta, Cuit, Estado) VALUES (@Id, @FechaAlta, @Cuit, @Estado)");
                datos.setearParametros("@Id", nuevo.Id); // Id should be the same as the Persona Id
                datos.setearParametros("@FechaAlta", nuevo.FechaAlta);
                datos.setearParametros("@Cuit", nuevo.Cuit);
                datos.setearParametros("@Estado", nuevo.Estado ? 1 : 0);
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
                datos.setearParametros("@Id", cliente.Id);
                datos.ejecutarAccion();

                datos.setearConsulta(@"UPDATE Cliente SET FechaAlta = @FechaAlta, Cuit = @Cuit, Estado = @Estado 
                                      WHERE Id = @Id");
                datos.setearParametros("@FechaAlta", cliente.FechaAlta);
                datos.setearParametros("@Cuit", cliente.Cuit);
                datos.setearParametros("@Estado", cliente.Estado ? 1 : 0);
                datos.setearParametros("@Id", cliente.Id);
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

        public Cliente obtenerPorId(int id)
        {
            Cliente cliente = null;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"SELECT C.Id, P.Nombre, P.Apellido, P.Dni, P.IdDireccion, P.Email, P.Telefono, P.Estado,
                                      C.FechaAlta, C.Cuit, C.Estado AS ClienteEstado,
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
                        Cuit = (string)datos.Lector["Cuit"],
                        Estado = Convert.ToBoolean((int)datos.Lector["ClienteEstado"]),
                        Direccion = new Direccion
                        {
                            Id = (int)datos.Lector["IdDireccion"],
                            Calle = (string)datos.Lector["Calle"],
                            Numero = (int)datos.Lector["Numero"],
                            Departamento = (string)datos.Lector["Departamento"],
                            Piso = (int)datos.Lector["Piso"],
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
    }
}