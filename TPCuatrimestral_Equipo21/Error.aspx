<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="TPCuatrimestral_Equipo21.Error" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Error</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Ocurrió un error</h1>
            <asp:Label ID="lblErrorMessage" runat="server" Text="Lo sentimos, ocurrió un error. Por favor, inténtelo de nuevo más tarde."></asp:Label>
        </div>
    </form>
</body>
</html>
