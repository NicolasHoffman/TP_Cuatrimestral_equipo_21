using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;

namespace TPCuatrimestral_Equipo21
{
    public partial class FrmNotificaciones : System.Web.UI.Page
    {
        private readonly NotificacionNegocio notiNegocio = new NotificacionNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["usuario"] != null)
                {
                    var usuario = (dominio.Usuario)Session["usuario"];
                    //Cargar datos 
                    CargarDatos(usuario.tipoUsuario.TipoUsuarioId);
                }
                else
                {
                    // caso en que no haya un usuario en sesión, pero no lo voy a usar por ahora
                    //litNombreUsuario.Text = "Usuario: Invitado";
                    Response.Redirect("~/Login.aspx");
                }
                
            }
        }
        private void CargarDatos(int Tipo)
        {
            rptNoti.DataSource = notiNegocio.Listar(Tipo);
            rptNoti.DataBind();
        }
        protected void rptNoti_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
        }
        protected void chkMarcarLeido_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkMarcarLeido = (CheckBox)sender;
     
            int idNotificacion = Convert.ToInt32(chkMarcarLeido.Attributes["CommandArgument"]);
            if (chkMarcarLeido.Checked)
            {
                MarcarComoLeida(idNotificacion);
            }

        }
        private void MarcarComoLeida(int id)
        {
            NotificacionNegocio negocio = new NotificacionNegocio();
            negocio.MarcarComoLeida(id);
            // Recargar las notificaciones o actualizar la vista si es necesario
            //CargarDatos();
            Response.Redirect("~/FrmNotificaciones.aspx");
        }
    }
 }