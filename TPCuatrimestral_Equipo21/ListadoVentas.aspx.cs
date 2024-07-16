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
    public partial class ListadoVentas : System.Web.UI.Page
    {
        private readonly VentaDetalleNegocio vdNegocio = new VentaDetalleNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarVentas();
            }
        }
        private void CargarVentas()
        {
            rptVentaD.DataSource = vdNegocio.ListarVentasConDetalles();
            rptVentaD.DataBind();
        }

        protected void txtNombreCliente_TextChanged(object sender, EventArgs e)
        {
            
        }

        protected void rptVentaD_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar")
            {
                int idVenta = Convert.ToInt32(e.CommandArgument);
                
            }
        }
    }
}