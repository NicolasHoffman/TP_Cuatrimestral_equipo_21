<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pedidos.aspx.cs" Inherits="TPCuatrimestral_Equipo21.Pedidos" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid px-4">
        <h1 class="mt-5">Pedidos</h1>
        <ol class="breadcrumb mb-4 mt-4">
            <li class="breadcrumb-item"><a href="index.html">Activo</a></li>
            <li class="breadcrumb-item active">Pedidos</li>
        </ol>
        <div class="card">
            <div class="card-header">
                <i class="fa-solid fa-truck-fast" style="color: #2c78aa;"></i> Lista de Pedidos
            </div>
            <div class="card-body">
                <div class="row mb-3">
                    <label class="form-label">Buscar por:</label>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="mb-3">
                            <label for="ddlPersonaAsignada" class="form-label">Persona Asignada</label>
                            <asp:DropDownList ID="ddlPersonaAsignada" CssClass="form-select" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPersonaAsignada_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="mb-3">
                            <label for="ddlEstadoPedido" class="form-label">Estado Pedido</label>
                            <asp:DropDownList ID="ddlEstadoPedido" CssClass="form-select" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEstadoPedido_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <hr />
                <table id="datatablesSimple" class="table table-striped table-bordered">
                    <thead>
                        <tr>
                            <th>PEDIDO</th>
                            <th>PERSONAL ASIGNADO</th>
                            <th>FECHA PEDIDO</th>
                            <th>ESTADO PEDIDO</th>
                            <th style="width: 80px;">Gestión</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptPedidos" runat="server" OnItemCommand="rptPedidos_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("Id") %></td>
                                    <td><%# Eval("NombreUsuario") %></td>
                                    <td><%# Convert.ToDateTime(Eval("FechaPedido")).ToShortDateString() %></td>
                                    <td><%# Eval("EstadoPedido.Descripcion") %></td>
                                    <td>
                                        <asp:LinkButton runat="server" CssClass="btn btn-primary btn-sm" CommandName="Seleccionar" CommandArgument='<%# Eval("Venta.Id") %>'><i class="fas fa-pen"></i></asp:LinkButton>
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
                    { select: 4, sortable: false }
                ]
            });
        });
    </script>
</asp:Content>
