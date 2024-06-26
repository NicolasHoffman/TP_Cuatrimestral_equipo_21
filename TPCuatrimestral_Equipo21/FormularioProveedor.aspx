<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormularioProveedor.aspx.cs" Inherits="TPCuatrimestral_Equipo21.FormularioProveedor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid px-4">
        <h1 class="mt-5">Agregar Proveedor</h1>
        <div class="card mt-4">
            <div class="card-header">
                <i class="fas fa-tag fa-lg" style="color: #2c78aa;"></i>Nuevo Proveedor
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="mb-3">
                            <label for="txtNombreProveedor" class="form-label">Nombre Proveedor:</label>
                            <asp:TextBox ID="txtNombreProveedor" runat="server" CssClass="form-control" placeholder="Ingrese Nombre"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <label for="txtTelefono" class="form-label">Telefono:</label>
                            <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" placeholder="Ingrese Telefono"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <label for="txtEmail" class="form-label">Email:</label>
                            <asp:TextBox ID="TextEmail" runat="server" CssClass="form-control" placeholder="Ingrese Email"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <label for="txtCuit" class="form-label">Cuit</label>
                            <asp:TextBox ID="txtCuit" runat="server" CssClass="form-control" placeholder="Ingrese Cuit sin guiones"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="mb-3">
                            <label for="txtCalle" class="form-label">Nombre de Calle:</label>
                            <asp:TextBox ID="TextCalle" runat="server" CssClass="form-control" placeholder="Ingrese Nombre de la Calle"></asp:TextBox>
                        </div>
                         <div class="mb-3">
                            <label for="txtNumero" class="form-label">Número:</label>
                            <asp:TextBox ID="TextNumero" runat="server" CssClass="form-control" placeholder="Ingrese Número"></asp:TextBox>
                        </div>
                         <div class="mb-3">
                            <label for="txtPiso" class="form-label">Piso:</label>
                            <asp:TextBox ID="TextPiso" runat="server" CssClass="form-control" placeholder="Ingrese Piso"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <label for="txtProvincia" class="form-label">Provincia:</label>
                            <asp:TextBox ID="TextLocalidad" runat="server" CssClass="form-control" placeholder="Ingrese Localidad"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <label for="txtCodigoPostal" class="form-label">Codigo Postal:</label>
                            <asp:TextBox ID="TextCodigoPostal" runat="server" CssClass="form-control" placeholder="Codigo Postal"></asp:TextBox>
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
