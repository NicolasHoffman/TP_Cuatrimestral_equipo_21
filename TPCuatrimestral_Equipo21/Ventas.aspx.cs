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
                    txtCliente.Text = cliente.Nombre; // Asumiendo que `NombreCompleto` es una propiedad que concatenará Nombre y Apellido
                }
                else
                {
                    txtCliente.Text = "Cliente no encontrado"; // O manejar algún mensaje de error
                }
            }
        }
    }
}