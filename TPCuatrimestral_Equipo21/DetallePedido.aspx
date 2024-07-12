<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetallePedido.aspx.cs" Inherits="TPCuatrimestral_Equipo21.DetallePedido" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid px-4">
        <h1 class="mt-5">Pedido</h1>
        <ol class="breadcrumb mb-4 mt-4">
            <li class="breadcrumb-item"><a href="index.html">Activo</a></li>
            <li class="breadcrumb-item active">Pedido</li>
        </ol>
        <div class="card">
            <div class="card-header">
                <i class="fas fa-tag fa-lg" style="color: #2c78aa;"></i>Lista de Articulos del Pedido
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
                            <th>ID</th>
                            <th>Articulo</th>
                            <th>Cantidad</th>
                        </tr>
                    </thead>
                    <tfoot>
                        <tr>
                            <th>ID</th>
                            <th>Articulo</th>
                            <th>Cantidad</th>
                            
                        </tr>
                    </tfoot>
                    <tbody>
                         <asp:Repeater ID="rptDetallePedido" runat="server" OnItemCommand="rptDetallePedido_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("IdArticulo") %></td>
                                    <td><%# Eval("NombreArticulo") %></td>
                                    <td><%# Eval("Cantidad") %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
                <asp:Button ID="btnPrepararPedido" runat="server" Text="Preparar" CssClass="btn btn-success" OnClick="btnPrepararPedido_Click"  />
                <asp:Button ID="btnPedidoPreparado" runat="server" Text="Preparado" CssClass="btn btn-success" OnClick="btnPedidoPreparado_Click"  />
                <asp:Button ID="btnEntregarPedido" runat="server" Text="Entregar" CssClass="btn btn-success" OnClick="btnEntregarPedido_Click"  />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger" OnClick="btnCancelar_Click"/>
            </div>
        </div>
    </div>


    <script src="https://cdn.jsdelivr.net/npm/simple-datatables@7.1.2/dist/umd/simple-datatables.min.js" crossorigin="anonymous"></script>
    <script>
        window.addEventListener('DOMContentLoaded', event => {
            const dataTable = new simpleDatatables.DataTable("#datatablesSimple", {
                columns: [
                    { select: 0, sortable: false },
                    { select: 1, sortable: false },
                    { select: 2, sortable: false }
                ]
            });
        });
    </script>
</asp:Content>
