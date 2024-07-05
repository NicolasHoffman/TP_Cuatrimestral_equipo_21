using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;

namespace TPCuatrimestral_Equipo21
{
    public partial class Pedidos : System.Web.UI.Page
    {
        private readonly PedidoNegocio pedidoNegocio = new PedidoNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            // Simular el inicio de sesión estableciendo manualmente el tipo de usuario en la sesión
            Session["tipousuario"] = 2;

            if (!IsPostBack)
            {
                //Cargar datos 
                CargarDatos();
            }
        }
        private void CargarDatos()
        {
            rptPedidos.DataSource = pedidoNegocio.listar();
            rptPedidos.DataBind();
        }
        protected void btnCrearNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("FormularioMarca.aspx");
        }


        protected void rptPedidos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar")
            {
                int idVenta = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("DetallePedido.aspx?idVenta=" + idVenta);

            }
        }

    }
           
    }


