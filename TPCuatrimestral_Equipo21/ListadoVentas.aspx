<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListadoVentas.aspx.cs" Inherits="TPCuatrimestral_Equipo21.ListadoVentas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class="container-fluid px-4">
    <h1 class="mt-5">Listado de Ventas</h1>
    <div class="card mt-4">
        <div class="card-body">
            <div class="row">
                <div class="col-md-4">
                    <div class="mb-4">
                        <label for="txtNombreCliente" class="form-label">Buscar Venta Por Dni Cliente</label>
                        <asp:TextBox ID="txtNombreCliente" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtNombreCliente_TextChanged" placeholder="Ingrese Dni"></asp:TextBox>
                    </div>
                </div>
            </div>
            <hr />
            <table id="datatablesSimple" class="table table-striped table-bordered">
                <thead>
                    <tr>
                        <th>Numero Venta</th>
                        <th>Vendedor</th>
                        <th>Cliente</th>
                        <th>Importe Total</th>
                        <th style="width: 80px;">ACCIÓN</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptVentaD" runat="server" OnItemCommand="rptVentaD_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("Id") %></td>
                                <td><%# Eval("NombreVendedor") %></td>
                                <td><%# Eval("NombreCliente") %></td>
                                <td><%# Eval("ImporteTotal", "{0:C}") %></td>
                                <td>
                                    <asp:LinkButton runat="server" CssClass="btn btn-primary btn-sm" CommandName="Seleccionar" CommandArgument='<%# Eval("Id") %>'><i class="fa-solid fa-rectangle-list"></i></asp:LinkButton>
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
                { select: 0, sortable: true },
                { select: 1, sortable: true },
                { select: 2, sortable: true },
                { select: 3, sortable: true },
                { select: 4, sortable: false}
            ]
        });
    });
</script>
</asp:Content>
