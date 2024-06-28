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
    public partial class FormularioProveedor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["id"] != null)
                    {
                        int id = int.Parse(Request.QueryString["id"]);
                        CargarProveedor(id);
                    }
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }
        private void CargarProveedor(int id)
        {
            ProveedorNegocio negocio = new ProveedorNegocio();
            Proveedor proveedor = null;
            try
            {
                proveedor = negocio.obtenerPorId(id);
            }
            catch (Exception ex)
            {
                Session.Add("error", "Error al obtener proveedor: " + ex.Message);
                Response.Redirect("Error.aspx", false);
            }

            if (proveedor != null)
            {
                try
                {
                    txtNombreProveedor.Text = proveedor.Nombre;
                    txtTelefono.Text = proveedor.Telefono;
                    txtEmail.Text = proveedor.Email;
                    txtCuit.Text = proveedor.Cuit;

                    txtCalle.Text = proveedor.Direccion.Calle;
                    txtNumero.Text = proveedor.Direccion.Numero.ToString();
                    txtPiso.Text = proveedor.Direccion.Piso.ToString();
                    txtDepartamento.Text = proveedor.Direccion.Departamento;
                    txtProvincia.Text = proveedor.Direccion.Provincia;
                    txtLocalidad.Text = proveedor.Direccion.Localidad;
                    txtCodigoPostal.Text = proveedor.Direccion.CodigoPostal;
                }
                catch (Exception ex)
                {
                    Session.Add("error", "Error al cargar datos del proveedor en los controles: " + ex.Message);
                    Response.Redirect("Error.aspx", false);
                }
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Proveedor nuevo = new Proveedor();
                ProveedorNegocio negocio = new ProveedorNegocio();
                DireccionNegocio direccionNegocio = new DireccionNegocio();

                nuevo.Nombre = txtNombreProveedor.Text;
                nuevo.Telefono = txtTelefono.Text;
                nuevo.Email = txtEmail.Text;
                nuevo.Cuit = txtCuit.Text;
                nuevo.Estado = false;

                Direccion nuevaDireccion = new Direccion();
                nuevaDireccion.Calle = txtCalle.Text;
                nuevaDireccion.Numero = int.Parse(txtNumero.Text);
                nuevaDireccion.Piso = int.Parse(txtPiso.Text);
                nuevaDireccion.Departamento = txtDepartamento.Text;
                nuevaDireccion.Provincia = txtProvincia.Text;
                nuevaDireccion.Localidad = txtLocalidad.Text;
                nuevaDireccion.CodigoPostal = txtCodigoPostal.Text;

                if (Request.QueryString["id"] != null)
                {
                    // Modificar Proveedor existente
                    int id = int.Parse(Request.QueryString["id"]);
                    nuevo.Id = id;

                    // Obtener el ID de la dirección existente del proveedor
                    Proveedor proveedorExistente = negocio.obtenerPorId(id);
                    if (proveedorExistente != null)
                    {
                        nuevaDireccion.Id = proveedorExistente.Direccion.Id;
                        direccionNegocio.modificar(nuevaDireccion.Id, nuevaDireccion);
                    }

                    nuevo.Direccion = nuevaDireccion;
                    negocio.modificar(id, nuevo);

                }
                else
                {
                // agreg nuevo Proveedor
                direccionNegocio.agregarDire(nuevaDireccion);
                nuevo.Direccion = nuevaDireccion;
                nuevo.Direccion.Id = direccionNegocio.obtenerUltimoId();
                negocio.agregar(nuevo);
                
                }
                Response.Redirect("Proveedores.aspx", false);

            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Proveedores.aspx", false);
        }
    }
}