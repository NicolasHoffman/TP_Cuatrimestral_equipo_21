using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using negocio;
using dominio;

namespace TPCuatrimestral_Equipo21
{
    public partial class FrmPrincipal : System.Web.UI.Page
    {
        protected string VentasPorMesJson;
        protected string VentasPorAnioJson;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Validaciones.HayUsuarioEnSesion(Session))
            {
                Response.Redirect("FrmMensaje.aspx?id=13", false);
            }
           

            if (!IsPostBack)
            {
                CargarVentasPorMes();
                CargarVentasPorAnio();
            }
        }

        private void CargarVentasPorMes()
        {
            VentaNegocio ventaNegocio = new VentaNegocio();
            List<VentasPorMes> ventasPorMes = ventaNegocio.ObtenerVentasPorMes();
            VentasPorMesJson = JsonConvert.SerializeObject(ventasPorMes);
        }

        private void CargarVentasPorAnio()
        {
            VentaNegocio ventaNegocio = new VentaNegocio();
            List<VentasPorAnio> ventasPorAnio = ventaNegocio.ObtenerVentasPorAnio();
            VentasPorAnioJson = JsonConvert.SerializeObject(ventasPorAnio);
        }

    }
}