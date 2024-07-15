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
    public partial class Clientes : System.Web.UI.Page
    {
        private readonly ClienteNegocio clienteNegocio = new ClienteNegocio();
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
            rptClientes.DataSource = clienteNegocio.listar();
            rptClientes.DataBind();
        }
        protected void btnCrearNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("FormularioCliente.aspx");
        }
        protected void rptClientes_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar")
            {
                string dni = e.CommandArgument.ToString();
                Response.Redirect("FormularioCliente.aspx?dni=" + dni);
            }
            else if (e.CommandName == "Eliminar")
            {
                try
                {
                    string dniCliente = e.CommandArgument.ToString(); // Obtener el ID del cliente

                    ClienteNegocio negocio = new ClienteNegocio();
                    negocio.eliminar(int.Parse(dniCliente));
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