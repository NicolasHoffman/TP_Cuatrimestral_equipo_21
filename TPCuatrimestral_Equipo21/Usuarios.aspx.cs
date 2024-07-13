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
            if (e.CommandName == "Seleccionar")
            {
                string id = e.CommandArgument.ToString();
                Response.Redirect("FrmUsuario.aspx?id=" + id);
            }
            else if (e.CommandName == "Eliminar")
            {
                try
                {
                    string idUsu = e.CommandArgument.ToString(); // Obtener el ID de la marca del comando

                    UsuarioNegocio negocio = new UsuarioNegocio();
                    negocio.eliminar(int.Parse(idUsu));

                    //DireccionNegocio direNego = new DireccionNegocio();
                    //direNego.agregarDire();

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
