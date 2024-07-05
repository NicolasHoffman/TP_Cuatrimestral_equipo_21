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
    public partial class Ventas : System.Web.UI.Page
    {
        private int IdClienteSeleccionado;
        private string FormaEntregaSeleccionada;
        private List<CarritoItem> Carrito
        {
            get
            {
                if (ViewState["Carrito"] == null)
                {
                    ViewState["Carrito"] = new List<CarritoItem>();
                }
                return (List<CarritoItem>)ViewState["Carrito"];
            }
            set
            {
                ViewState["Carrito"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnCrearCliente.Visible = false;
                txtCodigoProducto.Enabled = false;
                txtNombreproducto.Enabled = false;

                // Cargar el carrito y actualizar el total a pagar
                Repeater1.DataSource = Carrito;
                Repeater1.DataBind();
                ActualizarTotalAPagar();

                // Verificar si hay artículos en el carrito y deshabilitar la búsqueda y creación de cliente si es necesario
                if (Carrito.Count > 0)
                {
                    txtNombreCliente.Enabled = false;
                    btnCrearCliente.Enabled = false;
                }
            }
        }
        protected void txtNombreCliente_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNombreCliente.Text.Trim()))
            {
                ClienteNegocio negocio = new ClienteNegocio();
                Cliente cliente = negocio.obtenerPorDni(txtNombreCliente.Text.Trim());

                if (cliente != null)
                {
                    txtCliente.Text = cliente.Nombre;
                    txtCodigoProducto.Enabled = true;
                    txtNombreproducto.Enabled = true;
                    btnCrearCliente.Visible = false;
                    IdClienteSeleccionado = cliente.Id;

                    // Asignar la dirección del cliente al campo oculto
                    hdnClienteDireccion.Value = $"{cliente.Direccion.Calle} {cliente.Direccion.Numero}, {cliente.Direccion.Localidad}, {cliente.Direccion.Provincia}";
                }
                else
                {
                    txtCliente.Text = "Cliente no encontrado";
                    txtCodigoProducto.Enabled = false;
                    txtNombreproducto.Enabled = false;
                    btnCrearCliente.Visible = true;
                    hdnClienteDireccion.Value = "";
                }
            }
            else
            {
                txtCliente.Text = string.Empty; // Borra el texto cuando el campo DNI está vacío
                txtCodigoProducto.Enabled = false;
                txtNombreproducto.Enabled = false;
                btnCrearCliente.Visible = false;
                hdnClienteDireccion.Value = "";
            }
        }
        protected void txtCodigoProducto_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCodigoProducto.Text.Trim()))
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                Articulo art = negocio.obtenerPorCodigo(txtCodigoProducto.Text.Trim());

                if (art != null)
                {
                    List<Articulo> listaArticulos = new List<Articulo> { art };
                    rptVentas.DataSource = listaArticulos;
                    rptVentas.DataBind();
                }
                else
                {
                    rptVentas.DataSource = null;
                    rptVentas.DataBind();
                }
            }
            else
            {
                rptVentas.DataSource = null;
                rptVentas.DataBind(); // Borra el contenido cuando el campo Código está vacío
            }
        }
        protected void txtNombreproducto_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNombreproducto.Text.Trim()))
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                List<Articulo> art = negocio.obtenerPorNombreOMarca(txtNombreproducto.Text.Trim());

                if (art != null && art.Count > 0)
                {
                    rptVentas.DataSource = art;
                    rptVentas.DataBind();
                }
                else
                {
                    rptVentas.DataSource = null;
                    rptVentas.DataBind();
                }
            }
            else
            {
                rptVentas.DataSource = null;
                rptVentas.DataBind();
            }
        }
        protected void rptVentas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Agregar")
            {
                int articuloId = Convert.ToInt32(e.CommandArgument);
                TextBox txtCantidad = (TextBox)e.Item.FindControl("txtCantidad");
                int cantidad = int.Parse(txtCantidad.Text);

                ArticuloNegocio articuloNegocio = new ArticuloNegocio();
                Articulo producto = articuloNegocio.obtenerPorId(articuloId);

                CarritoItem carritoItem = Carrito.FirstOrDefault(i => i.Id == articuloId);

                if (carritoItem != null)
                {
                    carritoItem.Cantidad += cantidad;
                }
                else
                {
                    carritoItem = new CarritoItem(producto.Id, producto.Nombre, producto.Descripcion, cantidad, producto.Precio, producto.ImagenArt);
                    Carrito.Add(carritoItem);
                }

                Repeater1.DataSource = Carrito;
                Repeater1.DataBind();

                ActualizarTotalAPagar();

                // Deshabilitar la búsqueda y creación de cliente después de agregar un artículo al carrito
                txtNombreCliente.Enabled = false;
                btnCrearCliente.Enabled = false;
            }

        }


        protected void btnCrearCliente_Click(object sender, EventArgs e)
        {

            Response.Redirect("~/FormularioCliente.aspx");
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int productoId = Convert.ToInt32(e.CommandArgument);
                CarritoItem carritoItem = Carrito.FirstOrDefault(i => i.Id == productoId);

                if (carritoItem != null)
                {
                    Carrito.Remove(carritoItem);
                }

                Repeater1.DataSource = Carrito;
                Repeater1.DataBind();
            }
        }
        private decimal CalcularTotalCarrito()
        {
            decimal total = 0;

            foreach (var item in Carrito)
            {
                total += item.Precio * item.Cantidad;
            }

            return total;
        }

        private void ActualizarTotalAPagar()
        {
            decimal total = CalcularTotalCarrito();
            Label1.Text = $"S/. {total.ToString("0.00")}";
        }

        protected void btnConfirmarFormaEntrega_Click(object sender, EventArgs e)
        {
            if (rbtnDomicilio.Checked)
            {
                FormaEntregaSeleccionada = "Domicilio";
            }
            else if (rbtnDepósito.Checked)
            {
                FormaEntregaSeleccionada = "Depósito";
            }
          

           GenerarVenta();
        }
        private void GenerarVenta()
        {
            // Verifico que haya Art en el carrito y que tenga un cliente
            if (Carrito.Count > 0 && !string.IsNullOrEmpty(txtCliente.Text.Trim()))
            {
                VentaNegocio ventaNegocio = new VentaNegocio();
                DetalleVentaNegocio detalleVentaNegocio = new DetalleVentaNegocio();
                PedidoNegocio pedidoNegocio = new PedidoNegocio();

                //int idCliente = IdClienteSeleccionado;
                int idCliente = 1;

                Venta nuevaVenta = new Venta();
                nuevaVenta.IdCliente = idCliente;
                nuevaVenta.IdVendedor = 1;

                if (ddlFormaPago.SelectedValue == "Efectivo")
                {
                    nuevaVenta.IdFormaDePago = 1; // Efectivo
                }
                else if (ddlFormaPago.SelectedValue == "Transferencia Bancaria")
                {
                    nuevaVenta.IdFormaDePago = 3; // Transferencia bancaria
                }

                //nuevaVenta.IdFormaDePago = 1;
                nuevaVenta.ImporteTotal = CalcularTotalCarrito();
                //nuevaVenta.FormaDeEntrega = FormaEntregaSeleccionada; // Guardar la forma de entrega
                string forma = FormaEntregaSeleccionada;
                //nuevaVenta.FormaDeEntrega = FormaEntregaSeleccionada == "Domicilio" ? 1 : 2;

                try
                {
                    ventaNegocio.agregarVenta(nuevaVenta);

                    int idVentaGenerado = ventaNegocio.obtenerUltimoIdVenta();

                    foreach (CarritoItem item in Carrito)
                    {
                        DetalleVenta detalle = new DetalleVenta();
                        detalle.IdVenta = idVentaGenerado;
                        detalle.IdArticulo = item.Id;
                        detalle.Cantidad = item.Cantidad;
                        detalle.PrecioUnitario = item.Precio;

                        detalleVentaNegocio.agregarDetalleVenta(detalle);
                    }

                    // Guardar en la tabla Pedido si la opción seleccionada es Enviar a domicilio
                    if (FormaEntregaSeleccionada == "Domicilio")
                    {
                        Pedido nuevoPedido = new Pedido();
                        nuevoPedido.Venta = new Venta();
                        nuevoPedido.Venta.Id = idVentaGenerado;
                        nuevoPedido.EstadoPedido = new EstadoPedido();

                        nuevoPedido.EstadoPedido.Id = 1;
                        nuevoPedido.EstadoP = false;

                        pedidoNegocio.agregarPedido(nuevoPedido);
                    }

                    Carrito.Clear();
                    Repeater1.DataSource = Carrito;
                    Repeater1.DataBind();
                    ActualizarTotalAPagar();

                    Response.Write("<script>alert('Venta generada exitosamente');</script>");

                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Error al generar la venta: " + ex.Message + "');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('No se pueden generar ventas sin productos en el carrito o sin cliente seleccionado');</script>");
            }
        }
        protected void btnGenerarVenta_Click(object sender, EventArgs e)
        {
            // Verifico que haya Art en el carrito y que tenga un cliente
            if(Carrito.Count > 0 && !string.IsNullOrEmpty(txtCliente.Text.Trim())){
                VentaNegocio ventaNegocio = new VentaNegocio();
                DetalleVentaNegocio detalleVentaNegocio = new DetalleVentaNegocio();
                //int idCliente = IdClienteSeleccionado;

                int idCliente = 1;

                Venta nuevaVenta = new Venta();
                nuevaVenta.IdCliente = idCliente;
                nuevaVenta.IdVendedor = 1;
                nuevaVenta.IdFormaDePago = 1;
                nuevaVenta.ImporteTotal = CalcularTotalCarrito();
                string forma = FormaEntregaSeleccionada;

                try
                {
                    ventaNegocio.agregarVenta(nuevaVenta);

                    int idVentaGenerado = ventaNegocio.obtenerUltimoIdVenta();

                    foreach (CarritoItem item in Carrito)
                    {
                        DetalleVenta detalle = new DetalleVenta();
                        detalle.IdVenta = idVentaGenerado; 
                        detalle.IdArticulo = item.Id;
                        detalle.Cantidad = item.Cantidad;
                        detalle.PrecioUnitario = item.Precio;

                        detalleVentaNegocio.agregarDetalleVenta(detalle);
                    }

                    Carrito.Clear();
                    Repeater1.DataSource = Carrito;
                    Repeater1.DataBind();
                    ActualizarTotalAPagar();

                    Response.Write("<script>alert('Venta generada exitosamente');</script>");

                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Error al generar la venta: " + ex.Message + "');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('No se pueden generar ventas sin productos en el carrito o sin cliente seleccionado');</script>");
            }
            
        }


        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            // Limpiar datos del cliente
            txtNombreCliente.Text = string.Empty;
            txtCliente.Text = string.Empty;
            txtCodigoProducto.Text = string.Empty;
            txtNombreproducto.Text = string.Empty;

            // Limpiar estado del carrito
            Carrito.Clear();
            Repeater1.DataSource = Carrito;
            Repeater1.DataBind();

            // Limpiar lista de productos encontrados
            rptVentas.DataSource = null;
            rptVentas.DataBind();

            // Actualizar total a pagar
            ActualizarTotalAPagar();

            // Habilitar campos y botones necesarios
            txtNombreCliente.Enabled = true;
            btnCrearCliente.Enabled = true;
            txtCodigoProducto.Enabled = false;
            txtNombreproducto.Enabled = false;
            btnCrearCliente.Visible = false;
        }




    }
}