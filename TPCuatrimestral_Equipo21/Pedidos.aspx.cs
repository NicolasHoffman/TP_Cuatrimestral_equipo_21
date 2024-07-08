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
        private readonly UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
        private readonly EstadoPedidoNegocio estadoPedidoNegocio = new EstadoPedidoNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            // Simular el inicio de sesión estableciendo manualmente el tipo de usuario en la sesión
            Session["tipousuario"] = 2;

            if (!IsPostBack)
            {
                //Cargar datos 
                CargarEstados();
                CargarPersonas();
                CargarDatos();
            }
        }
        private void CargarEstados()
        {
            var estados = estadoPedidoNegocio.listar();
            ddlEstadoPedido.DataSource = estados;
            ddlEstadoPedido.DataTextField = "Descripcion";
            ddlEstadoPedido.DataValueField = "Id";
            ddlEstadoPedido.DataBind();
            ddlEstadoPedido.Items.Insert(0, new ListItem("Seleccione un estado", "0"));
        }
        private void CargarPersonas()
        {
            var personas = usuarioNegocio.listar();
            foreach (var persona in personas)
            {
                persona.Nombre = persona.Nombre + " " + persona.Apellido;
            }

            ddlPersonaAsignada.DataSource = personas;
            ddlPersonaAsignada.DataTextField = "Nombre"; 
            ddlPersonaAsignada.DataValueField = "Id";
            ddlPersonaAsignada.DataBind();
            ddlPersonaAsignada.Items.Insert(0, new ListItem("Seleccione una persona", "0"));
            ddlPersonaAsignada.Items.Insert(1, new ListItem("Sin asignar", "-1"));
        }
        private void CargarDatos(int estadoId = 0, int personaId = 0)
        {
            var pedidos = pedidoNegocio.listar();

            if (estadoId != 0)
            {
                pedidos = pedidos.Where(p => p.EstadoPedido.Id == estadoId).ToList();
            }
            if (personaId != 0)
            {
                if (personaId == -1)
                {
                    // Filtrar pedidos sin asignar
                    pedidos = pedidos.Where(p => p.IdUsuario == 0).ToList();
                }
                else
                {
                    // Filtrar pedidos asignados a una persona específica
                    pedidos = pedidos.Where(p => p.IdUsuario == personaId).ToList();
                }
            }

            foreach (var pedido in pedidos)
            {
                if (pedido.IdUsuario != 0)
                {
                    var usuario = usuarioNegocio.obtenerNombreApellidoPorId(pedido.IdUsuario);
                    if (usuario != null)
                    {
                        pedido.NombreUsuario = usuario.Nombre + " " + usuario.Apellido;
                    }
                    else
                    {
                        pedido.NombreUsuario = "Desconocido";
                    }
                }
                else
                {
                    pedido.NombreUsuario = "Sin asignar";
                }
            }

            rptPedidos.DataSource = pedidos;
            rptPedidos.DataBind();
        }
        protected void btnCrearNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("FormularioMarca.aspx");
        }
        protected void ddlEstadoPedido_SelectedIndexChanged(object sender, EventArgs e)
        {
            int estadoId = int.Parse(ddlEstadoPedido.SelectedValue);
            int personaId = int.Parse(ddlPersonaAsignada.SelectedValue);
            CargarDatos(estadoId, personaId);
        }

        protected void ddlPersonaAsignada_SelectedIndexChanged(object sender, EventArgs e)
        {
            int estadoId = int.Parse(ddlEstadoPedido.SelectedValue);
            int personaId = int.Parse(ddlPersonaAsignada.SelectedValue);
            CargarDatos(estadoId, personaId);
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


