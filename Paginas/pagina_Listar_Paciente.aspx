<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pagina_Listar_Paciente.aspx.cs" Inherits="proyecto_final.pagina_Listar_Paciente" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Clínica Médica - Listado Pacientes</title>
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
            background: #153f8a;
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
            grid-template-columns: repeat(3, 1fr); 
            gap: 20px; 
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
        
        .input-box input, .input-box select {
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
        
        .botones-arriba, .botones-abajo { 
            display: flex;
            gap: 15px;
            width: 100%; 
        }

        .botones-arriba {
            margin-bottom: 20px;
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
        .botones-abajo {
            justify-content: flex-end;
        }
        
        .btn-Buscar {
            background-color: #00913f;
            color: white;
        }
        
        .btn-Buscar:hover {
            background-color: #007a34;
            transform: translateY(-2px);
            box-shadow: 0 4px 12px rgba(220, 53, 69, 0.3);
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

         .grid-container {
             max-width: 100%;
             overflow-x: auto;
             margin-bottom: 25px;
             border: 1px solid #ccc;
             border-radius: 8px;
             background: white;
         }

        #GridViewPaciente {
            width: 100% ;
            max-width: 100%;
            display: table;
        }

        #GridViewPaciente table {
            width: 100% ;
            border-collapse: collapse;
        }

        #GridViewPaciente th {
            background-color: #153f8a;
            color: white;
            padding: 10px;
            font-weight: 600;
            text-align: left;
            font-size: 14px; 
        }

        #GridViewPaciente td {
            padding: 10px;
            border: 1px solid #d9d9d9;
            font-size: 14px;
        }

        #GridViewPaciente tr:nth-child(even) {
            background-color: #f2f6fc;
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

        @media (max-width: 760px) {
            .header-bar {
                flex-direction: column;
                gap: 10px;
                text-align: center;
            }
            
            .content-area {
                padding: 25px;
            }
            
            .button-group {
                flex-direction: column;
            }
            
            .warning-box {
                flex-direction: column;
                text-align: center;
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
                <i class="fa fa-search"></i>
                <h2>
                    <asp:Label ID="lblBuscarPaciente" runat="server" Text="Buscar Paciente"></asp:Label>
                </h2>
                <p>Busque y filtre la información de los pacientes</p>
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
                                <asp:Label ID="lblBuscarDni" runat="server" Text="Buscar por DNI"></asp:Label>
                            </label>
                            <div class="input-box">
                                <i class="fa fa-id-card"></i>
                                <asp:TextBox ID="txtBuscarDNI" runat="server" placeholder="Ej: 12345678" MaxLength="8"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="form-label">
                                <asp:Label ID="lblSexo" runat="server" Text="Filtrar por Sexo"></asp:Label>
                            </label>
                            <div class="input-box">
                                <i class="fa fa-venus-mars"></i>
                                <asp:DropDownList ID="ddlSexo" runat="server">
                                    <asp:ListItem Value="" Text="-- Todos --"></asp:ListItem>
                                    <asp:ListItem Value="M" Text="Masculino"></asp:ListItem>
                                    <asp:ListItem Value="F" Text="Femenino"></asp:ListItem>
                                    <asp:ListItem Value="O" Text="Otro"></asp:ListItem>
                                </asp:DropDownList>
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
        
                    </div>
    
                    <div class="botones-arriba" style="margin-top: 20px;">
                        <asp:Button ID="btnAplicarFiltros" runat="server" CssClass="btn btn-Buscar" Text="Aplicar Filtro" OnClick="btnAplicarFiltros_Click"/>
                        <asp:Button ID="btnLimpiarFiltros" runat="server" CssClass="btn btn-volver" Text="Limpiar Filtros" OnClick="btnLimpiarFiltros_Click"/>
                        <asp:Label ID="Label1" runat="server"></asp:Label>
                    </div>
                </div>

                <div class="divider"></div>

                <div class="grid-container">
                    <asp:GridView ID="GridViewPaciente" runat="server" CssClass="tabla-pacientes" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="GridViewPaciente_PageIndexChanging" PageSize="5" >
                         <Columns>
                             <asp:BoundField DataField="DniPaciente" HeaderText="DNI" />
                             <asp:BoundField DataField="NombrePaciente" HeaderText="Nombre" />
                             <asp:BoundField DataField="ApellidoPaciente" HeaderText="Apellido" />
                             <asp:BoundField DataField="SexoPaciente" HeaderText="Sexo" />
                             <asp:BoundField DataField="NacionalidadPac" HeaderText="Nacionalidad" />
                             <asp:BoundField DataField="TelefonoPac" HeaderText="Teléfono" />
                             <asp:BoundField DataField="EmailPac" HeaderText="Email" />
                             <asp:TemplateField HeaderText="Estado">
                                 <ItemTemplate>
                                     <asp:Label runat="server" Text='<%# Eval("Estado").Equals(true) ? "ACTIVO" : "BAJA" %>' 
                                                ForeColor='<%# Eval("Estado").Equals(true) ? System.Drawing.Color.Green : System.Drawing.Color.Red %>'></asp:Label>
                                 </ItemTemplate>
                             </asp:TemplateField>
                         </Columns>
                    </asp:GridView>
                </div>

                <div class="botones-abajo">
                    <asp:Button ID="BtnvolverMenu" runat="server" CssClass="btn btn-volver" Text="Volver al Menu" OnClick="BtnvolverMenu_Click" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>