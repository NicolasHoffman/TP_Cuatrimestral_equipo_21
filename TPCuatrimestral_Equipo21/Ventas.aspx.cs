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
                }
                else
                {
                    txtCliente.Text = "Cliente no encontrado";
                    txtCodigoProducto.Enabled = false;
                    txtNombreproducto.Enabled = false;
                    btnCrearCliente.Visible = true;
                }
            }
            else
            {
                txtCliente.Text = string.Empty; // Borra el texto cuando el campo DNI está vacío
                txtCodigoProducto.Enabled = false;
                txtNombreproducto.Enabled = false;
                btnCrearCliente.Visible = false;
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
           
            Response.Redirect("~/CrearCliente.aspx"); // Ejemplo de redirección a una página de creación de cliente
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


    }
}