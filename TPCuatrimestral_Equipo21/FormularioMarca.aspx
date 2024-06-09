<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormularioMarca.aspx.cs" Inherits="TPCuatrimestral_Equipo21.FormularioMarca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="container-fluid px-4">
            <h1 class="mt-5">Agregar Nueva Marcas</h1>
            <div class="card mt-4">
                <div class="card-header">
                    <i class="fas fa-tag fa-lg" style="color: #2c78aa;"></i> Nueva Marca
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-12">
                            <!-- Campo de texto para ingresar el nombre de la marca -->
                            <label for="txtNombreMarca" class="form-label">Nombre de la marca:</label>
                            <asp:TextBox ID="txtNombreMarca" runat="server" CssClass="form-control mb-3" placeholder="Ingrese marca"></asp:TextBox>
                            
                            <!-- Botones Aceptar y Cancelar -->
                            <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="btn btn-success me-2" OnClick="btnAceptar_Click" />
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger" OnClick="btnCancelar_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

</asp:Content>
