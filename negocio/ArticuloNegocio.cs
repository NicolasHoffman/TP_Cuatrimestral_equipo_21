using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> listar()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, A.Precio, A.ImagenUrl, A.Estado, C.Descripcion Categoria, C.Id IDCategoria, M.Nombre Marca, M.Id IDMarca FROM ARTICULOS A INNER JOIN MARCAS M ON M.Id = A.IdMarca INNER JOIN CATEGORIAS C ON C.Id = A.IdCategoria");
                datos.ejecturaLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Precio = (decimal)datos.Lector["Precio"];
                    aux.ImagenArt = (string)datos.Lector["ImagenUrl"];
                    aux.Estado = (int)datos.Lector["Estado"];

                    //IMPORTANTE PARA COMPOSICION y PARA TRAER COSAS DE OTRAS TABLAS REGISTROS COMPUESTOS
                    aux.Categoria = new Categori();
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    aux.Categoria.Id = (int)datos.Lector["IDCategoria"];

                    aux.Marca = new Marca();
                    aux.Marca.Nombre = (string)datos.Lector["Marca"];//PARA LA LISTA DESPLEGABLE Y MODIFICAR
                    aux.Marca.Id = (int)datos.Lector["IDMarca"];//PARA LA LISTA DESPLEGABLE Y MODIFICAR

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

        public void agregar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio, ImagenUrl, Estado) VALUES (@Codigo, @Nombre, @Descripcion, @IDMarca, @IDCategoria, @Precio, @ImagenUrl, @Estado)");

                datos.setearParametros("@Codigo", nuevo.Codigo);
                datos.setearParametros("@Nombre", nuevo.Nombre);
                datos.setearParametros("@Descripcion", nuevo.Descripcion);
                datos.setearParametros("@IDMarca", nuevo.Marca.Id);
                datos.setearParametros("@IDCategoria", nuevo.Categoria.Id);
                datos.setearParametros("@Precio", nuevo.Precio);
                datos.setearParametros("@ImagenUrl", nuevo.ImagenArt);
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
        public Articulo obtenerPorCodigo(string codigo)
        {
            Articulo articulo = null;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, A.Precio, A.ImagenUrl, A.Estado, C.Descripcion Categoria, C.Id IDCategoria, M.Nombre Marca, M.Id IDMarca FROM ARTICULOS A INNER JOIN MARCAS M ON M.Id = A.IdMarca INNER JOIN CATEGORIAS C ON C.Id = A.IdCategoria WHERE A.Codigo = @Codigo");
                datos.setearParametros("@Codigo", codigo);
                datos.ejecturaLectura();

                if (datos.Lector.Read())
                {
                    articulo = new Articulo
                    {
                        Id = (int)datos.Lector["Id"],
                        Codigo = (string)datos.Lector["Codigo"],
                        Nombre = (string)datos.Lector["Nombre"],
                        Descripcion = (string)datos.Lector["Descripcion"],
                        Precio = (decimal)datos.Lector["Precio"],
                        ImagenArt = (string)datos.Lector["ImagenUrl"],
                        Estado = (int)datos.Lector["Estado"],
                        Categoria = new Categori
                        {
                            Descripcion = (string)datos.Lector["Categoria"],
                            Id = (int)datos.Lector["IDCategoria"]
                        },
                        Marca = new Marca
                        {
                            Nombre = (string)datos.Lector["Marca"],
                            Id = (int)datos.Lector["IDMarca"]
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

            return articulo;
        }
    }
}
