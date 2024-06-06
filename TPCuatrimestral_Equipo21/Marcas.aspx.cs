using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace TPCuatrimestral_Equipo21
{
    public partial class Marcas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindMarcasGrid();
            }
        }
        private void BindMarcasGrid()
        {
            MarcaNegocio negocioMarca = new MarcaNegocio();
            int cantidadRegistros = gvResultados.PageSize; // Obtener la cantidad de registros por página
            List<Marca> marcas = negocioMarca.listar(cantidadRegistros);

            gvResultados.DataSource = marcas;
            gvResultados.DataBind();
        }

        protected void ddlCantidadRegistros_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvResultados.PageSize = Convert.ToInt32(ddlCantidadRegistros.SelectedValue);
            BindMarcasGrid();
        }

        protected void gvResultados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvResultados.PageIndex = e.NewPageIndex;
            BindMarcasGrid();
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string nombreMarca = txtBuscar.Text.Trim();
            MarcaNegocio negocioMarca = new MarcaNegocio();
            List<Marca> marcas = negocioMarca.BuscarPorNombre(nombreMarca);

            gvResultados.DataSource = marcas;
            gvResultados.DataBind();
        }


        protected void gvResultados_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Obtener el ID de la fila que se está eliminando
            int idMarca = Convert.ToInt32(gvResultados.DataKeys[e.RowIndex].Value);

            // Llamar al método para eliminar la marca con el ID especificado
            EliminarMarca(idMarca);

            // Volver a cargar las marcas en el GridView después de la eliminación
            BindMarcasGrid();
        }

        private void EliminarMarca(int idMarca)
        {
            MarcaNegocio negocio = new MarcaNegocio();
            negocio.eliminar(idMarca);
        }

    }
}