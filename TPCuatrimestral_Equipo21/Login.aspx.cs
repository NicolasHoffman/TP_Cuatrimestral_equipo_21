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
        protected void btnIngresar_Click(object sender, EventArgs e)
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
                    Session.Add("Error", "usuario o contraseña incorrectos");
                }
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
            }
        }
        protected void btnRecuperar_Click(object sender, EventArgs e)
        {
            string email = txtEmailRecuperacion.Text;
            if (!string.IsNullOrEmpty(email))
            {
                RecuperarContraNego recuperarContraNego = new RecuperarContraNego();
                int userId = recuperarContraNego.ObtenerUserIdPorEmail(email);
                if (userId != 0)
                {
                    string codigoRecuperacion = recuperarContraNego.GenerarCodigoRecuperacion(userId);
                    string correoCliente = email;
                    EmailService emailService = new EmailService();
                    emailService.armarCorreo(email, "Recuperación de contraseña", $"Su código de recuperación es: {codigoRecuperacion}");
                    try
                    {
                        emailService.enviarEmail();
                    }
                    catch (Exception ex)
                    {
                        Session.Add("error", ex);
                    }

                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Se ha enviado un código de recuperación a su correo electrónico.');", true);
                    Response.Redirect("Recuperar.aspx");
                }
                else
                {
                    // error si no encuentro mail 
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No se encontró una cuenta con ese correo electrónico.');", true);
                }
            }
        }
    }
}