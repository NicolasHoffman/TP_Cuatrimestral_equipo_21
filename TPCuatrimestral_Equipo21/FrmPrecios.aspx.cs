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
    public partial class FrmPrecios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Validaciones.HayUsuarioEnSesion(Session))
            {
                Response.Redirect("FrmMensaje.aspx?id=13", false);
            }
            if (!Validaciones.EsUsuarioAdministradorOVendedor(Session))
            {
                Response.Redirect("FrmMensaje.aspx?id=12", false);
            }

            if (!IsPostBack)
            {
                MarcaNegocio negmarca = new MarcaNegocio();

                List<Marca> listaMarcas = negmarca.listar();
                ddlMarca.DataSource = listaMarcas;
                ddlMarca.DataValueField = "Id";
                ddlMarca.DataTextField = "Nombre";
                ddlMarca.DataBind();

                CategoriNegocio negcat = new CategoriNegocio();

                List<Categori> listaCategorias = negcat.listar();
                ddlCategoria.DataSource = listaCategorias;
                ddlCategoria.DataValueField = "Id";
                ddlCategoria.DataTextField = "Descripcion";
                ddlCategoria.DataBind();

            }
        }

        protected void rblAumentarPor_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Ocu los paneles inicialmente
            pnlPorArticulo.Visible = false;
            pnlPorCategoria.Visible = false;
            pnlPorMarca.Visible = false;

            //muestro el panel segun l oque se seleccione
            switch (rblAumentarPor.SelectedValue)
            {
                case "Articulo":
                    pnlPorArticulo.Visible = true;
                    break;
                case "Categoria":
                    pnlPorCategoria.Visible = true;
                    break;
                case "Marca":
                    pnlPorMarca.Visible = true;
                    break;
            }
        }

        protected void txtCodigoProducto_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCodigoProducto.Text.Trim()))
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                Articulo art = negocio.obtenerPorCodigo(txtCodigoProducto.Text.Trim());

                List<Articulo> listaArticulos = new List<Articulo> { art };
                rptArticulos.DataSource = listaArticulos;
                rptArticulos.DataBind();
            }
        }
     
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            decimal porcentaje = 0;
            //valido por

            if (!decimal.TryParse(txtPorcentajeAumento.Text.Trim(), out porcentaje))
            {
                //lblMensaje.Text = "Ingrese un porcentaje válido.";
                //lblMensaje.Visible = true;
                Response.Redirect("FrmMensaje.aspx?id=" + 11);
            }
            string criterio = string.Empty;
            string valor = string.Empty;

            switch (rblAumentarPor.SelectedValue)
            {
                case "Articulo":
                    criterio = "codigo";
                    valor = txtCodigoProducto.Text.Trim();
                    break;
                case "Categoria":
                    criterio = "categoria";
                    valor = ddlCategoria.SelectedValue;
                    break;
                case "Marca":
                    criterio = "marca";
                    valor = ddlMarca.SelectedValue;
                    break;
                default:
                    
                    //lblMensaje.Text = "Seleccione un criterio válido.";
                    //lblMensaje.Visible = true;
                    return;
            }

            negocio.actualizarValor(criterio, valor, porcentaje);
            Response.Redirect("FrmMensaje.aspx?id=" + 10);

        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {

        }
        protected void rptArticulos_ItemCommand(object sender, EventArgs e)
        {

        }

    }
}