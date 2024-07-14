<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MiPerfil.aspx.cs" Inherits="TPCuatrimestral_Equipo21.MiPerfil" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="container-fluid px-4">
        <h1 class="mt-5">Perfil</h1>
        <div class="card mt-4">
            <div class="card-header">
                <i class="fas fa-user-plus" style="color: #2c78aa;"></i> Mi Perfil
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="mb-3">
                            <label for="lblNombre" class="form-label">Nombre:</label>
                            <asp:Label ID="lblNombre" runat="server" CssClass="form-control"></asp:Label>
                        </div>
                        <div class="mb-3">
                            <label for="lblApellido" class="form-label">Apellido:</label>
                            <asp:Label ID="lblApellido" runat="server" CssClass="form-control"></asp:Label>
                        </div>
                        <div class="mb-3">
                            <label for="lblDni" class="form-label">Dni:</label>
                            <asp:Label ID="lblDni" runat="server" CssClass="form-control"></asp:Label>
                        </div>
                        <div class="mb-3">
                            <label for="lblNombreUsu" class="form-label">Nombre Usuario:</label>
                            <asp:Label ID="lblNombreUsu" runat="server" CssClass="form-control"></asp:Label>
                        </div>
                        <div class="mb-3">
                            <label for="lblTipoUsuario" class="form-label">Tipo Usuario</label>
                            <asp:Label ID="lblTipoUsuario" runat="server" CssClass="form-control"></asp:Label>
                        </div>
                        <div class="mb-3">
                            <label for="lblEmail" class="form-label">Email:</label>
                            <asp:Label ID="lblEmail" runat="server" CssClass="form-control"></asp:Label>
                        </div>
                        <div class="mb-3">
                            <label for="lblTelefono" class="form-label">Telefono:</label>
                            <asp:Label ID="lblTelefono" runat="server" CssClass="form-control"></asp:Label>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="mb-3">
                            <label for="lblCalle" class="form-label">Domicilio:</label>
                            <asp:Label ID="lblCalle" runat="server" CssClass="form-control"></asp:Label>
                        </div>
                        <div class="mb-3">
                            <label for="lblNumero" class="form-label">Número:</label>
                            <asp:Label ID="lblNumero" runat="server" CssClass="form-control"></asp:Label>
                        </div>
                        <div class="mb-3">
                            <label for="lblPiso" class="form-label">Piso:</label>
                            <asp:Label ID="lblPiso" runat="server" CssClass="form-control"></asp:Label>
                        </div>
                        <div class="mb-3">
                            <label for="lblDepartamento" class="form-label">Departamento:</label>
                            <asp:Label ID="lblDepartamento" runat="server" CssClass="form-control"></asp:Label>
                        </div>
                        <div class="mb-3">
                            <label for="lblLocalidad" class="form-label">Localidad:</label>
                            <asp:Label ID="lblLocalidad" runat="server" CssClass="form-control"></asp:Label>
                        </div>
                        <div class="mb-3">
                            <label for="lblProvincia" class="form-label">Provincia:</label>
                            <asp:Label ID="lblProvincia" runat="server" CssClass="form-control"></asp:Label>
                        </div>
                        <div class="mb-3">
                            <label for="lblCodigoPostal" class="form-label">Codigo Postal:</label>
                            <asp:Label ID="lblCodigoPostal" runat="server" CssClass="form-control"></asp:Label>
                        </div>
                    </div>
                </div>
                <hr />
                <div class="d-flex justify-content-end">
                    <asp:Button ID="btnCambiarContra" runat="server" Text="Cambiar Contraseña" CssClass="btn btn-success me-2" OnClick="btnCambiarContra_Click" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
