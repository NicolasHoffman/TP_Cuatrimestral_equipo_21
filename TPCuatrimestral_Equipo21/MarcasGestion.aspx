<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MarcasGestion.aspx.cs" Inherits="TPCuatrimestral_Equipo21.MarcasGestion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- Metadatos -->
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Gestión de Marcas</title>
    
    <!-- Referencias CSS (Bootstrap, FontAwesome, etc.) -->
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" rel="stylesheet" />
    
    <!-- Referencias JavaScript (jQuery, Bootstrap, etc.) -->
    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>
    
</head>
<body>
    <!-- Formulario ASP.NET -->
    <form id="form1" runat="server">
        <div class="container-fluid px-4">
            <h1 class="mt-5">Gestión de Marcas</h1>
            <div class="card mt-4">
                <div class="card-header">
                    <i class="fas fa-tag fa-lg" style="color: #2c78aa;"></i> Lista de Marcas
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-12">
                            <!-- Campo de texto para ingresar el nombre de la marca -->
                            <label for="txtNombreMarca" class="form-label">Nombre de la marca:</label>
                            <asp:TextBox ID="txtNombreMarca" runat="server" CssClass="form-control mb-3" placeholder="Ingrese marca"></asp:TextBox>
                            
                            <!-- Botones Aceptar y Cancelar -->
                            <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="btn btn-success me-2" OnClick="btnAceptar_Click" />
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger" OnClick="btnCancelar_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>