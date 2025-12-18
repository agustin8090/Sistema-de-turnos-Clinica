<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pagina_informes.aspx.cs" Inherits="proyecto_final.Pagina_informes" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Clínica Médica - Informes</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css" />
    <link rel="icon" href="../imagen/logoUTN.png" type="image/png"/>
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

        .btn-generar {
            background-color: white;
            border: 2px solid #153f8a;
            border-radius: 10px;
            padding: 14px 25px;
            font-size: 15px;
            font-weight: 600;
            cursor: pointer;
            color: #153f8a;
            transition: 0.3s ease;
        }

        .btn-generar:hover {
            background-color: #153f8a;
            color: white;
        }

        .acciones-centrado {
            text-align: center;
            margin-bottom: 20px;
        }

        .resultados-container {
            display: flex;
            justify-content: space-around;
            gap: 20px;
            flex-wrap: wrap;
            margin-top: 20px;
        }

        .card {
            width: 210px;
            padding: 20px;
            text-align: center;
            border-radius: 10px;
            font-weight: bold;
        }

        .card h4 {
            margin-bottom: 10px;
        }

        .card-presentes {
            background: #e6f4ea;
            border: 1px solid #b4e0c4;
            color: #1e7e34;
        }
        .card-ausentes {
            background: #fdecea;
            border: 1px solid #f5b5ae;
            color: #c82333;
        }
        .card-total {
            background: #e9eefc;
            border: 1px solid #b4c7f7;
            color: #153f8a;
        }

        .card label {
            font-size: 28px;
            font-weight: bold;
        }

    </style>
</head>

