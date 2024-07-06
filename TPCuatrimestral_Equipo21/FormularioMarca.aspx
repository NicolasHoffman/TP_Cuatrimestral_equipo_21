<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormularioMarca.aspx.cs" Inherits="TPCuatrimestral_Equipo21.FormularioMarca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet">
<script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.5.4/dist/umd/popper.min.js"></script>
<script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    
    <script>
       function validar() {
            const txtNombreMarca = document.getElementById("txtNombreMarca");
            const errorMensaje = document.getElementById("errorMensaje");
            const soloLetras = /^[a-zA-Z\s]+$/;

            if (txtNombreMarca.value.trim() === "") {
                txtNombreMarca.classList.add("is-invalid");
                errorMensaje.innerText = "Campo requerido";
                errorMensaje.style.display = "block";
                return false;
            }
            else if (!soloLetras.test(txtNombreMarca.value.trim())) {
                txtNombreMarca.classList.add("is-invalid");
                errorMensaje.innerText = "Solo se permiten letras";
                errorMensaje.style.display = "block";
                return false;
            }

            txtNombreMarca.classList.remove("is-invalid");
            txtNombreMarca.classList.add("is-valid");
            errorMensaje.style.display = "none";
            return true;
        }

        function mostrarModal(success) {
            const modalTitle = document.getElementById("modalTitle");
            const modalBody = document.getElementById("modalBody");
            const btnContinuar = document.getElementById("btnContinuar");

            if (success) {
                modalTitle.innerHTML = '<i class="fas fa-check-circle text-success"></i> Carga realizada correctamente';
                modalBody.innerText = 'La marca ha sido cargada exitosamente.';
                btnContinuar.onclick = function () { window.location.href = 'Marcas.aspx'; };
            } else {
                modalTitle.innerHTML = '<i class="fas fa-times-circle text-danger"></i> Error en la carga';
                modalBody.innerText = 'Hubo un error al cargar la marca.';
                tnContinuar.onclick = function () { window.location.href = 'Marcas.aspx'; };
            }

            $('#resultModal').modal('show');
        }
    </script>

    <div class="container-fluid px-4">
        <h1 class="mt-5">Agregar Nueva Marca</h1>
        <div class="card mt-4">
            <div class="card-header">
                <i class="fas fa-tag fa-lg" style="color: #2c78aa;"></i> Nueva Marca
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-12">
                        <label for="txtNombreMarca" class="form-label">Nombre de la marca:</label>
                        <asp:TextBox ID="txtNombreMarca" ClientIDMode="Static" runat="server" CssClass="form-control mb-3" placeholder="Ingrese marca"></asp:TextBox>
                        <span id="errorMensaje" class="text-danger" style="display:none;">Campo requerido</span>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12">

                        <asp:Panel ID="pnlMensajes" runat="server" CssClass="alert alert-danger" Visible="false">
                        <asp:Label ID="lblMensajeError" runat="server"></asp:Label>
                        </asp:Panel>

                        <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="btn btn-success me-2" OnClientClick="return validar()" OnClick="btnAceptar_Click" />
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger" OnClick="btnCancelar_Click" />
                       
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal -->
    <div class="modal fade" id="resultModal" tabindex="-1" role="dialog" aria-labelledby="modalTitle" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalTitle"></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body" id="modalBody"></div>
                <div class="modal-footer">
                    <button type="button" id="btnContinuar" class="btn btn-primary">Continuar</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
