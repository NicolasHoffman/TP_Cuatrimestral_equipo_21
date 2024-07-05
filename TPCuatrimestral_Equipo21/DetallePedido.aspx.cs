using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;

namespace TPCuatrimestral_Equipo21
{
    public partial class DetallePedido : System.Web.UI.Page
    {
        private readonly DetalleVentaNegocio detalleventanegocio = new DetalleVentaNegocio();
        private readonly PedidoNegocio pedidonegocio = new PedidoNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Request.QueryString["idVenta"] != null)
                {
                    int idVenta = Convert.ToInt32(Request.QueryString["idVenta"]);

                    CargarDatos(idVenta);
                }
            }
        }

        private void CargarDatos(int idVenta)
        {
            rptDetallePedido.DataSource = detalleventanegocio.listarPorIdVenta(idVenta);
            rptDetallePedido.DataBind();

            string estadoPedidoDescripcion = pedidonegocio.ObtenerEstadoPedidoDescripcion(idVenta);
            MostrarBoton(estadoPedidoDescripcion);

        }
        protected void btnCrearNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("FormularioMarca.aspx");
        }
        protected void rptDetallePedido_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Pedidos.aspx");
        }
        protected void btnPrepararPedido_Click(object sender, EventArgs e)
        {
            int idVenta = Convert.ToInt32(Request.QueryString["idVenta"]);
            if (idVenta > 0)
            {
                pedidonegocio.cambiarEstado(idVenta, 2);
                Response.Redirect("Pedidos.aspx");
            }

        }
        

        protected void btnPedidoListo_Click(object sender, EventArgs e)
        {
            int idVenta = Convert.ToInt32(Request.QueryString["idVenta"]);
            if (idVenta > 0)
            {
                pedidonegocio.cambiarEstado(idVenta, 3);
                Response.Redirect("Pedidos.aspx");
            }

        }
        protected void btnPedidoEntregado_Click(object sender, EventArgs e)
        {
            int idVenta = Convert.ToInt32(Request.QueryString["idVenta"]);
            if (idVenta > 0)
            {
                pedidonegocio.cambiarEstado(idVenta, 5);
                Response.Redirect("Pedidos.aspx");
            }

        }

        private void MostrarBoton(string estadoPedidoDescripcion)
        {
            // Mostrar el botón solo si el estado del pedido es "Pendiente"
            switch (estadoPedidoDescripcion)
            {
                case "Pendiente":
                    btnPrepararPedido.Visible = true;
                    btnPedidoListo.Visible = false;
                    btnPedidoEntregdo.Visible = false;
                    break;
                case "En preparación":
                    btnPrepararPedido.Visible = false;
                    btnPedidoListo.Visible = true;
                    btnPedidoEntregdo.Visible = false;
                    break;
                case "Listo para enviar":
                    btnPrepararPedido.Visible = false;
                    btnPedidoListo.Visible = false;
                    btnPedidoEntregdo.Visible = true;
                    break;
                case "Entregado":
                    btnPrepararPedido.Visible = false;
                    btnPedidoListo.Visible = false;
                    break;
                default:
                    // En caso de un estado desconocido o nulo, ocultar todos los botones
                    btnPrepararPedido.Visible = false;
                    btnPedidoListo.Visible = false;

                    break;
            }

        }

    }
}