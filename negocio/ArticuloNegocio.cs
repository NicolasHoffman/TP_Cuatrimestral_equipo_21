﻿using System;
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
                datos.setearConsulta("INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio, ImagenUrl, IdProveedor, Estado) VALUES (@Codigo, @Nombre, @Descripcion, @IDMarca, @IDCategoria, @Precio, @ImagenUrl, @IDProveedor, @Estado)");

                datos.setearParametros("@Codigo", nuevo.Codigo);
                datos.setearParametros("@Nombre", nuevo.Nombre);
                datos.setearParametros("@Descripcion", nuevo.Descripcion);
                datos.setearParametros("@IDMarca", nuevo.Marca.Id);
                datos.setearParametros("@IDCategoria", nuevo.Categoria.Id);
                datos.setearParametros("@Precio", nuevo.Precio);
                datos.setearParametros("@ImagenUrl", nuevo.ImagenArt);
                datos.setearParametros("@IDProveedor", nuevo.Proveedor.Id);
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

        public List<Articulo> obtenerPorNombreOMarca(string terminoBusqueda)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, A.Precio, A.ImagenUrl, A.Estado, C.Descripcion Categoria, C.Id IDCategoria, M.Nombre Marca, M.Id IDMarca FROM ARTICULOS A INNER JOIN MARCAS M ON M.Id = A.IdMarca INNER JOIN CATEGORIAS C ON C.Id = A.IdCategoria WHERE A.Nombre LIKE @TerminoBusqueda OR M.Nombre LIKE @TerminoBusqueda");
                datos.setearParametros("@TerminoBusqueda", "%" + terminoBusqueda + "%");
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

                    aux.Categoria = new Categori();
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    aux.Categoria.Id = (int)datos.Lector["IDCategoria"];

                    aux.Marca = new Marca();
                    aux.Marca.Nombre = (string)datos.Lector["Marca"];
                    aux.Marca.Id = (int)datos.Lector["IDMarca"];

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

        public Articulo obtenerPorId(int id)
        {
            Articulo articulo = null;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, A.Precio, A.ImagenUrl, A.Estado, C.Descripcion Categoria, C.Id IDCategoria, M.Nombre Marca, M.Id IDMarca, PRO.Id IDProveedor FROM ARTICULOS A INNER JOIN MARCAS M ON M.Id = A.IdMarca INNER JOIN CATEGORIAS C ON C.Id = A.IdCategoria INNER JOIN PROVEEDOR PRO ON PRO.ID = A.IdProveedor WHERE A.Id = @Id");
                datos.setearParametros("@Id", id);
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
                        },
                        Proveedor= new Proveedor
                        {
                            Nombre = (string)datos.Lector["Nombre"],
                            Id = (int)datos.Lector["IDProveedor"]
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
        public void modificar(Articulo articulo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE ARTICULOS SET Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, IdMarca = @IDMarca, IdCategoria = @IDCategoria, Precio = @Precio, ImagenUrl = @ImagenUrl, IdProveedor = @IDProveedor, Estado = @Estado WHERE Id = @Id");

                datos.setearParametros("@Codigo", articulo.Codigo);
                datos.setearParametros("@Nombre", articulo.Nombre);
                datos.setearParametros("@Descripcion", articulo.Descripcion);
                datos.setearParametros("@IDMarca", articulo.Marca.Id);
                datos.setearParametros("@IDCategoria", articulo.Categoria.Id);
                datos.setearParametros("@Precio", articulo.Precio);
                datos.setearParametros("@ImagenUrl", articulo.ImagenArt);
                datos.setearParametros("@IDProveedor", articulo.Proveedor.Id);
                datos.setearParametros("@Estado", articulo.Estado);
                datos.setearParametros("@Id", articulo.Id);

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
        public void actualizarValor(string criterio, string valor, decimal porcentaje)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "UPDATE ARTICULOS SET Precio = Precio * (1 + @Porcentaje) WHERE ";
                switch (criterio.ToLower())
                {
                    case "categoria":
                        consulta += "IdCategoria = @Valor";
                        break;
                    case "marca":
                        consulta += "IdMarca = @Valor";
                        break;
                    case "codigo":
                        consulta += "Codigo = @Valor";
                        break;
                    default:
                        throw new ArgumentException("Criterio no válido");
                }

                datos.setearConsulta(consulta);
                datos.setearParametros("@Porcentaje", porcentaje / 100);
                datos.setearParametros("@Valor", valor);
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

