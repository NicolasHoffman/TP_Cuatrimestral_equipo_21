using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace TPCuatrimestral_Equipo21
{
    public partial class FormularioArticulo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //txtId.Enabled = false; para que no pueda modi el id

            if (!Validaciones.HayUsuarioEnSesion(Session))
            {
                Response.Redirect("FrmMensaje.aspx?id=13", false);
            }
            if (!Validaciones.EsUsuarioAdministradorOVendedor(Session))
            {
                Response.Redirect("FrmMensaje.aspx?id=12", false);
            }

            try
            {
                if (!IsPostBack)
                {
                    ProveedorNegocio negProv = new ProveedorNegocio();

                    List<Proveedor> listaProveedores = negProv.listar();

                    ddlProveedor.DataSource = listaProveedores;
                    ddlProveedor.DataValueField = "Id";
                    ddlProveedor.DataTextField = "Nombre";
                    ddlProveedor.DataBind();

                    MarcaNegocio negmarca = new MarcaNegocio();

                    List<Marca> listaMarcas = negmarca.listar();
                    ddlMarca.DataSource = listaMarcas;
                    ddlMarca.DataValueField = "Id";
                    ddlMarca.DataTextField = "Nombre";
                    ddlMarca.DataBind();

                    CategoriNegocio negcat = new CategoriNegocio();

                    List<Categori> listaCategorias = negcat.listar();
                    ddlCategoria.DataSource = listaCategorias;
                    ddlCategoria.DataValueField = "Id";
                    ddlCategoria.DataTextField = "Descripcion";
                    ddlCategoria.DataBind();

                    if (Request.QueryString["id"] != null)
                    {
                        int id = int.Parse(Request.QueryString["id"]);
                        CargarArticulo(id);
                    }
                }   

            }
            catch (Exception ex)
            {
                Session.Add("error", ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }
        private void CargarArticulo(int id)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo articulo = null;

            try
            {
                articulo = negocio.obtenerPorId(id);
            }
            catch (Exception ex)
            {
                Session.Add("error", "Error al obtener Articulo: " + ex.Message);
                Response.Redirect("Error.aspx", false);
            }
            if(articulo != null)
            {
                try
                {
                    txtCodigoArticulo.Text = articulo.Codigo;

                    ddlProveedor.SelectedValue = articulo.Proveedor.Id.ToString();

                    txtNombreArticulo.Text = articulo.Nombre;

                    ddlMarca.SelectedValue = articulo.Marca.Id.ToString();

                    ddlCategoria.SelectedValue = articulo.Categoria.Id.ToString();

                    txtPrecio.Text = articulo.Precio.ToString("F2");

                    txtDescripcion.Text = articulo.Descripcion;

                    imgArticuloNuevo.ImageUrl = articulo.ImagenArt;

                }
                catch (Exception ex)
                {
                    Session.Add("error", "Error al cargar datos del Articulo en los controles: " + ex.Message);
                    Response.Redirect("Error.aspx", false);
                }
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Articulo nuevo = new Articulo();
                ArticuloNegocio negocio = new ArticuloNegocio();
               

                nuevo.Codigo = txtCodigoArticulo.Text;
                nuevo.Nombre = txtNombreArticulo.Text;
                nuevo.Descripcion = txtDescripcion.Text;
                nuevo.ImagenArt = imgArticuloNuevo.ImageUrl;
                
                nuevo.Precio = decimal.Parse(txtPrecio.Text);

                nuevo.Proveedor = new Proveedor();
                nuevo.Proveedor.Id = int.Parse(ddlProveedor.SelectedValue);
                
                 nuevo.Marca = new Marca();
                nuevo.Marca.Id = int.Parse(ddlMarca.SelectedValue);

                nuevo.Categoria = new Categori();
                nuevo.Categoria.Id = int.Parse(ddlCategoria.SelectedValue);
                nuevo.Estado = 0;

                if (Request.QueryString["id"] != null)
                {
                    nuevo.Id = int.Parse(Request.QueryString["id"]);
                    negocio.modificar(nuevo);
                    Response.Redirect("FrmMensaje.aspx?id=" + 9, false);
                }
                else
                {
                  negocio.agregar(nuevo);
                  
                  Response.Redirect("FrmMensaje.aspx?id=" + 8, false);
                }

            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
            }
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Articulos.aspx", false);
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (txtImagen.HasFile)
            {
                try
                {

                    // donde se guardara las imagenes
                    string ruta = Server.MapPath("~/Images/Articulos/");
                    string fileName = System.IO.Path.GetFileName(txtImagen.PostedFile.FileName);
                    // guardar la imagen en el servidor
                    txtImagen.PostedFile.SaveAs(System.IO.Path.Combine(ruta, fileName));
                    // actualizar la URL de la imagen en la pag
                    imgArticuloNuevo.ImageUrl = "~/Images/Articulos/" + fileName;
                    // actualizar el UpdatePanel para mostrar la nueva imagen
                    UpdatePanel1.Update();
                }
                catch (Exception ex)
                {
                    // man cualquier error que pueda ocurrir al guardar la imagen
                    Session["Error"] = ex.ToString();
                }
            }
        }

    }
}

