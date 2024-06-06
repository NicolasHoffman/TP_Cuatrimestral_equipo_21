<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetalleArticulo.aspx.cs" Inherits="TPCuatrimestral_Equipo21.DetalleArticulo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
 
    <div class="container">
        <h1 class="mt-5">Detalle del Artículo</h1>
        <div class="card mb-3">
            <div class="row g-0">
                <div class="col-md-4">
                    <div id="carouselDetalle" class="carousel slide" data-bs-ride="carousel">
                        <div class="carousel-inner">
                            <div class="carousel-item active">
                                <img src="https://via.placeholder.com/300" class="d-block w-100" alt="...">
                            </div>
                            <div class="carousel-item">
                                <img src="https://via.placeholder.com/300" class="d-block w-100" alt="...">
                            </div>
                            <div class="carousel-item">
                                <img src="https://via.placeholder.com/300" class="d-block w-100" alt="...">
                            </div>
                        </div>
                        <button class="carousel-control-prev" type="button" data-bs-target="#carouselDetalle" data-bs-slide="prev">
                            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                            <span class="visually-hidden">Previous</span>
                        </button>
                        <button class="carousel-control-next" type="button" data-bs-target="#carouselDetalle" data-bs-slide="next">
                            <span class="carousel-control-next-icon" aria-hidden="true"></span>
                            <span class="visually-hidden">Next</span>
                        </button>
                    </div>
                </div>
                <div class="col-md-8">
                    <div class="card-body">
                        <h5 class="card-title">Nombre del Artículo</h5>
                        <p class="card-text">Descripción detallada del artículo.</p>
                        <p class="card-text"><strong>Marca:</strong> Marca del Artículo</p>
                        <p class="card-text"><strong>Categoría:</strong> Categoría del Artículo</p>
                        <p class="card-text"><strong>Precio:</strong> $ Precio del Artículo</p>
                        <p class="card-text"><strong>Stock:</strong> Stock disponible</p>
                        <p class="card-text"><strong>Estado:</strong> Estado del Artículo</p>
                        <a href="Articulos.aspx" class="btn btn-secondary">Volver a la lista</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
