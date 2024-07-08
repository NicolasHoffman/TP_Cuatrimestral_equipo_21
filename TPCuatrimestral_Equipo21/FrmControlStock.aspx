<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmControlStock.aspx.cs" Inherits="TPCuatrimestral_Equipo21.FrmControlStock" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid px-4">
        <h1 class="mt-5">Articulos</h1>
        <ol class="breadcrumb mb-4 mt-4">
            <li class="breadcrumb-item"><a href="index.html">Activo</a></li>
            <li class="breadcrumb-item active">Articulos</li>
        </ol>
        <div class="card">
            <div class="card-header">
                <i class="fas fa-tag fa-lg" style="color: #2c78aa;"></i>Lista de Articulos
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-12">
                        <asp:Button ID="Button1" runat="server" Text="Crear Nuevo" CssClass="btn btn-success" OnClick="btnCrearNuevo_Click" />
                    </div>
                </div>
                <hr />
                <table id="datatablesSimple" class="table table-striped table-bordered">
                    <thead>
                        <tr>
                            <th>Imagen</th>
                            <th>Codigo</th>
                            <th>Nombre</th>
                            <th>Stock Act</th>
                            <th>Stock Max</th>
                            <th>Stock Min</th>
                            <th>Administración</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptArticulos" runat="server" OnItemCommand="rptArticulos_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("Articulo.ImagenArt") %>' Height="50" Width="50" />
                                    </td>
                                    <td><%# Eval("Articulo.Codigo") %></td>
                                    <td><%# Eval("Articulo.Nombre") %></td>
                                    <td><%# Eval("Stock") %></td>
                                    <td><%# Eval("StockMax") %></td>
                                    <td><%# Eval("StockMin") %></td>
                                    <td>
                                        <asp:LinkButton runat="server" CssClass="btn btn-success btn-sm" CommandName="Sumar" CommandArgument='<%# Eval("Articulo.Codigo") %>'><i class="fas fa-plus"></i></asp:LinkButton>
                                        <asp:LinkButton runat="server" CssClass="btn btn-danger btn-sm ms-2" CommandName="Restar" CommandArgument='<%# Eval("Articulo.Codigo") %>'> <i class="fas fa-minus"></i></asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>
    </div>

    <script src="https://cdn.jsdelivr.net/npm/simple-datatables@7.1.2/dist/umd/simple-datatables.min.js" crossorigin="anonymous"></script>
    <script>
        window.addEventListener('DOMContentLoaded', event => {
            const dataTable = new simpleDatatables.DataTable("#datatablesSimple", {
                columns: [
                    { select: 0, sortable: false },
                    { select: 1, sortable: true },
                    { select: 2, sortable: true },
                    { select: 3, sortable: true },
                    { select: 4, sortable: true },
                    { select: 5, sortable: true },
                    { select: 6, sortable: true }
                ]
            });
        });
    </script>
</asp:Content>