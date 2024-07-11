<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Recuperar.aspx.cs" Inherits="TPCuatrimestral_Equipo21.Recuperar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
   <title>Restablecer Contraseña</title>

    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background-color: #EAF8FF;
        }
        .recuperar-container {
            max-width: 400px;
            margin: 50px auto;
            padding: 20px;
            background-color: white;
            border-radius: 10px;
            box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="recuperar-container">
                <h2 class="text-center">Restablecer Contraseña</h2>
                <div class="form-group">
                    <label for="txtCodigo">Código de Recuperación</label>
                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control" placeholder="Ingrese el código de recuperación"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtNuevaContrasena">Nueva Contraseña</label>
                    <asp:TextBox ID="txtNuevaContrasena" runat="server" TextMode="Password" CssClass="form-control" placeholder="Ingrese su nueva contraseña"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Button ID="btnRestablecer" runat="server" Text="Restablecer Contraseña" CssClass="btn btn-primary btn-block" OnClick="btnRestablecer_Click" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>