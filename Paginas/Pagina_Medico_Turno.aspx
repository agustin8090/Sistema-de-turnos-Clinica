<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pagina_Medico_Turno.aspx.cs" Inherits="proyecto_final.Pagina_Medico_Turno" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Clínica Médica - Turnos del Médico</title>

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css" />

    <style>
        * { margin: 0; padding: 0; box-sizing: border-box; }

        body {
            font-family: 'Segoe UI', sans-serif;
            background-color: #f5f7fa;
            min-height: 100vh;
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
            box-shadow: 0 2px 8px rgba(0,0,0,0.1);
        }

        .header-bar h1 {
            font-size: 22px;
            font-weight: 600;
        }

        .user-info {
            display: flex;
            align-items: center;
            gap: 10px;
            font-size: 14px;
        }

        .user-info i {
            font-size: 18px;
        }

        
        .main-container {
            max-width: 1400px;
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

        .page-header i {
            font-size: 48px;
            margin-bottom: 15px;
        }

        .page-header h2 {
            font-size: 26px;
            font-weight: 600;
            margin-bottom: 5px;
        }

        .page-header p {
            font-size: 14px;
            opacity: 0.9;
        }

        .content-area {
            padding: 40px;
        }

        
        .search-filters-section {
            background-color: #f8f9fa;
            padding: 25px;
            border-radius: 8px;
            margin-bottom: 30px;
            border: 1px solid #e0e4e9;
        }

        .filters-grid {
            display: grid;
            grid-template-columns: 1fr 1fr;
            gap: 20px;
            margin-bottom: 20px;
        }

        .form-label {
            display: block;
            margin-bottom: 8px;
            font-size: 14px;
            font-weight: 600;
            color: #333;
        }

        .input-box {
            display: flex;
            align-items: center;
            background-color: white;
            border: 2px solid #e0e4e9;
            border-radius: 8px;
            padding: 12px 15px;
            transition: all 0.3s ease;
        }

        .input-box i {
            margin-right: 12px;
            color: #153f8a;
            font-size: 18px;
        }

        .input-box:focus-within {
            border-color: #153f8a;
            box-shadow: 0 0 0 3px rgba(21, 63, 138, 0.1);
        }

        .input-box input {
            border: none;
            outline: none;
            width: 100%;
            font-size: 15px;
            color: #333;
            background: transparent;
        }

        .button-group {
            display: flex;
            gap: 15px;
            margin-top: 20px;
        }

        .btn-custom {
            flex: 1;
            border: none;
            border-radius: 8px;
            padding: 14px 20px;
            font-size: 15px;
            font-weight: 600;
            cursor: pointer;
            transition: all 0.3s ease;
            display: flex;
            justify-content: center;
            align-items: center;
            gap: 8px;
        }

        .btn-buscar { background-color: #00913f; color: white; }
        .btn-buscar:hover { background-color: #007a34; transform: translateY(-2px); }

        .btn-ver { background-color: #6c757d; color: white; }
        .btn-ver:hover { background-color: #5a6268; transform: translateY(-2px); }

        .divider {
            height: 1px;
            background: linear-gradient(to right, transparent, #e0e4e9, transparent);
            margin: 30px 0;
        }

        
        .gridview-container { 
            margin-top: 30px; 
            overflow-x: auto; 
            width: 100%;
        }

        #gvTurnos {
            width: 100% !important;
        }

        #gvTurnos th {
            background-color: #153f8a;
            color: white;
            padding: 12px 10px;
            font-size: 14px;
            font-weight: 600;
        }

        #gvTurnos td {
            padding: 12px 10px;
            border: 1px solid #d9d9d9;
            font-size: 14px;
        }

        #gvTurnos tr:nth-child(even) {
            background-color: #f2f6fc;
        }

        #gvTurnos tr:hover {
            background-color: #e8f0fe;
        }
    </style>
</head>

<body>

<form id="form1" runat="server">

    
    <div class="header-bar">
        <h1><i class="fa fa-heartbeat"></i> Clínica Médica</h1>

        <div class="user-info">
            <i class="fa fa-user-circle"></i>
            <asp:Label ID="lblMedico" runat="server"></asp:Label>
        </div>
    </div>

    <div class="main-container">

       
        <div class="page-header">
            <i class="fa fa-calendar-check"></i>
            <h2>Turnos del Médico</h2>
            <p>Seleccione una fecha o vea todos los turnos asignados</p>
        </div>

        <div class="content-area">

            
            <div class="search-filters-section">

                <div class="filters-grid">

                    <div>
                        <label class="form-label">Fecha del turno</label>
                        <div class="input-box">
                            <i class="fa fa-calendar"></i>
                            <asp:TextBox ID="txtFecha" runat="server" TextMode="Date"></asp:TextBox>
                        </div>
                    </div>

                </div>

                <div class="button-group">
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn-custom btn-buscar" OnClick="btnBuscar_Click" />
                    <asp:Button ID="btnVerTodos" runat="server" Text="Ver todos" CssClass="btn-custom btn-ver" OnClick="btnVerTodos_Click" />
                </div>

            </div>

            <div class="divider"></div>

            
            <div class="gridview-container">
                <asp:GridView 
                    ID="gvTurnos"
                    runat="server"
                    AutoGenerateColumns="False"
                    CssClass="table"
                    AllowPaging="True"
                    PageSize="10"
                    OnPageIndexChanging="gvTurnos_PageIndexChanging">

                    <Columns>
                        <asp:BoundField DataField="Paciente" HeaderText="Paciente" />
                        <asp:BoundField DataField="DniPaciente" HeaderText="DNI" />
                        <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="Hora" HeaderText="Hora" />
                    </Columns>

                </asp:GridView>
            </div>

        </div>

    </div>

</form>

</body>
</html>