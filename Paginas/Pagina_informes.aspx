<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pagina_informes.aspx.cs" Inherits="proyecto_final.Pagina_informes" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Clínica Médica - Informes</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css" />
    <style type="text/css">
        body {
            font-family: 'Segoe UI', sans-serif;
            background-color: #f5f7fa;
            margin: 0;
            padding: 20px;
        }

        
        .header-bar {
            background-color: #153f8a;
            color: white;
            padding: 15px 30px; 
            border-radius: 8px;
            margin-bottom: 30px;
            display: flex;
            justify-content: space-between;
            align-items: center;
            box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
        }

        .header-bar h1 {
            font-size: 22px;
            font-weight: 600;
            margin: 0;
        }

        .main-container {
            max-width: 950px;
            margin: 0 auto;
            background-color: white;
            border-radius: 10px;
            box-shadow: 0 4px 12px rgba(0,0,0,0.1);
            overflow: hidden;
        }

        .page-header {
            background: linear-gradient(135deg, #153f8a 0%, #1f56b5 100%);
            color: white;
            text-align: center;
            padding: 30px 20px;
        }

        .page-header h2 {
            font-size: 26px;
            font-weight: 600;
            margin-bottom: 5px;
        }

        .content-area {
            padding: 40px 50px;
        }

        .menu-grid {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(220px, 1fr));
            gap: 20px;
            margin-bottom: 30px;
        }

        
        .menu-button {
            background-color: white;
            border: 2px solid #e0e4e9;
            border-radius: 10px;
            padding: 25px 20px;
            text-align: center;
            cursor: pointer;
            transition: all 0.3s ease;
            display: flex;
            flex-direction: column;
            align-items: center;
            gap: 12px;
            text-decoration: none;  
            color: #333;
        }

        .menu-button:hover {
            border-color: #153f8a;
            background-color: #f8f9fb;
            transform: translateY(-2px);
            box-shadow: 0 4px 12px rgba(21, 63, 138, 0.15);
        }

        .menu-button i {
            font-size: 36px;
            color: #153f8a;
        }

        .menu-button span {
            color: #333;
            font-size: 15px;
            font-weight: 600;
            text-decoration: none; 
        }

        .report-section {
            border-top: 1px solid #ddd;
            padding-top: 25px;
            margin-top: 20px;
        }

        table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 15px;
        }

        table th, table td {
            border: 1px solid #e0e0e0;
            padding: 10px 12px;
            text-align: left;
        }

        table th {
            background-color: #153f8a;
            color: white;
            font-weight: 600;
        }

        table tr:nth-child(even) {
            background-color: #f8f9fb;
        }

        .chart, .bar-chart {
            height: 250px;
            background: #f8f9fa;
            border: 1px solid #ddd;
            border-radius: 10px;
            padding: 20px;
            margin: 20px 0;
            text-align: center;
            color: #555;
            font-weight: 600;
            line-height: 250px;
        }

        .btn-volver {
            background-color: #6c757d;
            color: white;
            border: none;
            border-radius: 8px;
            padding: 12px 30px;
            font-size: 15px;
            font-weight: 600;
            cursor: pointer;
            transition: all 0.3s ease;
            display: inline-flex;
            align-items: center;
            gap: 8px;
            margin-top: 20px;
        }

        .btn-volver:hover {
            background-color: #5a6268;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">
        <div class="header-bar">
            <h1><i class="fa fa-heartbeat"></i> Clínica Médica</h1>
            <div class="user-info">
                <i class="fa fa-user-circle"></i>
                <asp:Label ID="lblUsuario" runat="server" Text="Administrador"></asp:Label>
            </div>
        </div>

        <div class="main-container">
            <div class="page-header">
                <i class="fa fa-chart-line"></i>
                <h2>Panel de Informes</h2>
                <p>Seleccione un informe para visualizar sus resultados</p>
            </div>

            <div class="content-area">
                <div class="menu-grid">
                    <asp:LinkButton ID="lnkInforme1" runat="server" CssClass="menu-button" OnClick="lnkInforme1_Click">
                        <i class="fa fa-chart-pie"></i><span>Ausentes vs Presentes</span>
                    </asp:LinkButton>

                    <asp:LinkButton ID="lnkInforme2" runat="server" CssClass="menu-button" OnClick="lnkInforme2_Click">
                        <i class="fa fa-chart-column"></i><span>Asistencia Mensual</span>
                    </asp:LinkButton>

                    <asp:LinkButton ID="lnkInforme3" runat="server" CssClass="menu-button" OnClick="lnkInforme3_Click">
                        <i class="fa fa-table"></i><span>Listado de Ausentes</span>
                    </asp:LinkButton>

                    <asp:LinkButton ID="lnkInforme4" runat="server" CssClass="menu-button" OnClick="lnkInforme4_Click">
                        <i class="fa fa-stethoscope"></i><span>Promedio por Especialidad</span>
                    </asp:LinkButton>
                </div>

                <!-- PANEL 1 -->
                <asp:Panel ID="pnlInforme1" runat="server" Visible="true" CssClass="report-section">
                    <h3><i class="fa fa-chart-pie"></i> Ausentes vs Presentes</h3>
                    <div class="chart">Gráfico Circular Simulado (70% Presentes / 30% Ausentes)</div>
                </asp:Panel>

                <!-- PANEL 2 -->
                <asp:Panel ID="pnlInforme2" runat="server" Visible="false" CssClass="report-section">
                    <h3><i class="fa fa-chart-column"></i> Asistencia Mensual</h3>
                    <div class="bar-chart">Gráfico de Barras Simulado</div>
                </asp:Panel>

                <!-- PANEL 3 -->
                <asp:Panel ID="pnlInforme3" runat="server" Visible="false" CssClass="report-section">
                    <h3><i class="fa fa-table"></i> Listado de Ausentes</h3>
                    <table>
                        <tr><th>Paciente</th><th>Fecha</th><th>Médico</th><th>Especialidad</th></tr>
                        <tr><td>Gómez, Juan</td><td>10/10/2025</td><td>Dr. Martínez</td><td>Cardiología</td></tr>
                        <tr><td>Pérez, Laura</td><td>12/10/2025</td><td>Dra. López</td><td>Clínica General</td></tr>
                    </table>
                </asp:Panel>

                <!-- PANEL 4 -->
                <asp:Panel ID="pnlInforme4" runat="server" Visible="false" CssClass="report-section">
                    <h3><i class="fa fa-stethoscope"></i> Promedio de Asistencia por Especialidad</h3>
                    <table>
                        <tr><th>Especialidad</th><th>Turnos Totales</th><th>Presentes</th><th>Ausentes</th><th>% Asistencia</th></tr>
                        <tr><td>Cardiología</td><td>40</td><td>32</td><td>8</td><td>80%</td></tr>
                        <tr><td>Pediatría</td><td>30</td><td>28</td><td>2</td><td>93%</td></tr>
                    </table>
                </asp:Panel>

                <div style="text-align:center;">
                    <asp:Button ID="btnVolver" runat="server" CssClass="btn-volver" Text="Volver al Menú Principal" OnClick="btnVolver_Click" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>