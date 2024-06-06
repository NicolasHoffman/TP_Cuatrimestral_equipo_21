<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Marcas.aspx.cs" Inherits="TPCuatrimestral_Equipo21.Marcas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid px-4">
        <h1 class="mt-5">Marcas</h1>
        <ol class="breadcrumb mb-4 mt-4">
            <li class="breadcrumb-item"><a href="index.html">Activo</a></li>
            <li class="breadcrumb-item active">Marcas</li>
        </ol>
        <div class="card">
            <div class="card-header">
                <i class="fas fa-tag fa-lg" style="color: #2c78aa;"></i>Lista de Marcas
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-12">
                        
                        <asp:Button ID="btnCrearNuevo" runat="server" Text="Crear Nuevo" CssClass="btn btn-success" OnClientClick="return abrirVentana();" />
                    </div>
                </div>
                <hr />
                <div class="row mb-3">
                    <div class="col-12 col-md-6 d-flex align-items-center mb-2 mb-md-0">
                        <label for="ddlCantidadRegistros" class="form-label me-2">Entrada por página:</label>
                        <asp:DropDownList ID="ddlCantidadRegistros" runat="server" OnSelectedIndexChanged="ddlCantidadRegistros_SelectedIndexChanged" AutoPostBack="true" CssClass="form-select me-2 w-auto">
                            <asp:ListItem Text="10" Value="10"></asp:ListItem>
                            <asp:ListItem Text="20" Value="20"></asp:ListItem>
                            <asp:ListItem Text="30" Value="30"></asp:ListItem>
                            <asp:ListItem Text="40" Value="40"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-12 col-md-6 d-flex justify-content-md-end align-items-center">
                        <label for="txtBuscar" class="form-label me-2">Buscar por nombre:</label>
                        <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control me-2 w-auto" placeholder="Search" aria-label="Search"></asp:TextBox>
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" CssClass="btn btn-outline-success" />
                    </div>
                </div>
                <asp:GridView ID="gvResultados" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered" DataKeyNames="Id" AllowPaging="true" OnPageIndexChanging="gvResultados_PageIndexChanging" OnRowDeleting="gvResultados_RowDeleting">
                    <Columns>
                        <asp:TemplateField HeaderText="ID">
                            <ItemTemplate>
                                <%# Eval("Id") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nombre" SortExpression="Nombre">
                            <HeaderTemplate>
                                Nombre
                                <!-- Añadí íconos de flechas para ordenar -->
                                <i class="fas fa-sort-up" onclick="sortTable(1, 'asc')"></i>
                                <i class="fas fa-sort-down" onclick="sortTable(1, 'desc')"></i>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# Eval("Nombre") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Acción" ItemStyle-Width="120px">
                            <ItemTemplate>
                                <button type="button" class="btn btn-primary btn-sm"><i class="fas fa-pen"></i></button>
                               
                                <asp:LinkButton ID="btnEliminar" runat="server" CommandName="Delete" CommandArgument='<%# Eval("Id") %>' OnClientClick="return confirm('¿Estás seguro de que deseas eliminar esta marca?');" CssClass="btn btn-danger btn-sm ms-2" Text='<i class="fas fa-trash"></i>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
   
    <script>
        
        //Esto lo uso para cargar una nueva marca 
        function abrirVentana() {
            // tamaño de la pantalla emer
            var width = 400;
            var height = 300;

            var left = (window.innerWidth / 3) - (width / 3) + window.screenX;
            var top = (window.innerHeight / 3) - (height / 3) + window.screenY;

            // Abre una nueva ventana emergente en el centro de la pantalla
            var nuevaVentana = window.open('MarcasGestion.aspx', '_blank', 'width=' + width + ',height=' + height + ',top=' + top + ',left=' + left + ',resizable=yes');

            // Deshabilitar la navegación en la ventana principal mientras la ventana emergente esta abierta
            var bloquearVentanaPrincipal = setInterval(function () {
                if (nuevaVentana.closed) {
                    clearInterval(bloquearVentanaPrincipal);
                } else {
                    nuevaVentana.focus();
                }
            }, 100);

            // Verifica si la ventana principal se cerro 
            window.onbeforeunload = function () {
                nuevaVentana.close();
            };

            return false;
        }
        function sortTable(columnIndex, order) {
            var table, rows, switching, i, x, y, shouldSwitch;
            table = document.getElementById("<%= gvResultados.ClientID %>"); // Usar ClientID para obtener el ID generado del GridView
            switching = true;
            paginationRow = table.rows[table.rows.length - 1];// pa la paginacion
            while (switching) {
                switching = false;
                rows = table.rows;
                for (i = 1; i < (rows.length - 2); i++) {
                    shouldSwitch = false;
                    x = rows[i].getElementsByTagName("TD")[columnIndex];
                    y = rows[i + 1].getElementsByTagName("TD")[columnIndex];
                    if (order === "asc") {
                        if (x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase()) {
                            shouldSwitch = true;
                            break;
                        }
                    } else if (order === "desc") {
                        if (x.innerHTML.toLowerCase() < y.innerHTML.toLowerCase()) {
                            shouldSwitch = true;
                            break;
                        }
                    }
                }
                if (shouldSwitch) {
                    rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
                    switching = true;
                }
            }
            table.appendChild(paginationRow);
        }

    </script>
</asp:Content>

