using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class TipoUsuarioNegocio
    {
        public void agregarTipoUsuario(TipoUsuario tipoUsuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO TipoUsuario (Descripcion, Estado) VALUES (@descripcion, @estado)");
                // Asignar los valores de los parámetros directamente en la consulta
                datos.setearParametros("@descripcion", tipoUsuario.Descripcion);
                datos.setearParametros("@estado", tipoUsuario.Estado);
            
                datos.ejecutarAccion();
            }
             
            catch (Exception ex)
            {
                throw new Exception("Error al agregar Tipo Usuario a la base de datos", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }    
    }
}
