using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;


namespace TPCuatrimestral_Equipo21
{
    public partial class Articulos : System.Web.UI.Page
    {
        private readonly ArticuloNegocio articuloNegocio = new ArticuloNegocio();
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
            if (!IsPostBack)
            {
                CargarDatos();
            }
        }
        private void CargarDatos()
        {
            rptArticulos.DataSource = articuloNegocio.listar();
            rptArticulos.DataBind();
        }
        protected void btnCrearNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("FormularioArticulo.aspx");
        }
        protected void rptArticulos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar")
            {
                string id = e.CommandArgument.ToString();
                Response.Redirect("FormularioArticulo.aspx?id=" + id);
            }
            else if (e.CommandName == "Eliminar")
            {
                try
                {
                    string idMarca = e.CommandArgument.ToString(); // Obtener el ID de la marca del comando

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
