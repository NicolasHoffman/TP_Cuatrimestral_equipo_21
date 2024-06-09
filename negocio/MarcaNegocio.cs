using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class MarcaNegocio
    {
        public List<Marca> listar()
        {
            List<Marca> lista = new List<Marca>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("select Id, Nombre from MARCAS");
                datos.ejecturaLectura();

                while (datos.Lector.Read())
                {
                    Marca aux = new Marca();
                    aux.Id = (int)datos.Lector["id"];
                    aux.Nombre = (string)datos.Lector["Nombre"];

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
        public List<Marca> listar(int cantidadRegistros)
        {
            List<Marca> lista = new List<Marca>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("select Id, Nombre from MARCAS");
                datos.ejecturaLectura();

                while (datos.Lector.Read())
                {
                    Marca aux = new Marca();
                    aux.Id = (int)datos.Lector["id"];
                    aux.Nombre = (string)datos.Lector["Nombre"];

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
        public List<Marca> BuscarPorNombre(string nombre)
        {
            List<Marca> lista = new List<Marca>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT Id, Nombre FROM MARCAS WHERE Nombre LIKE @Nombre");
                datos.setearParametros("@Nombre", "%" + nombre + "%");
                datos.ejecturaLectura();

                while (datos.Lector.Read())
                {
                    Marca aux = new Marca();
                    aux.Id = (int)datos.Lector["id"];
                    aux.Nombre = (string)datos.Lector["Nombre"];

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
        public Marca obtenerMarcaPorId(string id)
        {
            Marca marca = null;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("select Id, Nombre from MARCAS where Id = @id");
                datos.setearParametros("@id", id);
                datos.ejecturaLectura();

                if (datos.Lector.Read())
                {
                    marca = new Marca();
                    marca.Id = (int)datos.Lector["id"];
                    marca.Nombre = (string)datos.Lector["Nombre"];
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

            return marca;
        }
        public void eliminar(int marca)
        {

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("DELETE FROM MARCAS WHERE Id = @Id");
                datos.setearParametros("@Id", marca);
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
        public void agregar(string Nombre)
        {

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Insert into MARCAS (Nombre) values (@nombre)");
                datos.setearParametros("@nombre", Nombre);
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
        public void modificar(int id, string nombre)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE MARCAS SET Nombre = @Nombre WHERE Id = @Id");
                datos.setearParametros("@Nombre", nombre);
                datos.setearParametros("@Id", id);
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
