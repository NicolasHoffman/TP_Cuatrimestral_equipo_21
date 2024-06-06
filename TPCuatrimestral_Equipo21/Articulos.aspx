<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Articulos.aspx.cs" Inherits="TPCuatrimestral_Equipo21.Articulos" %>
<%@ Import Namespace="System.Data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <div class="container-fluid px-4">
        <h1 class="mt-5">Artículos</h1>
        <ol class="breadcrumb mb-4 mt-4">
            <li class="breadcrumb-item"><a href="index.html">Activo</a></li>
            <li class="breadcrumb-item active">Artículos</li>
        </ol>
        <div class="card">
            <div class="card-header">
                <i class="fas fa-tag fa-lg" style="color: #2c78aa;"></i> Lista de Artículos
            </div>
            <div class="card-body">
                <div class="row mb-3">
                    <div class="col-12 col-md-6 d-flex align-items-center mb-2 mb-md-0">
                        <label for="ddlCantidadRegistros" class="form-label me-2">Entrada por página:</label>
                        <asp:DropDownList ID="ddlCantidadRegistros" runat="server" CssClass="form-select me-2 w-auto">
                            <asp:ListItem Text="10" Value="10"></asp:ListItem>
                            <asp:ListItem Text="20" Value="20"></asp:ListItem>
                            <asp:ListItem Text="30" Value="30"></asp:ListItem>
                            <asp:ListItem Text="40" Value="40"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-12 col-md-6 d-flex justify-content-md-end align-items-center">
                        <label for="txtBuscar" class="form-label me-2">Buscar por nombre:</label>
                        <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control me-2 w-auto" placeholder="Search" aria-label="Search"></asp:TextBox>
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-outline-success" />
                    </div>
                </div>
                <asp:GridView ID="gvResultados" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered">
                    <Columns>
                        <asp:BoundField DataField="Codigo" HeaderText="Codigo" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
                        <asp:BoundField DataField="Marca" HeaderText="Marca" />
                        <asp:BoundField DataField="Categoria" HeaderText="Categoria" />
                        <asp:BoundField DataField="Precio" HeaderText="Precio" />
                        <asp:BoundField DataField="Estado" HeaderText="Estado" />
                        <asp:TemplateField HeaderText="Acción">
                            <ItemTemplate>
                                <a href='detallearticulo.aspx?Codigo=<%# Eval("Codigo") %>' class="btn btn-info btn-sm">
                                    <i class="fas fa-info-circle"></i>
                                </a>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

</asp:Content>
<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarDatos();
        }
    }

    private void CargarDatos()
    {
        // Crear una tabla de datos simulada
        DataTable dt = new DataTable();
        dt.Columns.Add("Codigo");
        dt.Columns.Add("Nombre");
        dt.Columns.Add("Descripcion");
        dt.Columns.Add("Marca");
        dt.Columns.Add("Categoria");
        dt.Columns.Add("Precio", typeof(decimal));
        dt.Columns.Add("Estado");

        // Añadir filas a la tabla de datos simulada
        dt.Rows.Add("A001", "Artículo 1", "Descripción del artículo 1", "Marca 1", "Categoría 1", 100, "Activo");
        dt.Rows.Add("A002", "Artículo 2", "Descripción del artículo 2", "Marca 2", "Categoría 2", 200, "Inactivo");

        // Enlazar la tabla de datos al GridView
        gvResultados.DataSource = dt;
        gvResultados.DataBind();
    }
</script>