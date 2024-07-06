<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormularioCliente.aspx.cs" Inherits="TPCuatrimestral_Equipo21.FormularioCliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid px-4">
        <h1 class="mt-5">Agregar Cliente</h1>
        <div class="card mt-4">
            <div class="card-header">
                <i class="fas fa-tag fa-lg" style="color: #2c78aa;"></i>Nuevo Cliente
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="mb-3">
                            <label for="txtNombre" class="form-label">Nombre:</label>
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" required placeholder="Ingrese Nombre"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <label for="txtApellido" class="form-label">Apellido:</label>
                            <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" required placeholder="Ingrese Apellido"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <label for="txtDni" class="form-label">Dni:</label>
                            <asp:TextBox ID="txtDni" runat="server" CssClass="form-control" required placeholder="Ingrese Dni"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <label for="txtEmail" class="form-label">Email:</label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" required placeholder="Ingrese Email"></asp:TextBox>
                        </div>
                          <div class="mb-3">
                            <label for="txtTelefono" class="form-label">Telefono:</label>
                            <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" required placeholder="Ingrese Telefono"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <label for="txtCalle" class="form-label">Domicilio:</label>
                            <asp:TextBox ID="txtCalle" runat="server" CssClass="form-control" required placeholder="Ingrese Nombre de la Calle"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-6">
                         <div class="mb-3">
                            <label for="txtNumero" class="form-label">Número:</label>
                            <asp:TextBox ID="txtNumero" runat="server" CssClass="form-control" required placeholder="Ingrese Número"></asp:TextBox>
                        </div>
                         <div class="mb-3">
                            <label for="txtPiso" class="form-label">Piso:</label>
                            <asp:TextBox ID="txtPiso" runat="server" CssClass="form-control" placeholder="Ingrese Piso"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <label for="txtDepartamento" class="form-label">Departamento:</label>
                            <asp:TextBox ID="txtDepartamento" runat="server" CssClass="form-control" placeholder="Ingrese Departamento"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <label for="txtLocalidad" class="form-label">Localidad:</label>
                            <asp:TextBox ID="txtLocalidad" runat="server" CssClass="form-control" required placeholder="Ingrese Localidad"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <label for="txtProvincia" class="form-label">Provincia:</label>
                            <asp:TextBox ID="txtProvincia" runat="server" CssClass="form-control"  required placeholder="Ingrese Provincia"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <label for="txtCodigoPostal" class="form-label">Codigo Postal:</label>
                            <asp:TextBox ID="txtCodigoPostal" runat="server" CssClass="form-control" required placeholder="Codigo Postal"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <hr />
                <div class="d-flex justify-content-end">
                    <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="btn btn-success me-2" OnClick="btnAceptar_Click" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger" OnClick="btnCancelar_Click" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
