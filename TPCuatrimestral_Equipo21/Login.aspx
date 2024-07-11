<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TPCuatrimestral_Equipo21.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Iniciar Sesión</title>

    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background-color: #EAF8FF;
        }
        .login-container {
            max-width: 400px;
            margin: 50px auto;
            padding: 20px;
            background-color: white;
            border-radius: 10px;
            box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
        }
        .logo {
            display: block;
            margin: 0 auto 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="login-container">
                <img src="logo_url_aqui" alt="Logo" class="logo" />
                <h2 class="text-center">Iniciar Sesión</h2>
                <div class="form-group">
                    <label for="txtUsuario">Usuario</label>
                    <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" placeholder="Ingresá tu Usuario"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtPassword">Contraseña</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Ingrese su Contraseña"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group text-center">
                    <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" CssClass="btn btn-primary btn-block" OnClick="btnIngresar_Click" />
                </div>
                <p class="text-center"><a href="#" data-toggle="modal" data-target="#recuperarModal">¿Olvidaste tu contraseña?</a></p>
            </div>
        </div>

        <!-- Recu Contra-->
        <div class="modal fade" id="recuperarModal" tabindex="-1" aria-labelledby="recuperarModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="recuperarModalLabel">Recuperar Contraseña</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <label for="txtEmailRecuperacion">Ingrese su correo electrónico</label>
                            <asp:TextBox ID="txtEmailRecuperacion" runat="server" CssClass="form-control" placeholder="Email"></asp:TextBox>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnRecuperar" runat="server" Text="Enviar Código" CssClass="btn btn-primary" OnClick="btnRecuperar_Click" />
                    </div>
                </div>
            </div>
        </div>
    </form>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            <% if (Session["Error"] != null) { %>
            $('#errorModal').modal('show');
                <% Session["Error"] = null; %>
            <% } %>
        });
    </script>
</body>
</html>