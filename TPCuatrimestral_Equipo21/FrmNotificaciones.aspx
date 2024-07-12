<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmNotificaciones.aspx.cs" Inherits="TPCuatrimestral_Equipo21.FrmNotificaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
     <div class="container-fluid px-4">
        <h1 class="mt-5">Notificaciones</h1>
        <ol class="breadcrumb mb-4 mt-4">
            <li class="breadcrumb-item"><a href="index.html">Activo</a></li>
            <li class="breadcrumb-item active">Notificaciones</li>
        </ol>
        <div class="card">
            <div class="card-header">
                <i class="fas fa-tag fa-lg" style="color: #2c78aa;"></i>Lista de Marcas
            </div>
            <div class="card-body">

                <table id="datatablesSimple" class="table table-striped table-bordered">
                    <thead>
                        <tr>
                            <th>NOTIFICACION</th>
                            <th>MENSAJE</th>
                            <th>FECHA</th>
                            <th style="width: 150px;">MARCAR LEIDO</th>
                        </tr>
                    </thead>
                    <tfoot>
                        <tr>
                            <th>NOTIFICACION</th>
                            <th>MENSAJE</th>
                            <th>FECHA</th>
                            <th style="width: 150px;">MARCAR LEIDO</th>
                        </tr>
                    </tfoot>
                    <tbody>
                         <asp:Repeater ID="rptNoti" runat="server" OnItemCommand="rptNoti_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("Id") %></td>
                                    <td><%# Eval("Mensaje") %></td>
                                    <td><%# Convert.ToDateTime(Eval("Fecha")).ToShortDateString() %></td>
                                    <td>
                                       <!-- <asp:LinkButton runat="server" CssClass="btn btn-primary btn-sm" CommandName="Seleccionar" CommandArgument='<%# Eval("Id") %>'><i class="fas fa-pen"></i></asp:LinkButton>
                                       <asp:LinkButton runat="server" CssClass="btn btn-danger btn-sm ms-2" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>'><i class="fas fa-trash"></i></asp:LinkButton> -->
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
                    { select: 2, sortable: false }// Desactivar ordenamiento para la columna de "Acción"
                ]
            });
        });
    </script>

</asp:Content>
