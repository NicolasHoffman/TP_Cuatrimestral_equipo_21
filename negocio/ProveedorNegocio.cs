using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class ProveedorNegocio
    {
        public List<Proveedor> listar()
        {
            List<Proveedor> lista = new List<Proveedor>();
            AccesoDatos datos = new AccesoDatos();


        try
           {
               datos.setearConsulta("Select P.Id, P.Nombre, P.Telefono, P.Email,P.IdDireccion, P.Cuit, P.Estado , Dire.Calle,Dire.Numero, Dire.Departamento, Dire.Piso, Dire.Localidad, Dire.Provincia, dire.CodigoPostal from Proveedor P inner join Direccion Dire ON Dire.Id = P.IdDireccion WHERE P.Estado = 0");
               datos.ejecturaLectura();

                while (datos.Lector.Read())
                {
                    Proveedor aux = new Proveedor();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Telefono = (string)datos.Lector["Telefono"];
                    aux.Cuit = (string)datos.Lector["Cuit"];
                    aux.Email = (string)datos.Lector["Email"];
                    

                    //IMPORTANTE PARA COMPOSICION y PARA TRAER COSAS DE OTRAS TABLAS REGISTROS COMPUESTOS

                    aux.Direccion = new Direccion();
                    aux.Direccion.Calle = (string)datos.Lector["Calle"];
                    aux.Direccion.Numero = (int)datos.Lector["Numero"];
                    aux.Direccion.Departamento = (string)datos.Lector["Departamento"];
                    aux.Direccion.Piso = (string)datos.Lector["Piso"];
                    aux.Direccion.Localidad = (string)datos.Lector["Localidad"];
                    aux.Direccion.Provincia = (string)datos.Lector["Provincia"];

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

        public void eliminar(int proveedor)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Proveedor SET ESTADO = 1 WHERE Id = @Id");
                datos.setearParametros("@Id", proveedor);
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

        public void agregar (Proveedor nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO PROVEEDOR (Nombre, Telefono, Email, IdDireccion, Cuit, Estado) VALUES (@Nombre, @Telefono, @Email, @IdDireccion, @Cuit, @Estado)");
                datos.setearParametros("@Nombre", nuevo.Nombre);
                datos.setearParametros("@Telefono", nuevo.Telefono);
                datos.setearParametros("@Email", nuevo.Email);
                datos.setearParametros("@IdDireccion", nuevo.Direccion.Id);
                datos.setearParametros("@Cuit", nuevo.Cuit);
                datos.setearParametros("@Estado", nuevo.Estado);

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

        public void modificar(int id, Proveedor proveedor)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Proveedor SET Nombre = @Nombre, Telefono = @Telefono, Email = @Email, IdDireccion = @IdDireccion, Cuit = @Cuit, Estado = @Estado WHERE Id = @Id");
                datos.setearParametros("@Nombre", proveedor.Nombre);
                datos.setearParametros("@Telefono", proveedor.Telefono);
                datos.setearParametros("@Email", proveedor.Email);
                datos.setearParametros("@IdDireccion", proveedor.Direccion.Id);
                datos.setearParametros("@Cuit", proveedor.Cuit);
                datos.setearParametros("@Estado", proveedor.Estado ? 1 : 0); // Convertir bool a int (1 para true, 0 para false)
                datos.setearParametros("@Id", proveedor.Id);

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
        public Proveedor obtenerPorId(int id)
        {
            Proveedor proveedor = null;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT P.Id, P.Nombre, P.Telefono, P.Email, P.IdDireccion, P.Cuit, P.Estado, Dire.Calle, Dire.Numero, Dire.Departamento, Dire.Piso, Dire.Localidad, Dire.Provincia, Dire.CodigoPostal FROM Proveedor P INNER JOIN Direccion Dire ON Dire.Id = P.IdDireccion WHERE P.Id = @Id");
                datos.setearParametros("@Id", id);
                datos.ejecturaLectura();

                if (datos.Lector.Read())
                {
                    proveedor = new Proveedor
                    {
                        Id = (int)datos.Lector["Id"],
                        Nombre = (string)datos.Lector["Nombre"],
                        Telefono = (string)datos.Lector["Telefono"],
                        Email = (string)datos.Lector["Email"],
                        Cuit = (string)datos.Lector["Cuit"],
                        Estado = Convert.ToBoolean((int)datos.Lector["Estado"]),
                        Direccion = new Direccion
                        {
                            Id = (int)datos.Lector["IdDireccion"],
                            Calle = (string)datos.Lector["Calle"],
                            Numero = (int)datos.Lector["Numero"],
                            Departamento = (string)datos.Lector["Departamento"],
                            Piso = (string)datos.Lector["Piso"],
                            Localidad = (string)datos.Lector["Localidad"],
                            Provincia = (string)datos.Lector["Provincia"],
                            CodigoPostal = (string)datos.Lector["CodigoPostal"]
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                // Loguear error para depuración
                Console.WriteLine("Error al obtener proveedor por ID: " + ex.Message);
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

            return proveedor;
        }
    }
}
