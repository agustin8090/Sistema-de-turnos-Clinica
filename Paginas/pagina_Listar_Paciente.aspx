<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pagina_Listar_Paciente.aspx.cs" Inherits="proyecto_final.pagina_Listar_Paciente" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Clínica Médica - Listado Pacientes</title>
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
            max-width: 1800px;
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
        
        .botones-arriba {
    display: flex;
    flex-direction: row;
    gap: 15px;
    width: 100%;
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
    display: flex;
    justify-content: flex-end; /* lo manda a la derecha */
    margin-top: 20px;
    width: 100%;
}
        
        .btn-Buscar {
            background-color: #00913f;
            color: white;
        }
        
        .btn-Buscar:hover {
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
}

#GridViewPaciente td {
    padding: 10px;
    border: 1px solid #d9d9d9;
}

#GridViewPaciente tr:nth-child(even) {
    background-color: #f2f6fc;
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
                <i class="fa fa-search"></i>
                <h2>
                    <asp:Label ID="lblBuscarPaciente" runat="server" Text="Buscar Paciente"></asp:Label>
                </h2>
                <p>Ingrese el DNI del paciente a buscar</p>
            </div>
            
            <div class="content-area">
   
                
                <div class="form-group">
                    <asp:GridView ID="GridViewPaciente" runat="server" CssClass="tabla-pacientes" ></asp:GridView>
                    <label class="form-label">
                        <br />
                        <asp:Label ID="lblideliminarpaciente" runat="server" Text="DNI del Paciente"></asp:Label>
                    </label>
                    <div class="input-box">
                        <i class="fa fa-id-card"></i>
                        <asp:TextBox ID="txtBuscar" runat="server" placeholder="Ej: 12345678" MaxLength="8" OnTextChanged="txtBuscar_TextChanged"></asp:TextBox>
                    </div>
                </div>
                
                <div class="divider"></div>
                
                <div class="botones-arriba">
                    <asp:Button ID="btnBuscar" runat="server" CssClass="btn btn-Buscar" Text="Buscar Paciente por DNI" OnClick="btnBuscar_Click"/>

                    <asp:Label ID="Label1" runat="server"></asp:Label>
                </div>
                <div class="botones-abajo">
                 <asp:Button ID="BtnvolverMenu" runat="server" CssClass="btn btn-volver" Text="Volver al Menu" OnClick="BtnvolverMenu_Click" />
                    
                </div>
            </div>
        </div>
    </form>
</body>
</html>