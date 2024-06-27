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
    public partial class Proveedor : System.Web.UI.Page
    {
        private readonly ProveedorNegocio proveedorNegocio = new ProveedorNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
          
                CargarDatos();
            }
        }
        private void CargarDatos()
        {
            rptProveedor.DataSource = proveedorNegocio.listar();
            rptProveedor.DataBind();
        }

        protected void btnCrearNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("FormularioArticulo.aspx");
        }
        protected void rptProveedor_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar")
            {
                //string id = e.CommandArgument.ToString();
                //Response.Redirect("FormularioArticulo.aspx?id=" + id);
            }
            else if (e.CommandName == "Eliminar")
            {
                try
                {
                    string idProveedor = e.CommandArgument.ToString(); // Obtener el ID de la marca del comando

                    ProveedorNegocio negocio = new ProveedorNegocio();
                    negocio.eliminar(int.Parse(idProveedor));

                    // Recargar los datos después de la eliminación
                    CargarDatos();
                }
                catch (Exception ex)
                {
                    Session.Add("Error", ex);
                    throw;
                }
            }
        }


    }
}