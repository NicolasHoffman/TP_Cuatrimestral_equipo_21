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
    public partial class FormularioCliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnAceptar_Click(object sender, EventArgs e)
        {

            Cliente nuevo = new Cliente();
            ClienteNegocio negocio = new ClienteNegocio();
            DireccionNegocio direccionNegocio = new DireccionNegocio();

            nuevo.Nombre = txtNombre.Text;
            nuevo.Apellido = txtApellido.Text;
            nuevo.Dni = txtDni.Text;
            nuevo.Email = txtEmail.Text;
            nuevo.Telefono = txtTelefono.Text;
            nuevo.Estado = false;
            //nuevo.FechaAlta = DateTime.Parse(txtFechaAlta.Text);
            //nuevo.Cuit = txtCuit.Text;
            //nuevo.Estado = chkEstado.Checked;

            Direccion nuevaDireccion = new Direccion();
            nuevaDireccion.Calle = txtCalle.Text;
            nuevaDireccion.Numero = int.Parse(txtNumero.Text);
            nuevaDireccion.Piso = int.Parse(txtPiso.Text);
            nuevaDireccion.Departamento = txtDepartamento.Text;
            nuevaDireccion.Provincia = txtProvincia.Text;
            nuevaDireccion.Localidad = txtLocalidad.Text;
            nuevaDireccion.CodigoPostal = txtCodigoPostal.Text;

            // Agregar nuevo Cliente
            direccionNegocio.agregarDire(nuevaDireccion);
            nuevo.Direccion = nuevaDireccion;
            nuevo.Direccion.Id = direccionNegocio.obtenerUltimoId();
            negocio.agregar(nuevo);

            Response.Redirect("Clientes.aspx", false);

        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Clientes.aspx", false);
        }
    }
}