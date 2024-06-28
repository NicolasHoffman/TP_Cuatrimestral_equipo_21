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
               datos.setearConsulta("Select P.Id, P.Nombre, P.Telefono, P.Email,P.IdDireccion, P.Cuit, P.Estado , Dire.Calle,Dire.Numero, Dire.Departamento, Dire.Piso, Dire.Localidad, Dire.Provincia, dire.CodigoPostal from Proveedor P inner join Direccion Dire ON Dire.Id = P.IdDireccion");
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
                    aux.Direccion.Departamento = (int)datos.Lector["Departamento"];
                    aux.Direccion.Piso = (int)datos.Lector["Piso"];
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
                datos.setearConsulta("INSERT INTO PROVEEDORES (Nombre, Telefono, Cuit, Email, Calle, Numero, Departamento, Localidad, Provincia, CodigoPostal, Estado) VALUES (@Nombre, @Telefono, @Cuit, @Email, @Calle, @Numero, @Departamento, @Localidad, @Provincia, @CodigoPostal, @Estado)");
                datos.setearParametros("@Nombre", nuevo.Nombre);
                datos.setearParametros("@Telefono", nuevo.Telefono);
                datos.setearParametros("@Cuit", nuevo.Cuit);
                datos.setearParametros("@Email", nuevo.Email);
                datos.setearParametros("@Calle", nuevo.Direccion.Calle);
                datos.setearParametros("@Numero", nuevo.Direccion.Numero);
                datos.setearParametros("@Departamento", nuevo.Direccion.Departamento);
                datos.setearParametros("@Localidad", nuevo.Direccion.Localidad);
                datos.setearParametros("@Provincia", nuevo.Direccion.Provincia);
                datos.setearParametros("@CodigoPostal", nuevo.Direccion.CodigoPostal);
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
    }
}
