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
    public partial class FormularioMarca : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string id = Request.QueryString["id"];

            if (!string.IsNullOrEmpty(id) && !IsPostBack)
            {
                try
                {
                    MarcaNegocio negocio = new MarcaNegocio();
                    Marca marca = negocio.obtenerMarcaPorId(id);

                    if (marca != null)
                    {
                        // Pre cargar el nombre de la marca en el TextBox
                        txtNombreMarca.Text = marca.Nombre;
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
                
                MarcaNegocio negocio = new MarcaNegocio();

                String nuevaMarca = txtNombreMarca.Text.ToUpper().Trim();

                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    negocio.modificar(id, nuevaMarca);
                }
                else
                {
                    negocio.agregar(nuevaMarca);
                }
                Response.Redirect("Marcas.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw;
            }
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Marcas.aspx", false);
        }
    }
}