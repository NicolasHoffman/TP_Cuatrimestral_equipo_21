using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;

namespace TPCuatrimestral_Equipo21
{
    public partial class FrmControlStock : System.Web.UI.Page
    {
        private readonly ControlStockNegocio controlStockNegocio = new ControlStockNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //Cargar datos 
                CargarDatos();
            }
        }
        private void CargarDatos()
        {
            rptArticulos.DataSource = controlStockNegocio.listar();
            rptArticulos.DataBind();
        }

        protected void rptArticulos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Sumar")
            {
                try
                {
                    string IdArticuloStr = e.CommandArgument.ToString();
                    int IdArticulo = int.Parse(IdArticuloStr);

                    TextBox txtCantidadRestar = (TextBox)e.Item.FindControl("txtCantidad");
                    int cantidad = int.Parse(txtCantidadRestar.Text);

                    ControlStockNegocio negocio = new ControlStockNegocio();

                    int stockActual = negocio.obtStock(IdArticulo);

                        negocio.sumarStock(cantidad, IdArticulo);
                        CargarDatos();
                }
                catch (Exception ex)
                {
                    Session.Add("Error", ex);
                    throw;
                }


            }
            else if (e.CommandName == "Restar")
            {
                try
                {
                    string IdArticuloStr = e.CommandArgument.ToString();
                    int IdArticulo = int.Parse(IdArticuloStr);

                    TextBox txtCantidadRestar = (TextBox)e.Item.FindControl("txtCantidad");
                    int cantidad = int.Parse(txtCantidadRestar.Text);

                    ControlStockNegocio negocio = new ControlStockNegocio();

                    int stockActual = negocio.obtStock(IdArticulo);

                    if(stockActual >= cantidad)
                    {
                        negocio.descontarStock(cantidad, IdArticulo);
                        CargarDatos();
                    }
                    else
                    {
                        string mensaje = "No se puede descontar el stock. Stock actual: " + stockActual;
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + mensaje + "');", true);
                    }

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