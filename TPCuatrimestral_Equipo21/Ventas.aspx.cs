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
        protected void Page_Load(object sender, EventArgs e)
        {

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
                }
                else
                {
                    txtCliente.Text = "Cliente no encontrado";
                }
            }
            else
            {
                txtCliente.Text = string.Empty; // Borra el texto cuando el campo DNI está vacío
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







        protected void rptVentas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar")
            {
                string id = e.CommandArgument.ToString();
                Response.Redirect("FormularioArticulo.aspx?id=" + id);
            }
            else if (e.CommandName == "Eliminar")
            {
                try
                {
                    string idMarca = e.CommandArgument.ToString(); // Obtener el ID de la marca del comando

                    // MarcaNegocio negocio = new MarcaNegocio();
                    // negocio.eliminar(int.Parse(idMarca));

                    // Recargar los datos después de la eliminación
                    //CargarDatos();
                }
                catch (Exception ex)
                {
                    Session.Add("Error", ex);
                    throw;
                }
            }
        }

    }
}