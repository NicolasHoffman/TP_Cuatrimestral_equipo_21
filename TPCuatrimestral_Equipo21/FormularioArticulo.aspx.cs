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
                nuevo.ImagenArt = "das";
                nuevo.Precio = decimal.Parse(txtPrecio.Text);
                // me falta ver lo de la imagen

                nuevo.Marca = new Marca();
                nuevo.Marca.Id = int.Parse(ddlMarca.SelectedValue);

                nuevo.Categoria = new Categori();
                nuevo.Categoria.Id = int.Parse(ddlCategoria.SelectedValue);


                /* IMAGEN VER DESPUES !!!
                 * 
                //string ruta = Server.MapPath("./Images/Articulos/");
                //txtImagen.PostedFile.SaveAs(ruta + "articulo-" + ".jpg");

                // Ruta donde se guardarán las imágenes
                string ruta = Server.MapPath("./Images/Articulos/");

                // Obtener el nombre del archivo
                string fileName = System.IO.Path.GetFileName(txtImagen.PostedFile.FileName);

                // Guardar la imagen con su nombre original
                txtImagen.PostedFile.SaveAs(System.IO.Path.Combine(ruta, fileName));

                // Actualizar la URL de la imagen en la página
                imgArticuloNuevo.ImageUrl = "./Images/Articulos/" + fileName;

               */

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
                string ruta = Server.MapPath("~/Images/Articulos/");
                string fileName = System.IO.Path.GetFileName(txtImagen.PostedFile.FileName);
                txtImagen.PostedFile.SaveAs(System.IO.Path.Combine(ruta, fileName));
                imgArticuloNuevo.ImageUrl = "~/Images/Articulos/" + fileName;
                UpdatePanel1.Update(); // Actualizar el UpdatePanel
            }
        }
    }
}