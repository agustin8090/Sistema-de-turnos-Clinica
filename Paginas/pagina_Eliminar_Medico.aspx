<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pagina_Eliminar_Medico.aspx.cs" Inherits="proyecto_final.pagina_Eliminar_Medico" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Clínica Médica - Eliminar Medico</title>
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
            max-width: 700px;
            margin: 0 auto;
            background-color: white;
            border-radius: 10px;
            box-shadow: 0 4px 12px rgba(0,0,0,0.1);
            overflow: hidden;
        }
        
        .page-header {
            background: linear-gradient(135deg, #dc3545 0%, #c82333 100%);
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
        
        .warning-box {
            background-color: #fff3cd;
            border: 2px solid #ffc107;
            border-radius: 8px;
            padding: 15px 20px;
            margin-bottom: 30px;
            display: flex;
            align-items: center;
            gap: 15px;
        }
        
        .warning-box i {
            font-size: 28px;
            color: #856404;
        }
        
        .warning-box p {
            margin: 0;
            color: #856404;
            font-size: 14px;
            font-weight: 500;
        }
        
        .form-group {
            margin-bottom: 25px;
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
        
        .input-box input {
            border: none;
            outline: none;
            width: 100%;
            font-size: 15px;
            color: #333;
        }
        
        .input-box input::placeholder {
            color: #999;
        }
        
        .button-group {
            display: flex;
            gap: 15px;
            margin-top: 30px;
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
        
        .btn-eliminar {
            background-color: #dc3545;
            color: white;
        }
        
        .btn-eliminar:hover {
            background-color: #c82333;
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
        
        @media (max-width: 768px) {
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
            <h1>
                <i class="fa fa-heartbeat"></i> Clínica Médica
            </h1>
            <div class="user-info">
                <i class="fa fa-user-circle"></i>
                <asp:Label ID="lblnombreusuario" runat="server" Text="Usuario"></asp:Label>
            </div>
        </div>
        
        <div class="main-container">
           <div class="page-header">
    <i class="fa fa-user-md"></i>
    <h2>
        <asp:Label ID="lbleliminarMedico" runat="server" Text="Eliminar Médico"></asp:Label>
    </h2>
    <p>Ingrese el Legajo del médico a eliminar</p>
</div>
            
            <div class="content-area">
                <div class="warning-box">
                    <i class="fa fa-exclamation-triangle"></i>
                    <p>¡Advertencia! Esta acción no se puede deshacer. Asegúrese de ingresar el Legajo correcto antes de continuar.</p>
                </div>
                
                <div class="form-group">
                    <label class="form-label">
                        <asp:Label ID="lblideliminarMedico" runat="server" Text="Legajo del Medico"></asp:Label>
                    </label>
                    <div class="input-box">
                        
                        <asp:TextBox ID="txtelimnar" runat="server" placeholder="Ej: 12345678" MaxLength="8"></asp:TextBox>
                    </div>
                </div>
                
                <div class="divider">
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </div>
                
                <div class="button-group">
                    <asp:Button ID="btneliminar" runat="server" CssClass="btn btn-eliminar" Text="Eliminar Medico" OnClick="btneliminar_Click" />
                    <asp:Button ID="volver" runat="server" CssClass="btn btn-volver" Text="Volver" OnClick="volver_Click" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
