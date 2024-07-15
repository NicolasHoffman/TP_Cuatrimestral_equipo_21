<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ventas.aspx.cs" Inherits="TPCuatrimestral_Equipo21.Ventas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hdnClienteDireccion" runat="server" />
    <asp:HiddenField ID="hdnClienteSeleccionado" runat="server" />
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
                            <th>Imagen</th>
                            <th>Codigo</th>
                            <th>Nombre</th>
                            <th>Descripción</th>
                            <th>Marca</th>
                            <th>Categoria</th>
                            <th>Precio</th>
                            <th>Stock</th>
                            <th>Cantidad</th>
                            <th>Acción</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptVentas" runat="server" OnItemCommand="rptVentas_ItemCommand">
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
                                    <td><%# Eval("Stock") %></td>

                                    <td>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control input-cantidad" Style="width: 1px;" Text="1" data-stock='<%# Eval("Stock") %>'></asp:TextBox>
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
                <div>Total a Pagar:
                    <asp:Label ID="Label1" runat="server" Text="S/. 0.00"></asp:Label></div>

                <div class="row mt-4">
                    <div class="col-md-6">
                        <label for="ddlFormaPago" class="form-label">Forma de Pago:</label>
                        <asp:DropDownList ID="ddlFormaPago" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Efectivo" Value="Efectivo"></asp:ListItem>
                            <asp:ListItem Text="Transferencia Bancaria" Value="Transferencia Bancaria"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <hr />
                <asp:Button ID="btnGenerarVenta" runat="server" Text="Generar Venta" CssClass="btn btn-success" OnClick="btnGenerarVenta_Click" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger" OnClick="btnCancelar_Click" />

            </div>
        </div>
    </div>

    <!-- Modal para seleccionar la forma de entrega -->
    <div class="modal fade" id="modalFormaEntrega" tabindex="-1" role="dialog" aria-labelledby="modalFormaEntregaLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalFormaEntregaLabel">Elegí la forma de entrega</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div>
                        <asp:RadioButton ID="rbtnDomicilio" runat="server" GroupName="FormaEntrega" Text="Enviar a domicilio" Checked="True" />
                        <div class="ml-4">
                            <p id="direccionClienteModal"></p>
                            <a href="FormularioDireccion.aspx" class="btn btn-link">Editar o elegir otro domicilio</a>
                        </div>
                    </div>
                    <div>
                        <asp:RadioButton ID="rbtnDepósito" runat="server" GroupName="FormaEntrega" Text="Retirar en Depósito" />
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnConfirmarFormaEntrega" runat="server" Text="Confirmar" CssClass="btn btn-primary" OnClick="btnConfirmarFormaEntrega_Click" />
                </div>
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
                    { select: 6, sortable: true },
                    { select: 7, sortable: false }, // Desactivar ordenamiento para la columna de "Estado"
                    { select: 8, sortable: false }, // Desactivar ordenamiento para la columna de "Cantidad"
                    { select: 9, sortable: false }  // Desactivar ordenamiento para la columna de "Acción"
                ]
            });
        });
    </script>

    <script>

        $(document).ready(function () {
            // Validación para el campo de cantidad
            $(document).on('input', '.input-cantidad', function () {
                var value = $(this).val();
                var stock = $(this).data('stock');

                if (!/^\d+$/.test(value) || parseInt(value) <= 0) {
                    $(this).val('');
                    alert('Por favor ingrese un número positivo.');
                } else if (parseInt(value) > stock) {
                    $(this).val('');
                    alert('La cantidad ingresada supera el stock disponible.');
                }
            });

            $('#<%=btnGenerarVenta.ClientID %>').click(function (e) {
                e.preventDefault();

                // Obtener la dirección del cliente desde el campo oculto
                var direccionCliente = $('#<%=hdnClienteDireccion.ClientID %>').val();

                // Mostrar la dirección en el modal
                $('#direccionClienteModal').text(direccionCliente);

                $('#modalFormaEntrega').modal('show');
            });
            // para q cancelar me cierre modal
            $('.btn-secondary[data-dismiss="modal"]').click(function () {
                $('#modalFormaEntrega').modal('hide');
            });
            // para la cruz!! 
            $('#modalFormaEntrega .close').click(function () {
                $('#modalFormaEntrega').modal('hide');
            });
        });
    </script>

</asp:Content>
