<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pagina_login.aspx.cs" Inherits="proyecto_final.pagina_login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Clínica Médica - Ingreso al Sistema</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css" />
    <style>
        body {
            font-family: 'Segoe UI', sans-serif;
            background-color: white;
            height: 100vh;
            margin: 0;
            display: flex;
            justify-content: center;
            align-items: center;
        }

        .login-container {
            background-color: white;
            border-radius: 10px;
            width: 360px;
            box-shadow: 0 4px 12px rgba(0,0,0,0.25);
            overflow: hidden;
            border: 1px solid #cfd3d9;
        }

        .login-header {
            background-color: #153f8a;
            color: white;
            text-align: center;
            padding: 20px 0;
        }

        .login-header h2 {
            margin: 0;
            font-size: 22px;
        }

        .login-header h4 {
            margin: 5px 0 0;
            font-size: 15px;
            font-weight: normal;
        }

        .login-body {
            padding: 30px 25px;
        }

        .input-box {
            display: flex;
            align-items: center;
            border: 1px solid #cfd3d9;
            border-radius: 20px;
            padding: 10px;
            margin-bottom: 15px;
        }

        .input-box i {
            margin-right: 10px;
            color: #153f8a;
        }

        .input-box input, .input-box select {
            border: none;
            outline: none;
            width: 100%;
            font-size: 14px;
        }

        .btn-ingresar {
            width: 100%;
            border: none;
            border-radius: 6px;
            background-color: #153f8a;
            color: white;
            font-weight: bold;
            padding: 10px 0;
            cursor: pointer;
            transition: 0.2s;
        }

        .btn-ingresar:hover {
            background-color: #1f56b5;
        }

        .footer-text {
            text-align: center;
            font-size: 12px;
            margin-top: 15px;
            color: gray;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-container">
            <div class="login-header">
                <h2>Clínica Médica</h2>
                <h4>Ingreso al Sistema</h4>
            </div>

            <div class="login-body">

                <div class="input-box">
                    <i class="fa fa-user"></i>
                    <asp:TextBox ID="txtUsuario" runat="server" placeholder="Usuario" MaxLength="50"></asp:TextBox>
                </div>

                <div class="input-box">
                    <i class="fa fa-key"></i>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Contraseña"></asp:TextBox>
                </div>

                <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" CssClass="btn-ingresar" OnClick="btnIngresar_Click" />

                <div class="footer-text">
                </div>
            </div>
        </div>
    </form>
</body>
</html>
