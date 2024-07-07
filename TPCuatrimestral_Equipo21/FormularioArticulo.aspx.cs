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

                    //ArticuloNegocio negocio = new ArticuloNegocio();
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
                    }   ddlCategoria.DataBind();

                }
                catch (Exception ex)
                {
                Session.Add("error", ex);
                throw;
                //FALTA !mandar a una pagina de error
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
                nuevo.ImagenArt = imgArticuloNuevo.ImageUrl; // RUTA DE IMAGEN
                
                nuevo.Precio = decimal.Parse(txtPrecio.Text);

                nuevo.Proveedor = new Proveedor();
                nuevo.Proveedor.Id = int.Parse(ddlProveedor.SelectedValue);
                
                 nuevo.Marca = new Marca();
                nuevo.Marca.Id = int.Parse(ddlMarca.SelectedValue);

                nuevo.Categoria = new Categori();
                nuevo.Categoria.Id = int.Parse(ddlCategoria.SelectedValue);

                if (Request.QueryString["id"] != null)
                {
                    //int id = int.Parse(Request.QueryString["id"]);
                   // negocio.modificar(id, nuevaMarca);
                }
                else
                {
                  negocio.agregar(nuevo);
                  Response.Redirect("Articulos.aspx", false);
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

                    // Ruta donde se guardarán las imágenes
                    string ruta = Server.MapPath("~/Images/Articulos/");
                    string fileName = System.IO.Path.GetFileName(txtImagen.PostedFile.FileName);
                    // Guardar la imagen en el servidor
                    txtImagen.PostedFile.SaveAs(System.IO.Path.Combine(ruta, fileName));
                    // Actualizar la URL de la imagen en la página
                    imgArticuloNuevo.ImageUrl = "~/Images/Articulos/" + fileName;
                    // Actualizar el UpdatePanel para mostrar la nueva imagen
                    UpdatePanel1.Update();
                }
                catch (Exception ex)
                {
                    // Manejar cualquier error que pueda ocurrir al guardar la imagen
                    Session["Error"] = ex.ToString();
                }
            }
        }

    }
}

