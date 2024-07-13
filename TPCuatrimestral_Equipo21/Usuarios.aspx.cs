using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;

namespace TPCuatrimestral_Equipo21
{
    
    public partial class Usuarios : System.Web.UI.Page
    {
        private readonly UsuarioNegocio negUsu = new UsuarioNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDatos();
            }
        }
        private void CargarDatos()
        {
            rptUsuarios.DataSource = negUsu.listar();
            rptUsuarios.DataBind();
        }
        protected void btnCrearNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmUsuario.aspx");
        }

        protected void rptUsuarios_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }
    }
}
