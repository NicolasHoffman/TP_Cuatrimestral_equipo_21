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
    public partial class FrmMensaje : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int mensajeId;
                if (int.TryParse(Request.QueryString["id"], out mensajeId))
                {
                    MensajeNegocio mensajeNegocio = new MensajeNegocio();
                    Mensaje mensaje = mensajeNegocio.obtenerMensajePorId(mensajeId);

                    if (mensaje != null)
                    {
                        // Determinar si es un mensaje de éxito o error
                        bool isSuccess = mensaje.Tipo == "exito";

                        // Llamada a la función de JavaScript para mostrar el mensaje
                        string script = $"showMessage({isSuccess.ToString().ToLower()}, '{mensaje.Titulo}', '{mensaje.Cuerpo}', '{mensaje.RedireccionUrl}');";
                        ClientScript.RegisterStartupScript(this.GetType(), "showMessageScript", script, true);
                    }
                }
            }
        }
    }
}