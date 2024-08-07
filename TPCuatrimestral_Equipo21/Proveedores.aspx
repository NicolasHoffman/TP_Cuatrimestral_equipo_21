﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Proveedores.aspx.cs" Inherits="TPCuatrimestral_Equipo21.Proveedores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="container-fluid px-4">
        <h1 class="mt-5">Proveedores</h1>
        <ol class="breadcrumb mb-4 mt-4">
            <li class="breadcrumb-item"><a href="index.html">Activo</a></li>
            <li class="breadcrumb-item active">Proveedores</li>
        </ol>
        <div class="card">
            <div class="card-header">
                <i class="fa-solid fa-truck-field" style="color: #2c78aa;"></i> Lista de Proveedores
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
                            <th>Nombre</th>
                            <th>Telefono</th>
                            <th>Email</th>
                            <th>Cuit</th>
                            <th>Direccion</th>
                            <th>Numero</th>
                            <th>Localidad</th>
                            <th>Acción</th>
                        </tr>
                    </thead>
                   
                    <tbody>
                         <asp:Repeater ID="rptProveedor" runat="server" OnItemCommand="rptProveedor_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("Nombre") %></td>
                                    <td><%# Eval("Telefono") %></td>
                                    <td><%# Eval("Email") %></td>
                                    <td><%# Eval("Cuit") %></td>
                                    <td><%# Eval("Direccion.Calle") %></td>
                                    <td><%# Eval("Direccion.Numero") %></td>
                                    <td><%# Eval("Direccion.Localidad") %></td>
                                    <td>
                                       <asp:LinkButton runat="server" CssClass="btn btn-primary btn-sm" CommandName="Seleccionar" CommandArgument='<%# Eval("Id") %>'><i class="fas fa-pen"></i></asp:LinkButton>
                                       <asp:LinkButton runat="server" CssClass="btn btn-danger btn-sm ms-2" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>'><i class="fas fa-trash"></i></asp:LinkButton>
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
                    { select: 4, sortable: true },
                    { select: 5, sortable: true },
                    { select: 6, sortable: false }, // Desactivar ordenamiento para la columna de "Estado"
             
                ]
            });
        });
    </script>
</asp:Content>
