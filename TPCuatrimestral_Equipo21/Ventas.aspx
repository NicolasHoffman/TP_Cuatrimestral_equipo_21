<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ventas.aspx.cs" Inherits="TPCuatrimestral_Equipo21.Ventas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .input-group-append button {
            border-radius: 0;
        }

            .input-group-append button i {
                font-size: 0.3rem;
            }
    </style>
    <div class="container-fluid px-4">
        <h1 class="mt-5">Nueva Venta</h1>
        <div class="card mt-4">
            <div class="card-body">
                <div class="row">
                    <label for="txtNombreCliente" class="form-label">Datos Cliente</label>
                    <div class="col-md-4">
                        <div class="mb-4">
                            <label for="txtNombreCliente" class="form-label">Buscar Cliente Por Dni</label>
                            <asp:TextBox ID="txtNombreCliente" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtNombreCliente_TextChanged" placeholder="Ingrese Dni"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="mb-4">
                            <label for="txtCliente" class="form-label">Cliente:</label>
                            <asp:TextBox ID="txtCliente" runat="server" CssClass="form-control" ReadOnly="true" Text=""></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="mb-4">
                            <asp:Button ID="btnCrearCliente" runat="server" Text="Crear Cliente" CssClass="btn btn-primary" OnClick="btnCrearCliente_Click" Visible="false" />
                        </div>
                    </div>
                </div>

                <div class="row mt-4">
                    <div class="col-md-4">
                        <div class="mb-4">
                            <label for="txtCodigoProducto" class="form-label">Buscar Producto por Codigo</label>
                            <asp:TextBox ID="txtCodigoProducto" runat="server" CssClass="form-control" placeholder="Ingrese Codigo" Enabled="false" AutoPostBack="true" OnTextChanged="txtCodigoProducto_TextChanged"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="mb-4">
                            <label for="txtNombreproducto" class="form-label">Buscar por Nombre y/o por Marca</label>
                            <asp:TextBox ID="txtNombreproducto" runat="server" CssClass="form-control" placeholder="Buscar por Nombre y/o por Marca" Enabled="false" AutoPostBack="true" OnTextChanged="txtNombreproducto_TextChanged"></asp:TextBox>
                        </div>
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
                            <th>Cantidad</th>
                            <th>Acción</th>
                        </tr>
                    </thead>
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
                                        <div class="input-group">
                                            <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control input-cantidad" Style="width: 1px;" Text="1"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td>
                                        <asp:LinkButton runat="server" CssClass="btn btn-success btn-sm" CommandName="Agregar" CommandArgument='<%# Eval("Id") %>'><i class="fas fa-plus"></i> Agregar</asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>

                <hr />
                <h1 class="mt-5">Carrito</h1>
                <table id="datatablesSimple2" class="table table-striped table-bordered">
                    <thead>
                        <tr>
                            <th>Codigo</th>
                            <th>Nombre</th>
                            <th>Precio</th>
                            <th>Cantidad</th>
                            <th>Total</th>
                            <th>Acción</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("Id") %></td>
                                    <td><%# Eval("Nombre") %></td>
                                    <td><%# Eval("Precio") %></td>    
                                    <td><%# Eval("Cantidad") %></td>
                                    <td><%# Eval("Total") %></td>
                                    <td>
                                        <asp:LinkButton runat="server" CssClass="btn btn-danger btn-sm" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>'><i class="fas fa-minus"></i> Eliminar</asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
                <div>Total a Pagar: <asp:Label ID="Label1" runat="server" Text="S/. 0.00"></asp:Label></div>
                <asp:Button ID="btnGenerarVenta" runat="server" Text="Generar Venta" CssClass="btn btn-success" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger" />
                
            </div>
        </div>
    </div>

    <script src="https://cdn.jsdelivr.net/npm/simple-datatables@7.1.2/dist/umd/simple-datatables.min.js" crossorigin="anonymous"></script>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js" crossorigin="anonymous"></script>
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
                    { select: 7, sortable: false }, // Desactivar ordenamiento para la columna de "Cantidad"
                    { select: 8, sortable: false }  // Desactivar ordenamiento para la columna de "Acción"
                ]
            });
        });
    </script>

</asp:Content>
