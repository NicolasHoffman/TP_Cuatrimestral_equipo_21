using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;
using dominio;

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
                //sesion
                pedidonegocio.asignarUsuario(idVenta, 1);
                Response.Redirect("Pedidos.aspx");
            }
        }
        
        protected void btnPedidoPreparado_Click(object sender, EventArgs e)
        {
            int idVenta = Convert.ToInt32(Request.QueryString["idVenta"]);
            if (idVenta > 0)
            {
                pedidonegocio.cambiarEstado(idVenta, 3);
                Response.Redirect("Pedidos.aspx");
            }

        }
        protected void btnEntregarPedido_Click(object sender, EventArgs e)
        {
            int idVenta = Convert.ToInt32(Request.QueryString["idVenta"]);

            if (idVenta > 0)
            {
                
                pedidonegocio.cambiarEstado(idVenta, 4);

                VentaNegocio ventaNegocio = new VentaNegocio();
                int idCliente = ventaNegocio.obtenerIdClientePorIdVenta(idVenta);

                //si encuentra el id envio correo

                if (idCliente > 0)
                {
                    ClienteNegocio clienteNegocio = new ClienteNegocio();
                    Cliente cliente = clienteNegocio.obtenerClientePorId(idCliente);

                    if (cliente != null)
                    {
                        string correoCliente = cliente.Email;
                        string asunto = "hola soy el asunto";
                        EmailService emailService = new EmailService();
                        emailService.armarCorreo(correoCliente, asunto);
                        try
                        {
                            emailService.enviarEmail();
                        }
                        catch (Exception ex)
                        {
                            Session.Add("error", ex);
                        }
                    }
                    else
                    {
                        //si n ose encuentra cliente
                        Session.Add("error", new Exception("No se encontró el cliente asociado a este pedido."));
                    }
                }
            }
            else
            {
                // Manejar caso donde no se encontró el IdCliente para este IdVenta
                Session.Add("error", new Exception("No se encontró el IdCliente para este pedido."));
            }

            // Redireccionar a la página de Pedidos después de procesar
            Response.Redirect("Pedidos.aspx");

            

        }

        private void MostrarBoton(string estadoPedidoDescripcion)
        {
            // Mostrar el botón solo si el estado del pedido es "Pendiente"
            switch (estadoPedidoDescripcion)
            {
                case "Pendiente":
                    btnPrepararPedido.Visible = true;
                    btnPedidoPreparado.Visible = false;
                    btnEntregarPedido.Visible = false;
                    break;
                case "En preparación":
                    btnPrepararPedido.Visible = false;
                    btnPedidoPreparado.Visible = true;
                    btnEntregarPedido.Visible = false;
                    break;
                case "Listo para enviar":
                    btnPrepararPedido.Visible = false;
                    btnPedidoPreparado.Visible = false;
                    btnEntregarPedido.Visible = true;
                    break;
                case "En Camino":
                    btnPrepararPedido.Visible = false;
                    btnPedidoPreparado.Visible = false;
                    btnEntregarPedido.Visible = false;
                    break;
                default:
                    // En caso de un estado desconocido o nulo, ocultar todos los botones
                    btnPrepararPedido.Visible = false;
                    btnPedidoPreparado.Visible = false;
                    btnEntregarPedido.Visible = false;
                    break;
            }

        }

    }
}