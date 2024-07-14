<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmMensaje.aspx.cs" Inherits="TPCuatrimestral_Equipo21.FrmMensaje" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mensaje</title>
    <!-- Bootstrap CSS -->
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" rel="stylesheet" />
    <!-- FontAwesome para los íconos -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" rel="stylesheet" />
    <!-- jQuery -->
    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js"></script>
    <!-- Bootstrap JS -->
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>
    <script type="text/javascript">
        function showMessage(isSuccess, title, body, redirectUrl) {
            let icon = isSuccess
                ? '<i class="fas fa-check-circle fa-sm" style="color: #63E6BE;"></i>'
                : '<i class="fas fa-times-circle fa-sm" style="color: #E66363;"></i>';

            let messageHtml = `
                <div class="modal fade" id="messageModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                  <div class="modal-dialog" role="document">
                    <div class="modal-content">
                      <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">${icon} ${title}</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                          <span aria-hidden="true">&times;</span>
                        </button>
                      </div>
                      <div class="modal-body">
                        ${body}
                      </div>
                      <div class="modal-footer">
                        <button type="button" class="btn btn-primary" onclick="window.location.href='${redirectUrl}'">Continuar</button>
                      </div>
                    </div>
                  </div>
                </div>
            `;

            $("body").append(messageHtml);
            $("#messageModal").modal('show');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <!-- Contenido principal -->
        </div>
    </form>
</body>
</html>