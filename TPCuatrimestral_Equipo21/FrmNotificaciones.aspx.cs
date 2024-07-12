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
                //Cargar datos 
                CargarDatos();
            }
        }
        private void CargarDatos()
        {
            rptNoti.DataSource = notiNegocio.listar();
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