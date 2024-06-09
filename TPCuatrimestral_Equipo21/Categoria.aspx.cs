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
    public partial class Categoria : System.Web.UI.Page
    {
        private readonly CategoriNegocio categoriNegocio = new CategoriNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                CargarDatos();
            }
        }
        private void CargarDatos()
        {
            rptCategorias.DataSource = categoriNegocio.listar();
            rptCategorias.DataBind();
        }
        protected void btnCrearNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("FormularioCategoria.aspx");
        }

        protected void rptCategorias_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar")
            {
                string id = e.CommandArgument.ToString();
                Response.Redirect("FormularioCategoria.aspx?id=" + id);
            }
            else if (e.CommandName == "Eliminar")
            {
                try
                {
                    string idCategoria = e.CommandArgument.ToString(); // Obtener el ID de la categoria del comando

             
                    CategoriNegocio negocio = new CategoriNegocio();
                    negocio.eliminar(int.Parse(idCategoria));

                    // Recargar los datos después de la eliminación
                    CargarDatos();
                }
                catch (Exception ex)
                {
                    Session.Add("Error", ex);
                    throw;
                }
            }
        }
    }
}
