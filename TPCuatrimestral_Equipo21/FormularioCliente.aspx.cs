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
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["dni"] != null)
                    {
                        string dni = Request.QueryString["dni"];
                        CargarCliente(dni);
                    }
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }

        private void CargarCliente(string dni)
        {
            ClienteNegocio negocio = new ClienteNegocio();
            Cliente cliente = null;

            try
            {
                cliente = negocio.obtenerPorDni(dni);
            }
            catch (Exception ex)
            {
                Session.Add("error", "Error al obtener cliente: " + ex.Message);
                Response.Redirect("Error.aspx", false);
            }

            if (cliente != null)
            {
                try
                {
                    txtNombre.Text = cliente.Nombre;
                    txtApellido.Text = cliente.Apellido;
                    txtDni.Text = cliente.Dni;
                    txtEmail.Text = cliente.Email;
                    txtTelefono.Text = cliente.Telefono;
                    //txtFechaAlta.Text = cliente.FechaAlta.ToString("yyyy-MM-dd");

                    txtCalle.Text = cliente.Direccion.Calle;
                    txtNumero.Text = cliente.Direccion.Numero.ToString();
                    txtPiso.Text = cliente.Direccion.Piso.ToString();
                    txtDepartamento.Text = cliente.Direccion.Departamento;
                    txtProvincia.Text = cliente.Direccion.Provincia;
                    txtLocalidad.Text = cliente.Direccion.Localidad;
                    txtCodigoPostal.Text = cliente.Direccion.CodigoPostal;
                }
                catch (Exception ex)
                {
                    Session.Add("error", "Error al cargar datos del cliente en los controles: " + ex.Message);
                    Response.Redirect("Error.aspx", false);
                }
            }
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

            if (Request.QueryString["dni"] != null)
            {
                // Modificar Cliente 
                string dni = Request.QueryString["dni"];


                // Obtener el ID de la dirección existente del proveedor
                Cliente clienteExistente = negocio.obtenerPorDni(dni);
                if (clienteExistente != null)
                {
                    nuevo.Id = clienteExistente.Id;
                    nuevaDireccion.Id = clienteExistente.Direccion.Id;
                    direccionNegocio.modificar(nuevaDireccion.Id, nuevaDireccion);
                    nuevo.Direccion = nuevaDireccion;
                    negocio.modificar(nuevo.Id, nuevo);
                }
            }
            else
            {
                // Agregar nuevo Cliente
                direccionNegocio.agregarDire(nuevaDireccion);
                nuevo.Direccion = nuevaDireccion;
                nuevo.Direccion.Id = direccionNegocio.obtenerUltimoId();
                negocio.agregar(nuevo);
            }
            Response.Redirect("Clientes.aspx", false);

        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Clientes.aspx", false);
        }
    }
}