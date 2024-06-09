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
                datos.setearConsulta("INSERT INTO Direccion (Calle, Departamento, Numero, Piso, Provincia, Localidad, Codigo_postal) VALUES (@calle, @departamento, @numero, @piso, @provincia, @localidad, @codigoPostal)");
                // Asignar los valores de los parámetros directamente en la consulta
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
    }
}