<body>
<form id="form1" runat="server">

    <div class="header-bar">
        <h1><i class="fa fa-heartbeat"></i> Clínica Médica</h1>
        <div class="user-info">
            <i class="fa fa-user-circle"></i>
            <asp:Label ID="lblAdmin" runat="server"></asp:Label>
        </div>
    </div>

    <div class="main-container">
        <div class="page-header">
            <i class="fa fa-chart-line"></i>
            <h2>Panel de Informes</h2>
            <p>Seleccione un informe para visualizar sus resultados</p>
        </div>

        <div class="content-area">

            <!-- BOTONES DEL MENÚ -->
            <div class="menu-grid">

                <asp:LinkButton ID="lnkInforme1" runat="server" CssClass="menu-button" OnClick="lnkInforme1_Click">
                    <i class="fa fa-chart-pie"></i><span>Ausentes vs Presentes</span>
                </asp:LinkButton>

                <asp:LinkButton ID="lnkInforme2" runat="server" CssClass="menu-button" OnClick="lnkInforme2_Click">
                    <i class="fa fa-calendar-days"></i><span>Turnos entre fechas</span>
                </asp:LinkButton>

                <asp:LinkButton ID="lnkInforme3" runat="server" CssClass="menu-button" OnClick="lnkInforme3_Click">
                    <i class="fa fa-stethoscope"></i><span>Asistencia por Especialidad</span>
                </asp:LinkButton>

            </div>

            <!-- =============== INFORME 1 ======================= -->

            <asp:Panel ID="pnlInforme1" runat="server" Visible="false" CssClass="report-section">

                <h3><i class="fa fa-chart-pie"></i> Ausentes vs Presentes</h3>

                <div class="acciones-centrado">
                    <label style="font-weight:bold; margin-right:10px;">Filtrar por Especialidad:</label>

                    <asp:DropDownList ID="ddlEspecialidad" runat="server" CssClass="form-select"
                        style="padding: 8px 15px; border:2px solid #153f8a; border-radius:8px;">
                    </asp:DropDownList>

                    <asp:Button ID="btnGenerar" runat="server" CssClass="btn-generar"
                        Text="Generar Informe" OnClick="btnGenerar_Click" />
                    <asp:Button ID="Button1" runat="server"
                        Text="Limpiar Filtros" CssClass="btn-generar" OnClick="btnLimpiarFiltro2_Click" />
                </div>

                <asp:Panel ID="pnlResultados" runat="server" Visible="false" CssClass="resultados-container">

                    <div class="card card-presentes">
                        <h4>Presentes</h4>
                        <asp:Label ID="lblPresentes" runat="server"></asp:Label><br />
                        <asp:Label ID="lblPorcPresentes" runat="server"></asp:Label>
                    </div>

                    <div class="card card-ausentes">
                        <h4>Ausentes</h4>
                        <asp:Label ID="lblAusentes" runat="server"></asp:Label><br />
                        <asp:Label ID="lblPorcAusentes" runat="server"></asp:Label>
                    </div>

                    <div class="card card-total">
                        <h4>Total Turnos</h4>
                        <asp:Label ID="lblTotal" runat="server"></asp:Label>
                    </div>

                    <div style="width:100%; text-align:center; margin-top:25px;">
                        <asp:Chart ID="chartAsistencia" runat="server" Width="700px" Height="400px"></asp:Chart>
                    </div>

                </asp:Panel>

            </asp:Panel>

            <!-- =============== INFORME 2 ======================= -->

            <asp:Panel ID="pnlInforme2" runat="server" Visible="false" CssClass="report-section">

                <h3><i class="fa fa-calendar-days"></i> Turnos entre dos fechas</h3>

                <div style="margin-bottom:20px;">
                    <label>Desde:</label>
                    <asp:TextBox ID="txtFechaDesde" runat="server" TextMode="Date"></asp:TextBox>

                    <label style="margin-left:20px;">Hasta:</label>
                    <asp:TextBox ID="txtFechaHasta" runat="server" TextMode="Date"></asp:TextBox>

                    <asp:Button ID="btnFiltrarFechas" runat="server"
                        Text="Buscar" CssClass="btn-generar" OnClick="btnFiltrarFechas_Click" />
                    <asp:Button ID="BtnLimpiar" runat="server"
                       Text="Limpiar Filtros" CssClass="btn-generar" OnClick="btnLimpiarFiltro_Click" />
                </div>

                <asp:Panel ID="pnlResultadosFechas" runat="server" Visible="false">

                    <div class="resultados-container">

                        <div class="card card-total">
                            <h4>Total de turnos</h4>
                            <asp:Label ID="lblTotalFechas" runat="server"></asp:Label>
                        </div>

                        <div class="card card-presentes">
                            <h4>Presentes</h4>
                            <asp:Label ID="lblPresentesFechas" runat="server"></asp:Label><br />
                            <asp:Label ID="lblPorcPresentesFechas" runat="server"></asp:Label>
                        </div>

                        <div class="card card-ausentes">
                            <h4>Ausentes</h4>
                            <asp:Label ID="lblAusentesFechas" runat="server"></asp:Label><br />
                            <asp:Label ID="lblPorcAusentesFechas" runat="server"></asp:Label>
                        </div>

                    </div>

                    <br />
                    <br />

                    <asp:Label ID="lblCantidadTurnos" runat="server" Font-Bold="true"></asp:Label>


                    <h4 style="margin-top:30px;">
                        <i class="fa fa-list"></i> Detalle de turnos
                    </h4>
                    <table style="margin-top:20px;">
                        <tr>
                            <th>Paciente</th>
                            <th>Especialidad</th>
                            <th>Fecha</th>
                            <th>Estado</th>
                        </tr>

                        <asp:Repeater ID="repTurnosFechas" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%# ((ValueTuple<string,string,DateTime,string>)Container.DataItem).Item1 %></td>
                                    <td><%# ((ValueTuple<string,string,DateTime,string>)Container.DataItem).Item2 %></td>
                                    <td><%# ((ValueTuple<string,string,DateTime,string>)Container.DataItem).Item3.ToString("dd/MM/yyyy") %></td>
                                    <td><%# ((ValueTuple<string,string,DateTime,string>)Container.DataItem).Item4 %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </asp:Panel>

            </asp:Panel>

            <!-- =============== INFORME 3 ======================= -->

            <asp:Panel ID="pnlInforme3" runat="server" Visible="false" CssClass="report-section">

                <h3><i class="fa fa-stethoscope"></i> Promedio por Especialidad</h3>

                <asp:Panel ID="pnlEspecialidad" runat="server"></asp:Panel>

            </asp:Panel>



            <div style="text-align:center;">
                <asp:Button ID="btnVolver" runat="server" CssClass="btn-volver"
                    Text="Volver al Menú Principal" OnClick="btnVolver_Click" />
            </div>

        </div>
    </div>
</form>
</body>
</html>