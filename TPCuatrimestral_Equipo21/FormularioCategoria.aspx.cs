using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using dominio;
using negocio;


namespace TPCuatrimestral_Equipo21
{
    public partial class FormularioCategoria : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Validaciones.HayUsuarioEnSesion(Session))
            {
                Response.Redirect("FrmMensaje.aspx?id=13", false);
            }
            if (!Validaciones.EsUsuarioAdministradorOVendedor(Session))
            {
                Response.Redirect("FrmMensaje.aspx?id=12", false);
            }

            string id = Request.QueryString["id"];

            if (!string.IsNullOrEmpty(id) && !IsPostBack)
            {
                try
                {
                   CategoriNegocio negocio = new CategoriNegocio();
                   Categori catego = negocio.obtenerCategoriPorId(id);

                     if (catego != null)
                     {
                        //Pre cargar el nombre de la Categoria en el TextBox
                        txtNombreCategoria.Text = catego.Descripcion;
                     }

                }
                catch (Exception ex)
                {
                    Session.Add("Error", ex);
                    throw;
                }
            }

        }
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
           try
            {

                CategoriNegocio negocio = new CategoriNegocio();

                // lo uso para convertir la primera letra en mayuscula
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                string nuevaCatego = textInfo.ToTitleCase(txtNombreCategoria.Text.ToLower().Trim());

                if (Request.QueryString["id"] != null)
               {
                    int id = int.Parse(Request.QueryString["id"]);
                    negocio.modificar(id, nuevaCatego);
                }
                else
                {
                    negocio.agregar(nuevaCatego);
                }
                Response.Redirect("Categoria.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw;
            }
           
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Categoria.aspx", false);
        }
    }
}
