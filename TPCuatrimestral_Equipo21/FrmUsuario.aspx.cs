using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;
using dominio;

namespace TPCuatrimestral_Equipo21
{
    public partial class FrmUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TipoUsuarioNegocio negtipoUsu = new TipoUsuarioNegocio();

                List<TipoUsuario> listaTipoUsuario = negtipoUsu.listar();

                ddlTipoUsuario.DataSource = listaTipoUsuario;
                ddlTipoUsuario.DataValueField = "TipoUsuarioId";
                ddlTipoUsuario.DataTextField = "Descripcion";
                ddlTipoUsuario.DataBind();
            }
        }
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            Usuario nuevo = new Usuario();
            UsuarioNegocio negocio = new UsuarioNegocio();
            DireccionNegocio direccionNegocio = new DireccionNegocio();
            TipoUsuario tipoUsuario = new TipoUsuario();
            
            nuevo.Nombre = txtNombre.Text;
            nuevo.Apellido = txtApellido.Text;
            nuevo.Dni = txtDni.Text;
            nuevo.Email = txtEmail.Text;
            nuevo.Telefono = txtTelefono.Text;
            nuevo.EstadoUsu = false;

            Direccion nuevaDireccion = new Direccion();
            nuevaDireccion.Calle = txtCalle.Text;
            nuevaDireccion.Numero = int.Parse(txtNumero.Text);
            nuevaDireccion.Piso = int.Parse(txtPiso.Text);
            nuevaDireccion.Departamento = txtDepartamento.Text;
            nuevaDireccion.Provincia = txtProvincia.Text;
            nuevaDireccion.Localidad = txtLocalidad.Text;
            nuevaDireccion.CodigoPostal = txtCodigoPostal.Text;

            nuevo.NombreUsuario = txtNombreUsu.Text;

            nuevo.Contra = txtDni.Text;
            nuevo.tipoUsuario = new TipoUsuario();
            nuevo.tipoUsuario.TipoUsuarioId = int.Parse(ddlTipoUsuario.SelectedValue);

            if (Request.QueryString["dni"] != null)
            {

            }
            else
            {
                // Agregar nuevo Usuario
                direccionNegocio.agregarDire(nuevaDireccion);
                nuevo.Direccion = nuevaDireccion;
                nuevo.Direccion.Id = direccionNegocio.obtenerUltimoId();
                negocio.agregar(nuevo);
            }

            Response.Redirect("Usuarios.aspx", false);




        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Usuarios.aspx", false);
        }

     
    }
}