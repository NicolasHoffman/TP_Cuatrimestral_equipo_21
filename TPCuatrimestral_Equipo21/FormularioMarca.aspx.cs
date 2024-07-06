using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using dominio;
using negocio;

namespace TPCuatrimestral_Equipo21
{
    public partial class FormularioMarca : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string id = Request.QueryString["id"];

            if (!string.IsNullOrEmpty(id) && !IsPostBack)
            {
                try
                {
                    MarcaNegocio negocio = new MarcaNegocio();
                    Marca marca = negocio.obtenerMarcaPorId(id);

                    if (marca != null)
                    {
                        // Pre cargar el nombre de la marca en el TextBox
                        txtNombreMarca.Text = marca.Nombre;
                    }
               
                }
                catch (Exception ex)
                {
                    Session.Add("Error", ex);
                    throw;
                }
            }

        }
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            bool success = false;
            string mensajeError = "";
            try
            {
                string nombreMarca = txtNombreMarca.Text.Trim();

                if (Validaciones.NoPuedeEstarVacia(nombreMarca))
                {
                    if (Validaciones.SoloLetras(nombreMarca))
                    {
                        MarcaNegocio negocio = new MarcaNegocio();

                        // Convertir la primera letra en mayúscula
                        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                        string nuevaMarca = textInfo.ToTitleCase(nombreMarca.ToLower());

                        // Verificar si la marca ya existe
                        if (negocio.existencia(nuevaMarca))
                        {
                            mensajeError = "La Marca ingresada ya existe.";
                        }
                        else
                        {
                            if (Request.QueryString["id"] != null)
                            {
                                int id = int.Parse(Request.QueryString["id"]);
                                negocio.modificar(id, nuevaMarca);
                            }
                            else
                            {
                                negocio.agregar(nuevaMarca);
                            }
                            success = true;
                        }
                    }
                    else
                    {
                        mensajeError = "El campo 'Nombre de la marca' solo puede contener letras.";
                    }
                }
                else
                {
                    mensajeError = "El campo 'Nombre de la marca' es obligatorio.";
                }
            }
            catch (Exception ex)
            {
                success = false;
                Session.Add("error", ex);
                throw;
            }
            finally
            {
                if (!success && !string.IsNullOrEmpty(mensajeError))
                {
                    lblMensajeError.Text = mensajeError;
                    pnlMensajes.Visible = true;
                }
                else
                {
                    pnlMensajes.Visible = false;
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", $"mostrarModal({success.ToString().ToLower()});", true);
            }
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Marcas.aspx", false);
        }
    }
}