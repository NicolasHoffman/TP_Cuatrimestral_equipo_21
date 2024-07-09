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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnIngresar_click(object sender, EventArgs e)
        {
          
               Usuario usuario = new Usuario();
            UsuarioNegocio negocio = new UsuarioNegocio();
            
            try
            {

                usuario.NombreUsuario = (txtUsuario.Text);
                usuario.Contra = (txtPassword.Text);

                if (negocio.Loguear(usuario))
                {
                    Session.Add("usuario", usuario);
                    Response.Redirect("Ventas.aspx", false);
                }
                else
                {
                    Session.Add("Error", "user o apss incorrectos");

                }

     
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
            }

        }

    }
}