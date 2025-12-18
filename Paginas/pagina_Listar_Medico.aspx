<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pagina_Listar_Medico.aspx.cs" Inherits="proyecto_final.pagina_Listar_Medico" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Clínica Médica - Listado Médicos</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css" />
    <link rel="icon" href="../imagen/logoUTN.png" type="image/png"/>
    <style type="text/css">
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }
        
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
        
        .clinic-name h1 {
            font-size: 22px;
            margin: 0;
        }

        .header-right {
            display: flex;
            align-items: center;
            gap: 15px;
        }

        .user-info {
            display: flex;
            align-items: center;
            gap: 6px;
            font-weight: 600;
        }
        .home-btn {
            background-color: white; 
            border: none;
            color: #1B54B6;
            padding: 8px 14px;
            border-radius: 6px;
            font-weight: 600;
            transition: 0.2s;
        }

        .home-btn:hover {
            transform: scale(1.03);
            color: #0F2F66;
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
        
        .form-group {
            margin-bottom: 0;
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
        
        .input-box:focus-within {
            border-color: #153f8a;
            box-shadow: 0 0 0 3px rgba(21, 63, 138, 0.1);
        }
        
        .input-box i {
            margin-right: 12px;
            color: #153f8a;
            font-size: 18px;
        }
        
        .input-box input,
        .input-box select {
            border: none;
            outline: none;
            width: 100%;
            font-size: 15px;
            color: #333;
            background: transparent;
        }
        
        .input-box input::placeholder {
            color: #999;
        }
        
        .button-group {
            display: flex;
            gap: 15px;
            margin-top: 20px;
        }
        
        .btn {
            flex: 1;
            border: none;
            border-radius: 8px;
            padding: 14px 20px;
            font-size: 15px;
            font-weight: 600;
            cursor: pointer;
            transition: all 0.3s ease;
            display: flex;
            align-items: center;
            justify-content: center;
            gap: 8px;
        }
        
        .btn i {
            font-size: 16px;
        }
        
        .btn-buscar {
            background-color: #00913f;
            color: white;
        }
        
        .btn-buscar:hover {
            background-color: #007a34;
            transform: translateY(-2px);
            box-shadow: 0 4px 12px rgba(0, 145, 63, 0.3);
        }
        
        .btn-limpiar {
            background-color: #ffc107;
            color: #333;
        }
        
        .btn-limpiar:hover {
            background-color: #e0a800;
            transform: translateY(-2px);
            box-shadow: 0 4px 12px rgba(255, 193, 7, 0.3);
        }
        
        .btn-volver {
            background-color: #6c757d;
            color: white;
        }
        
        .btn-volver:hover {
            background-color: #5a6268;
            transform: translateY(-2px);
            box-shadow: 0 4px 12px rgba(108, 117, 125, 0.3);
        }
        
        .divider {
            height: 1px;
            background: linear-gradient(to right, transparent, #e0e4e9, transparent);
            margin: 30px 0;
        }

        .gridview-container {
            overflow-x: auto;
            margin-top: 30px;
        }
        
        #GridViewMedico {
            width: 100%;
            max-width: 100%;
            display: table;
            border-collapse: collapse;
        }

        #GridViewMedico table {
            width: 100%;
            border-collapse: collapse;
        }

        #GridViewMedico th {
            background-color: #153f8a;
            color: white;
            padding: 12px 10px;
            font-weight: 600;
            text-align: left;
            font-size: 14px;
            white-space: nowrap;
        }

        #GridViewMedico td {
            padding: 12px 10px;
            border: 1px solid #d9d9d9;
            font-size: 14px;
        }

        #GridViewMedico tr:nth-child(even) {
            background-color: #f2f6fc;
        }
        
        #GridViewMedico tr:hover {
            background-color: #e8f0fe;
        }
        
        .btn-action {
            padding: 6px 12px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            font-size: 13px;
            font-weight: 600;
            transition: all 0.2s ease;
            margin: 0 3px;
        }
        
        .btn-editar {
            background-color: #007bff;
            color: white;
        }
        
        .btn-editar:hover {
            background-color: #0056b3;
        }
        
        .btn-eliminar {
            background-color: #dc3545;
            color: white;
        }
        
        .btn-eliminar:hover {
            background-color: #c82333;
        }
        
        .info-box {
            background-color: #d1ecf1;
            border-left: 4px solid #0c5460;
            padding: 15px;
            border-radius: 5px;
            margin-bottom: 20px;
            display: flex;
            align-items: center;
            gap: 12px;
        }
        
        .info-box i {
            font-size: 24px;
            color: #0c5460;
        }
        
        .info-box p {
            margin: 0;
            color: #0c5460;
            font-size: 14px;
        }
        
        @media (max-width: 768px) {
            .header-bar {
                flex-direction: column;
                gap: 10px;
                text-align: center;
            }
            
            .content-area {
                padding: 25px;
            }
            
            .filters-grid {
                grid-template-columns: 1fr;
            }
            
            .button-group {
                flex-direction: column;
            }
            
            #GridViewMedico {
                font-size: 12px;
            }
            
            #GridViewMedico th,
            #GridViewMedico td {
                padding: 8px 5px;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="header-bar">
            <div class="clinic-name">
                <h1><i class="fa fa-heartbeat"></i> Clínica Médica</h1>
            </div>

            <div class="header-right">
                <div class="user-info">
                    <i class="fa fa-user-circle"></i>
                    <asp:Label ID="lblAdmin" runat="server"></asp:Label>
                </div>

                <asp:Button ID="Button1" runat="server"
    	            Text="Home"
    	            CssClass="home-btn"
    	            OnClick="btnHome_Click" />
            </div>
        </div>
        
        <div class="main-container">
            <div class="page-header">
                <i class="fa fa-user-md"></i>
                <h2>Listado de Médicos</h2>
                <p>Busque y administre la información de los médicos</p>
            </div>
            
            <div class="content-area">
                
                <div class="info-box">
                    <i class="fa fa-info-circle"></i>
                    <p>Seleccione un criterio a la vez</p>
                </div>
                
                <div class="search-filters-section">
                    <div class="filters-grid">
                        <div class="form-group">
                            <label class="form-label">
                                <asp:Label ID="lblBuscarDNI" runat="server" Text="Buscar por DNI"></asp:Label>
                            </label>
                            <div class="input-box">
                                <i class="fa fa-id-card"></i>
                                <asp:TextBox ID="txtBuscarDNI" runat="server" placeholder="Ej: 12345678" MaxLength="8"></asp:TextBox>
                            </div>
                        </div>
                        
                        <div class="form-group">
                            <label class="form-label">
                                <asp:Label ID="lblBuscarLegajo" runat="server" Text="Buscar por Legajo"></asp:Label>
                            </label>
                            <div class="input-box">
                                <i class="fa fa-hashtag"></i>
                                <asp:TextBox ID="txtBuscarLegajo" runat="server" placeholder="Ej: LEG001"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="form-label">
                            <asp:Label ID="lblFiltroEstado" runat="server" Text="Filtrar por Estado"></asp:Label>
                        </label>
                        <div class="input-box">
                            <i class="fa fa-user-check"></i>
                            <asp:DropDownList ID="ddlEstado" runat="server">
                                <asp:ListItem Value="-1" Text="-- Todos --"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Activo / Alta"></asp:ListItem>
                                <asp:ListItem Value="0" Text="Inactivo / Baja"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    
                    <div class="form-group">
                        <label class="form-label">
                            <asp:Label ID="lblFiltroEspecialidad" runat="server" Text="Filtrar por Especialidad"></asp:Label>
                        </label>
                        <div class="input-box">
                            <i class="fa fa-stethoscope"></i>
                            <asp:DropDownList ID="ddlEspecialidad" runat="server" AppendDataBoundItems="True">
                            </asp:DropDownList>
                        </div>
                    </div>
                    
                    <div class="form-group" style="margin-bottom: 20px;">
                        <label class="form-label">
                            <asp:Label ID="lblFiltroDias" runat="server" Text="Filtrar por Días de Atención"></asp:Label>
                        </label>
                        <div style="background-color: white; border: 2px solid #e0e4e9; border-radius: 8px; padding: 12px 15px;">
                            <asp:CheckBoxList ID="cblDiasAtencion" runat="server" RepeatDirection="Horizontal" RepeatColumns="7" CellPadding="10" CellSpacing="10" ForeColor="#333">
                                <asp:ListItem Value="Lunes">Lunes</asp:ListItem>
                                <asp:ListItem Value="Martes">Martes</asp:ListItem>
                                <asp:ListItem Value="Miércoles">Miércoles</asp:ListItem>
                                <asp:ListItem Value="Jueves">Jueves</asp:ListItem>
                                <asp:ListItem Value="Viernes">Viernes</asp:ListItem>
                                <asp:ListItem Value="Sábado">Sábado</asp:ListItem> 
                            </asp:CheckBoxList>
                        </div>
                    </div>

                    <div class="button-group">
                        <asp:Button ID="btnBuscar" runat="server" CssClass="btn btn-buscar" Text="Buscar" OnClick="btnBuscar_Click" />
                        <asp:Button ID="btnLimpiar" runat="server" CssClass="btn btn-limpiar" Text="Limpiar Filtros" OnClick="btnLimpiar_Click" />
                        <asp:Button ID="btnVolver" runat="server" CssClass="btn btn-volver" Text="Volver" OnClick="btnVolver_Click" />
                    &nbsp;<asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    </div>
                </div>
                
                <div class="divider"></div>
          
                <div class="gridview-container">
                    <asp:GridView ID="GridViewMedico" runat="server" CssClass="tabla-medicos" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="GridViewMedico_PageIndexChanging" PageSize="5">
                        <Columns>
                            <asp:BoundField DataField="legajo" HeaderText="Legajo" />
                            <asp:BoundField DataField="dni" HeaderText="DNI" />
                            <asp:BoundField DataField="apellido" HeaderText="Apellido" />
                            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                            <asp:BoundField DataField="telefono" HeaderText="Teléfono" />
                            <asp:BoundField DataField="email" HeaderText="Email" />
                            <asp:BoundField DataField="nombre_especialidad" HeaderText="Especialidad" />
                            <asp:BoundField DataField="dias_atencion" HeaderText="Días Atención" />

                            <asp:BoundField DataField="hora_inicio" HeaderText="Desde"
                                HtmlEncode="false" NullDisplayText="-" />

                            <asp:BoundField DataField="hora_fin" HeaderText="Hasta"
                                HtmlEncode="false" NullDisplayText="-" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </form>
</body>
</html>