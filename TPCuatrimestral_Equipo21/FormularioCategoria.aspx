<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormularioCategoria.aspx.cs" Inherits="TPCuatrimestral_Equipo21.FormularioCategoria" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <div class="container-fluid px-4">
            <h1 class="mt-5">Agregar Nueva Categoria</h1>
            <div class="card mt-4">
                <div class="card-header">
                    <i class="fas fa-tag fa-lg" style="color: #2c78aa;"></i> Nueva Categoria
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-12">
                            <!-- Campo de texto para ingresar el nombre de la marca -->
                            <label for="txtNombreCategoria" class="form-label">Nombre de la Categoria:</label>
                            <asp:TextBox ID="txtNombreCategoria" runat="server" CssClass="form-control mb-3" placeholder="Ingrese Nueva Categorias"></asp:TextBox>
                            
                            <!-- Botones Aceptar y Cancelar -->
                            <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="btn btn-success me-2" OnClick="btnAceptar_Click" />
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger" OnClick="btnCancelar_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
