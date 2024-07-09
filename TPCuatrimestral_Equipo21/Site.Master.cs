using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPCuatrimestral_Equipo21
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["usuario"] != null)
                {
                    var usuario = (dominio.Usuario)Session["usuario"];
                    litNombreUsuario.Text = $"Usuario: {usuario.NombreUsuario}";
                }
                else
                {
                    // Manejar el caso en que no haya un usuario en sesión
                    litNombreUsuario.Text = "Usuario: Invitado";
                }
            }
        }
        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            // Limpiar la sesión
            Session.Abandon();
            Session.Clear();

            // Redirigir a la página de inicio de sesión
            Response.Redirect("Login.aspx");
        }
    }

}