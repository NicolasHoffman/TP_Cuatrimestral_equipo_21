using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class DireccionNegocio
    {
        public void agregarDire(Direccion direccion)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO Direccion (Calle, Departamento, Numero, Piso, Provincia, Localidad, CodigoPostal) VALUES (@calle, @departamento, @numero, @piso, @provincia, @localidad, @codigoPostal)");
                datos.setearParametros("@calle", direccion.Calle);
                datos.setearParametros("@departamento", direccion.Departamento);
                datos.setearParametros("@numero", direccion.Numero);
                datos.setearParametros("@piso", direccion.Piso);
                datos.setearParametros("@provincia", direccion.Provincia);
                datos.setearParametros("@localidad", direccion.Localidad);
                datos.setearParametros("@codigoPostal", direccion.CodigoPostal);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar la dirección a la base de datos", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public int obtenerUltimoId()
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT TOP 1 Id FROM Direccion ORDER BY Id DESC");
                datos.ejecturaLectura();
                if (datos.Lector.Read())
                {
                    return (int)datos.Lector["Id"];
                }
                else
                {
                    throw new Exception("No se pudo obtener el ID de la última dirección.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el ID de la última dirección", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void modificar(int id, Direccion direccion)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Direccion SET Calle = @Calle, Departamento = @Departamento, Numero = @Numero, Piso = @Piso, Provincia = @Provincia, Localidad = @Localidad, CodigoPostal = @CodigoPostal WHERE Id = @Id");
                datos.setearParametros("@Calle", direccion.Calle);
                datos.setearParametros("@Departamento", direccion.Departamento);
                datos.setearParametros("@Numero", direccion.Numero);
                datos.setearParametros("@Piso", direccion.Piso);
                datos.setearParametros("@Provincia", direccion.Provincia);
                datos.setearParametros("@Localidad", direccion.Localidad);
                datos.setearParametros("@CodigoPostal", direccion.CodigoPostal);
                datos.setearParametros("@Id", id); 
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar la dirección en la base de datos", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        // ver si cambio el otro odificar 
        public void modificar(Direccion direccion)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Direccion SET Calle = @Calle, Departamento = @Departamento, Numero = @Numero, Piso = @Piso, Provincia = @Provincia, Localidad = @Localidad, CodigoPostal = @CodigoPostal WHERE Id = @Id");
                datos.setearParametros("@Calle", direccion.Calle);
                datos.setearParametros("@Departamento", direccion.Departamento);
                datos.setearParametros("@Numero", direccion.Numero);
                datos.setearParametros("@Piso", direccion.Piso);
                datos.setearParametros("@Provincia", direccion.Provincia);
                datos.setearParametros("@Localidad", direccion.Localidad);
                datos.setearParametros("@CodigoPostal", direccion.CodigoPostal);
                datos.setearParametros("@Id", direccion.Id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar la dirección en la base de datos", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void eliminar(int IdDireccion)
        {

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Direccion SET Estado = 1 WHERE Id  = @Id");
                datos.setearParametros("@Id", IdDireccion);
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
