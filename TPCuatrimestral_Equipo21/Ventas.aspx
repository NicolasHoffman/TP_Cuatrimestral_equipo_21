<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ventas.aspx.cs" Inherits="TPCuatrimestral_Equipo21.Ventas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   <div class="container-fluid px-4">
        <h1 class="mt-5">Nueva Venta</h1>
        <div class="card mt-4">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-4">
                        <div class="mb-4">
                            <label for="txtNombreCliente" class="form-label">Datos Cliente</label>
                            <asp:TextBox ID="txtNombreCliente" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtNombreCliente_TextChanged" placeholder="Ingrese Dni"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="mb-4">
                            <label for="txtCliente" class="form-label">Cliente:</label>
                            <asp:TextBox ID="txtCliente" runat="server" CssClass="form-control" ReadOnly="true" Text=""></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">
                        <hr />
                        <div class="col-md-4">
                            <div class="mb-4">
                                <label for="txtCodigoProducto" class="form-label">Datos Producto</label>
                                <asp:TextBox ID="txtCodigoProducto" runat="server" CssClass="form-control" placeholder="Ingrese Codigo" AutoPostBack="true" OnTextChanged="txtCodigoProducto_TextChanged"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="mb-4">
                                <label for="txtNombreproducto" class="form-label">Datos Producto</label>
                                <asp:TextBox ID="txtNombreproducto" runat="server" CssClass="form-control" placeholder="Ingrese Codigo"></asp:TextBox>
                            </div>
                        </div>

                        <hr />
                        <table id="datatablesSimple" class="table table-striped table-bordered">
                            <thead>
                                <tr>
                                    <th>Codigo</th>
                                    <th>Nombre</th>
                                    <th>Descripción</th>
                                    <th>Marca</th>
                                    <th>Categoria</th>
                                    <th>Precio</th>
                                    <th>Estado</th>
                                    <th>Acción</th>
                                </tr>
                            </thead>
                            <tfoot>
                                <tr>
                                    <th>Codigo</th>
                                    <th>Nombre</th>
                                    <th>Descripcion</th>
                                    <th>Marca</th>
                                    <th>Categoria</th>
                                    <th>Precio</th>
                                    <th>Estado</th>
                                    <th>Acción</th>
                                </tr>
                            </tfoot>
                            <tbody>
                                <asp:Repeater ID="rptVentas" runat="server" OnItemCommand="rptVentas_ItemCommand">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("Id") %></td>
                                            <td><%# Eval("Nombre") %></td>
                                            <td><%# Eval("Descripcion") %></td>
                                            <td><%# Eval("Marca") %></td>
                                            <td><%# Eval("Categoria") %></td>
                                            <td><%# Eval("Precio") %></td>
                                            <td><%# Eval("Estado") %></td>
                                            <td>
                                               
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </div>
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
                    { select: 4, sortable: true },
                    { select: 5, sortable: true },
                    { select: 6, sortable: false }, // Desactivar ordenamiento para la columna de "Estado"
                    { select: 7, sortable: false } // Desactivar ordenamiento para la columna de "Acción"
                ]
            });
        });
    </script>


</asp:Content>
