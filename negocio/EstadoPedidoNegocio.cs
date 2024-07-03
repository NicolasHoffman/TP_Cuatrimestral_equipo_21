using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class EstadoPedidoNegocio
    {
       public List<EstadoPedido> listar()
       {
            List<EstadoPedido> lista = new List<EstadoPedido>();

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Select Id, Descripcion from EstadoPedido");
                datos.ejecturaLectura();

                while (datos.Lector.Read())
                {
                    EstadoPedido aux = new EstadoPedido();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    lista.Add(aux);
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

    }
}
