﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;
using dominio;

namespace TPCuatrimestral_Equipo21
{
    public partial class FrmUsuario : System.Web.UI.Page
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

            try
            {
                if (!IsPostBack)
                {
                    TipoUsuarioNegocio negtipoUsu = new TipoUsuarioNegocio();
                    List<TipoUsuario> listaTipoUsuario = negtipoUsu.listar();

                    ddlTipoUsuario.DataSource = listaTipoUsuario;
                    ddlTipoUsuario.DataValueField = "TipoUsuarioId";
                    ddlTipoUsuario.DataTextField = "Descripcion";
                    ddlTipoUsuario.DataBind();

                    if (Request.QueryString["id"] != null)
                    {
                        int id = int.Parse(Request.QueryString["id"]);
                        CargarUsuario(id);
                    }
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.Message);
                Response.Redirect("Error.aspx", false);
            }
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
                    txtNombre.Text = usuario.Nombre;
                    txtApellido.Text = usuario.Apellido;
                    txtDni.Text = usuario.Dni;
                    txtEmail.Text = usuario.Email;
                    txtTelefono.Text = usuario.Telefono;

                    txtCalle.Text = usuario.Direccion.Calle;
                    txtNumero.Text = usuario.Direccion.Numero.ToString();
                    txtPiso.Text = usuario.Direccion.Piso.ToString();
                    txtDepartamento.Text = usuario.Direccion.Departamento;
                    txtProvincia.Text = usuario.Direccion.Provincia;
                    txtLocalidad.Text = usuario.Direccion.Localidad;
                    txtCodigoPostal.Text = usuario.Direccion.CodigoPostal;

                    txtNombreUsu.Text = usuario.NombreUsuario;

                    ddlTipoUsuario.SelectedValue = usuario.tipoUsuario.TipoUsuarioId.ToString();

                    //lo uso apra almacenar el id de la dire
                    hfDireccionId.Value = usuario.Direccion.Id.ToString();
                }
                catch (Exception ex)
                {
                    Session.Add("error", "Error al cargar datos del Usuario en los controles: " + ex.Message);
                    Response.Redirect("Error.aspx", false);
                }
            }
        }
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario nuevo = new Usuario();
                UsuarioNegocio negocio = new UsuarioNegocio();
                DireccionNegocio direccionNegocio = new DireccionNegocio();
                TipoUsuario tipoUsuario = new TipoUsuario();
                int idMen = 0;

                nuevo.Nombre = txtNombre.Text;
                nuevo.Apellido = txtApellido.Text;
                nuevo.Dni = txtDni.Text;
                nuevo.Email = txtEmail.Text;
                nuevo.Telefono = txtTelefono.Text;
                nuevo.EstadoUsu = false;



                Direccion nuevaD = new Direccion();
                nuevaD.Calle = txtCalle.Text;
                nuevaD.Numero = int.Parse(txtNumero.Text);
                nuevaD.Piso = string.IsNullOrEmpty(txtPiso.Text) ? null : txtPiso.Text;
                nuevaD.Departamento = string.IsNullOrEmpty(txtDepartamento.Text) ? null : txtDepartamento.Text;
                nuevaD.Provincia = txtProvincia.Text;
                nuevaD.Localidad = txtLocalidad.Text;
                nuevaD.CodigoPostal = txtCodigoPostal.Text;

                nuevo.NombreUsuario = txtNombreUsu.Text;
               

                nuevo.tipoUsuario = new TipoUsuario();
                nuevo.tipoUsuario.TipoUsuarioId = int.Parse(ddlTipoUsuario.SelectedValue);

                if (Request.QueryString["Id"] != null)
                {
                    // Mantengo la contra al modificar
                    int idUsuario = int.Parse(Request.QueryString["Id"]);
                    Usuario usuarioExistente = negocio.obtenerPorId(idUsuario);

                    if (usuarioExistente != null)
                    {
                        // Mantener la contraseña existente al modificar
                        nuevo.Contra = usuarioExistente.Contra;

                        // Modificar dirección
                        nuevaD.Id = int.Parse(hfDireccionId.Value);
                        direccionNegocio.modificar(nuevaD);

                        // Modificar usuario
                        nuevo.Id = int.Parse(Request.QueryString["Id"]);
                        negocio.modificar(nuevo);
                    }
                    else
                    {
                        throw new Exception("No se encontró el usuario para modificar.");
                    }

                    idMen = 1;
                    Response.Redirect("FrmMensaje.aspx?id=" + idMen);
                }
                else
                {
                    // Agregar nuevo Usuario
                    nuevo.Contra = txtDni.Text;
                    direccionNegocio.agregarDire(nuevaD);
                    nuevo.Direccion = nuevaD;
                    nuevo.Direccion.Id = direccionNegocio.obtenerUltimoId();
                    negocio.agregar(nuevo);
                }

                idMen = 2;
                Response.Redirect("FrmMensaje.aspx?id=" + idMen, false);
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex);
                throw;
            }

        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Usuarios.aspx", false);
        }
    }
}