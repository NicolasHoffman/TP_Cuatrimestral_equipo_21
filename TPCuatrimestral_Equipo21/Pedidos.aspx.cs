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
            var pedidos = pedidoNegocio.listar();
            

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


