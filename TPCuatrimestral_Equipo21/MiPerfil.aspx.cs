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
    public partial class MiPerfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["usuario"] == null)
                {
                    Session.Add("Error", "Debes loguearte");
                    Response.Redirect("Login.aspx", false);
                }
                if (!IsPostBack)
                {
                    Usuario usuario = (Usuario)Session["usuario"];
                    int userId = usuario.Id; // cambiar ahora asumo 1 asi veo
                    CargarUsuario(userId);
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnCambiarContra_Click(object sender, EventArgs e)
        {
            Response.Redirect("Usuarios.aspx", false);
        }
        private void CargarUsuario(int id)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            Usuario usuario = null;
            try
            {
                usuario = negocio.obtenerPorId(id);
            }
            catch (Exception ex)
            {
                Session.Add("error", "Error al obtener Usuario: " + ex.Message);
                Response.Redirect("Error.aspx", false);
            }

            if (usuario != null)
            {
                try
                {
                    lblNombre.Text = usuario.Nombre;
                    lblApellido.Text = usuario.Apellido;
                    lblDni.Text = usuario.Dni;
                    lblEmail.Text = usuario.Email;
                    lblTelefono.Text = usuario.Telefono;

                    lblCalle.Text = usuario.Direccion.Calle;
                    lblNumero.Text = usuario.Direccion.Numero.ToString();
                    lblPiso.Text = usuario.Direccion.Piso.ToString();
                    lblDepartamento.Text = usuario.Direccion.Departamento;
                    lblProvincia.Text = usuario.Direccion.Provincia;
                    lblLocalidad.Text = usuario.Direccion.Localidad;
                    lblCodigoPostal.Text = usuario.Direccion.CodigoPostal;

                    lblNombreUsu.Text = usuario.NombreUsuario;

                    lblTipoUsuario.Text = usuario.tipoUsuario.Descripcion;
                }
                catch (Exception ex)
                {
                    Session.Add("error", "Error al cargar datos del Usuario en los controles: " + ex.Message);
                    Response.Redirect("Error.aspx", false);
                }
            }
        }



    }
}