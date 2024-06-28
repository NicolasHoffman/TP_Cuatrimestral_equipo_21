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

        }
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Proveedor nuevo = new Proveedor();
                ProveedorNegocio negocio = new ProveedorNegocio();

                nuevo.Nombre = txtNombreProveedor.Text;
                nuevo.Telefono = txtTelefono.Text;
                nuevo.Cuit = txtCuit.Text;
                nuevo.Email = txtEmail.Text;

                nuevo.Direccion = new Direccion();
                nuevo.Direccion.Calle = txtCuit.Text;
                nuevo.Direccion.Numero = int.Parse(txtNumero.Text);
                nuevo.Direccion.Departamento = int.Parse(txtDepartamento.Text);
                nuevo.Direccion.Localidad = txtLocalidad.Text;
                nuevo.Direccion.Provincia = txtProvincia.Text;
                nuevo.Direccion.CodigoPostal = txtCodigoPostal.Text;
                nuevo.Estado = false;

               // if (Request.QueryString["id"] != null)
               // {
                    //int id = int.Parse(Request.QueryString["id"]);
                    // negocio.modificar(id, nuevaMarca);
                //}
               // else
                //{
                    negocio.agregar(nuevo);
                    Response.Redirect("Proveedores.aspx", false);
               // }

            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
            }
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Articulos.aspx", false);
        }
    }
}