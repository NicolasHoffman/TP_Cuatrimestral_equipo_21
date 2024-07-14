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
    public partial class Recuperar : System.Web.UI.Page
    {
        int idMen = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnRestablecer_Click(object sender, EventArgs e)
        {
            string codigo = txtCodigo.Text;
            string nuevaContrasena = txtNuevaContrasena.Text;
            int userId = ObtenerUserIdDesdeCodigo(codigo); // Necesitas implementar esta función

            if (userId != 0)
            {
                RecuperarContraNego recuperarContraNego = new RecuperarContraNego();
                if (recuperarContraNego.ValidarCodigoRecuperacion(userId, codigo))
                {
                    recuperarContraNego.ActualizarContrasena(userId, nuevaContrasena);

                    //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Su contraseña ha sido restablecida exitosamente.');", true);
                    idMen = 6;
                    Response.Redirect("FrmMensaje.aspx?id=" + idMen);
                }
                else
                {

                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('El código de recuperación no es válido o ha expirado.');", true);
                }
            }
        }
        private int ObtenerUserIdDesdeCodigo(string codigo)
        {
            RecuperarContraNego recuperarContraNego = new RecuperarContraNego();
            return recuperarContraNego.ObtenerUserIdDesdeCodigo(codigo);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}