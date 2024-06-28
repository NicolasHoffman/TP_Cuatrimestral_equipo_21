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
                            <asp:TextBox ID="txtNombreCliente" runat="server" CssClass="form-control" placeholder="Ingrese Cuit"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="mb-4">
                            <label for="txtCliente" class="form-label">Cliente:</label>
                            <asp:TextBox ID="txtCliente" runat="server" CssClass="form-control" placeholder="Ingrese Nombre de la Calle"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">
                        <hr />
                        <div class="col-md-4">

                            <div class="mb-4">

                                <label for="txtCodigoProducto" class="form-label">Datos Producto</label>
                                <asp:TextBox ID="txtCodigoProducto" runat="server" CssClass="form-control" placeholder="Ingrese Codigo"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">

                            <div class="mb-4">
                                <label for="txtNombreproducto" class="form-label">Datos Producto</label>
                                <asp:TextBox ID="txtNombreproducto" runat="server" CssClass="form-control" placeholder="Ingrese Codigo"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                </div>

            </div>
        </div>
    </div>


</asp:Content>
