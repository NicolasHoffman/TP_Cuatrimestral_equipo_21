using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;

namespace TPCuatrimestral_Equipo21
{
    public partial class DetalleArticulo : System.Web.UI.Page
    {
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
        }
    }
}