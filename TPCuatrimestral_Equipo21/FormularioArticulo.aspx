<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormularioArticulo.aspx.cs" Inherits="TPCuatrimestral_Equipo21.FormularioArticulo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid px-4">
        <h1 class="mt-5">Agregar Articulo</h1>
        <div class="card mt-4">
            <div class="card-header">
                <i class="fas fa-tag fa-lg" style="color: #2c78aa;"></i>Nuevo Articulo
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="mb-3">
                            <label for="txtCodigoArticulo" class="form-label">Codigo Articulo:</label>
                            <asp:TextBox ID="txtCodigoArticulo" runat="server" CssClass="form-control" placeholder="Ingrese Codigo"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <label for="txtNombreArticulo" class="form-label">Nombre del Articulo:</label>
                            <asp:TextBox ID="txtNombreArticulo" runat="server" CssClass="form-control" placeholder="Ingrese Nombre del Articulo"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <label for="ddlMarca" class="form-label">Marca</label>
                            <asp:DropDownList ID="ddlMarca" CssClass="form-select" runat="server"></asp:DropDownList>
                        </div>
                        <div class="mb-3">
                            <label for="ddlCategoria" class="form-label">Categoria</label>
                            <asp:DropDownList ID="ddlCategoria" CssClass="form-select" runat="server"></asp:DropDownList>
                        </div>
                        <div class="mb-3">
                            <label for="txtPrecio" class="form-label">Precio</label>
                            <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" placeholder="Ingrese Precio"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="mb-3">
                            <label for="txtDescripcion" class="form-label">Descripción</label>
                            <asp:TextBox runat="server" TextMode="MultiLine" ID="txtDescripcion" CssClass="form-control" />
                        </div>
                        <div class="mb-3">
                            <label for="txtImagen" class="form-label">Imagen Articulo</label>
                            <input type="file" id="txtImagen" runat="server" class="form-control" />
                        </div>
                        <div class="mb-3">
                            <asp:Image ID="imgArticuloNuevo" ImageUrl="https://www.italfren.com.ar/images/catalogo/imagen-no-disponible.jpeg" runat="server" CssClass="img-fluid small-image" />
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
