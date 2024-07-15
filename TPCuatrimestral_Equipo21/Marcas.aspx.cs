using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace TPCuatrimestral_Equipo21
{
    public partial class Marcas : System.Web.UI.Page
    {
        private readonly MarcaNegocio marcaNegocio = new MarcaNegocio();
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
                //Cargar datos 
                CargarDatos();
            }
        }
        private void CargarDatos()
        {
            rptMarcas.DataSource = marcaNegocio.listar();
            rptMarcas.DataBind();
        }
        protected void btnCrearNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("FormularioMarca.aspx");
        }

        protected void rptMarcas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar")
            {
                string id = e.CommandArgument.ToString();
                Response.Redirect("FormularioMarca.aspx?id=" + id);
            }
            else if (e.CommandName == "Eliminar")
            {
                try
                {
                    string idMarca = e.CommandArgument.ToString(); // Obtener el ID de la marca del comando

                    MarcaNegocio negocio = new MarcaNegocio();
                    negocio.eliminar(int.Parse(idMarca));

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
