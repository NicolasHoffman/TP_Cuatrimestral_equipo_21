using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;

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
                    CargarCantidadNotificaciones(usuario.Id);
                }
                else
                {
                    // caso en que no haya un usuario en sesión, pero no lo voy a usar por ahora
                    //litNombreUsuario.Text = "Usuario: Invitado";
                    Response.Redirect("~/Login.aspx");
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

        private void CargarCantidadNotificaciones(int idUsuario)
        {
            //youtub min 3.45
            NotificacionNegocio notificacionNegocio = new NotificacionNegocio();
            int cantidadNotificaciones = notificacionNegocio.ContarNoLeidas(idUsuario);
            litNotificationCount.Text = cantidadNotificaciones > 0 ? $"<span class='badge bg-danger'>{cantidadNotificaciones}</span>" : string.Empty;
        }
    }

}