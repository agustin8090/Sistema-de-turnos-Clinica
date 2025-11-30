<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pagina_ABML_paciente.aspx.cs" Inherits="proyecto_final.pagina_ABML_paciente" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Clínica Médica - Gestión de Pacientes</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css" />
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
            max-width: 900px;
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
        
        .menu-grid {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
            gap: 20px;
            margin-bottom: 30px;
        }
        
        .menu-button {
            background-color: white;
            border: 2px solid #e0e4e9;
            border-radius: 10px;
            padding: 30px 20px;
            text-align: center;
            cursor: pointer;
            transition: all 0.3s ease;
            text-decoration: none;
            display: flex;
            flex-direction: column;
            align-items: center;
            gap: 15px;
        }
        
        .menu-button:hover {
            border-color: #153f8a;
            background-color: #f8f9fb;
            transform: translateY(-2px);
            box-shadow: 0 4px 12px rgba(21, 63, 138, 0.15);
        }
        
        .menu-button i {
            font-size: 40px;
            color: #153f8a;
        }
        
        .menu-button span {
            color: #333;
            font-size: 15px;
            font-weight: 600;
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
            margin-top: 10px;
        }
        
        .btn-volver:hover {
            background-color: #5a6268;
            transform: translateX(-2px);
        }
        
        .btn-volver i {
            font-size: 16px;
        }
        
        .divider {
            height: 1px;
            background: linear-gradient(to right, transparent, #e0e4e9, transparent);
            margin: 30px 0;
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
            
            .menu-grid {
                grid-template-columns: 1fr;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="header-bar">
            <h1>
                <i class="fa fa-heartbeat"></i> Clínica Médica
            </h1>
            <div class="user-info">
                <i class="fa fa-user-circle"></i>
                <asp:Label ID="lblnombreUsuario" runat="server" Text="Usuario"></asp:Label>
            </div>
        </div>
        
        <div class="main-container">
            <div class="page-header">
                <i class="fa fa-users"></i>
                <h2>
                    <asp:Label ID="lblpaciente" runat="server" Text="Gestión de Pacientes"></asp:Label>
                </h2>
                <p>Seleccione una opción para administrar pacientes</p>
            </div>
            
            <div class="content-area">
                <div class="menu-grid">
                    <asp:Button ID="btnagregar" runat="server" CssClass="menu-button" Text='Agregar Paciente' OnClick="btnagregar_Click" />
                    
                    <asp:Button ID="btnmodificar" runat="server" CssClass="menu-button" Text='Modificar Paciente' OnClick="btnmodificar_Click" />
                    
                    <asp:Button ID="btnborrar" runat="server" CssClass="menu-button" Text='Eliminar Paciente' OnClick="btnborrar_Click" />
                    
                    <asp:Button ID="btnlistar" runat="server" CssClass="menu-button" Text='Listar Pacientes' OnClick="btnlistar_Click" />
                </div>
                
                <div class="divider"></div>
                
                <div style="text-align: center;">
                    <asp:Button ID="btnvolver" runat="server" CssClass="btn-volver" Text=' Volver al Menú Principal' OnClick="btnvolver_Click" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>