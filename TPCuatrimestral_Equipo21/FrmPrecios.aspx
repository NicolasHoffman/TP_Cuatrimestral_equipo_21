<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmPrecios.aspx.cs" Inherits="TPCuatrimestral_Equipo21.FrmPrecios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

   <div class="container-fluid px-4">
        <h1 class="mt-5">Pedidos</h1>
        <ol class="breadcrumb mb-4 mt-4">
            <li class="breadcrumb-item"><a href="index.html">Activo</a></li>
            <li class="breadcrumb-item active">Pedidos</li>
        </ol>
        <div class="card">
            <div class="card-header">
                <i class="fa-solid fa-truck-fast" style="color: #2c78aa;"></i> Actualizar Precio
            </div>
            <div class="card-body">
                <div class="mb-3">
                    <label for="txtPorcentajeAumento" class="form-label">Porcentaje de Aumento:</label>
                    <asp:TextBox ID="txtPorcentajeAumento" runat="server" CssClass="form-control" placeholder="Ingrese el % de aumento"></asp:TextBox>
                </div>
                <hr />
                <div class="row mb-3">
                    <label class="form-label">Aumentar por:</label>
                    <asp:RadioButtonList ID="rblAumentarPor" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rblAumentarPor_SelectedIndexChanged" CssClass="form-check">
                        <asp:ListItem Text="Por Artículo" Value="Articulo"></asp:ListItem>
                        <asp:ListItem Text="Por Categoría" Value="Categoria"></asp:ListItem>
                        <asp:ListItem Text="Por Marca" Value="Marca"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <!-- Panel por arti -->
               <asp:Panel ID="pnlPorArticulo" runat="server" Visible="false">
                    <div class="mb-4">
                        <label for="txtCodigoProducto" class="form-label">Buscar Producto por Código</label>
                        <asp:TextBox ID="txtCodigoProducto" runat="server" CssClass="form-control" placeholder="Ingrese Código" AutoPostBack="true" OnTextChanged="txtCodigoProducto_TextChanged"></asp:TextBox>
                    </div>
                    <hr />
                    <table id="datatablesSimple" class="table table-striped table-bordered">
                        <thead>
                            <tr>
                                <th>Imagen</th>
                                <th>Código</th>
                                <th>Nombre</th>
                                <th>Descripción</th>
                                <th>Marca</th>
                                <th>Categoría</th>
                                <th>Precio</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="rptArticulos" runat="server" OnItemCommand="rptArticulos_ItemCommand">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("ImagenArt") %>' Height="50" Width="50" />
                                        </td>
                                        <td><%# Eval("Id") %></td>
                                        <td><%# Eval("Nombre") %></td>
                                        <td><%# Eval("Descripcion") %></td>
                                        <td><%# Eval("Marca") %></td>
                                        <td><%# Eval("Categoria") %></td>
                                        <td><%# Eval("Precio") %></td>

                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </asp:Panel>
                <!-- Panel por cate -->
                <asp:Panel ID="pnlPorCategoria" runat="server" Visible="false">
                    <div class="mb-3">
                        <label for="ddlCategoria" class="form-label">Categoría</label>
                        <asp:DropDownList ID="ddlCategoria" CssClass="form-select" runat="server"></asp:DropDownList>
                    </div>
                </asp:Panel>
                <!-- Panel por marca -->
                <asp:Panel ID="pnlPorMarca" runat="server" Visible="false">
                    <div class="mb-3">
                        <label for="ddlMarca" class="form-label">Marca</label>
                        <asp:DropDownList ID="ddlMarca" CssClass="form-select" runat="server"></asp:DropDownList>
                    </div>
                </asp:Panel>
                <hr />
                <div class="d-flex justify-content-end">
                    <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="btn btn-success me-2" OnClick="btnAceptar_Click" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger" OnClick="btnCancelar_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
