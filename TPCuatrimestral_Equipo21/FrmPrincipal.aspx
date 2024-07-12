<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmPrincipal.aspx.cs" Inherits="TPCuatrimestral_Equipo21.FrmPrincipal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .card-container {
            margin-top: 20px; 
            margin-left: 20px; 
            margin-right: 20px;
            margin-bottom: 20px;
        }
    </style>

     <div class="row">
        <!-- Gráfico de Ventas por Mes -->
        <div class="col-xl-6">
            <div class="card border-primary mb-3 card-container">
                <div class="card-header bg-primary text-white">
                    <i class="fas fa-chart-bar me-1"></i>
                    Ventas Mensuales
                </div>
                <div class="card-body">
                    <canvas id="ventasPorMesChart" width="100%" height="80"></canvas>
                </div>
            </div>
        </div>

        <!-- Gráfico de Ventas por Año -->
        <div class="col-xl-6">
            <div class="card border-primary mb-3 card-container">
                <div class="card-header bg-primary text-white">
                    <i class="fas fa-chart-bar me-1"></i>
                    Ventas Anuales
                </div>
                <div class="card-body">
                    <canvas id="ventasPorAnioChart" width="100%" height="80"></canvas>
                </div>
            </div>
        </div>
    </div>

    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <script>
        document.addEventListener('DOMContentLoaded', (event) => {
            // Configuración y datos para el gráfico de Ventas por Mes
            var ctxMes = document.getElementById('ventasPorMesChart').getContext('2d');
            var ventasPorMes = JSON.parse('<%= VentasPorMesJson %>');

            var labelsMes = ventasPorMes.map(v => v.Mes);
            var dataMes = ventasPorMes.map(v => v.Cantidad);

            var myBarChartMes = new Chart(ctxMes, {
                type: 'bar',
                data: {
                    labels: labelsMes,
                    datasets: [{
                        label: 'Ventas por Mes',
                        data: dataMes,
                        backgroundColor: 'rgba(75, 192, 192, 0.2)',
                        borderColor: 'rgba(75, 192, 192, 1)',
                        borderWidth: 1
                    }]
                },
                options: {
                    scales: {
                        y: {
                            beginAtZero: true
                        }
                    }
                }
            });

            // Configuración y datos para el gráfico de Ventas por Año
            var ctxAnio = document.getElementById('ventasPorAnioChart').getContext('2d');
            var ventasPorAnio = JSON.parse('<%= VentasPorAnioJson %>');

            var labelsAnio = ventasPorAnio.map(v => v.Anio);
            var dataAnio = ventasPorAnio.map(v => v.Cantidad);

            var myBarChartAnio = new Chart(ctxAnio, {
                type: 'bar',
                data: {
                    labels: labelsAnio,
                    datasets: [{
                        label: 'Ventas por Año',
                        data: dataAnio,
                        backgroundColor: 'rgba(54, 162, 235, 0.2)',
                        borderColor: 'rgba(54, 162, 235, 1)',
                        borderWidth: 1
                    }]
                },
                options: {
                    scales: {
                        y: {
                            beginAtZero: true
                        }
                    }
                }
            });
        });
    </script>
</asp:Content>
