<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pagina_asignar_turnos.aspx.cs" Inherits="proyecto_final.Pagina_asignar_turnos" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Clínica Médica - Asignación de Turnos</title>
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
            padding: 1px 30px;
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
        }

        .form-container {
            max-width: 900px;
            margin: 0 auto;
            background-color: white;
            border-radius: 10px;
            box-shadow: 0 4px 12px rgba(0,0,0,0.1);
            overflow: hidden;
        }

        .form-header {
            background: linear-gradient(135deg, #153f8a 0%, #1f56b5 100%);
            color: white;
            text-align: center;
            padding: 30px 20px;
        }

        .form-header i {
            font-size: 48px;
            margin-bottom: 15px;
        }

        .form-header h2 {
            font-size: 26px;
            font-weight: 600;
            margin-bottom: 5px;
        }

        .form-body {
            padding: 40px 50px;
        }

        .form-row {
            display: grid;
            grid-template-columns: 1fr 1fr;
            gap: 20px;
            margin-bottom: 20px;
        }

        .form-row.full-width {
            grid-template-columns: 1fr;
        }

        .form-group label {
            font-weight: 600;
            display: block;
            margin-bottom: 6px;
            color: #333;
        }

        .form-group select,
        .form-group input {
            width: 100%;
            padding: 10px 12px;
            border: 1px solid #ccc;
            border-radius: 8px;
            font-size: 14px;
            outline: none;
            transition: border-color 0.3s;
            box-sizing: border-box;
        }

        .form-group select:focus,
        .form-group input:focus {
            border-color: #153f8a;
        }

        .form-actions {
            display: flex;
            justify-content: center;
            gap: 20px;
            margin-top: 30px;
        }

        .btn {
            border: none;
            border-radius: 8px;
            padding: 12px 40px;
            font-size: 15px;
            font-weight: 600;
            cursor: pointer;
            transition: all 0.3s ease;
            display: inline-flex;
            align-items: center;
            gap: 8px;
        }

        .btn i {
            font-size: 16px;
        }

        .btn-asignar {
            background-color: #153f8a;
            color: white;
        }

        .btn-asignar:hover {
            background-color: #1f56b5;
        }

        .btn-cancelar {
            background-color: #6c757d;
            color: white;
        }

        .btn-cancelar:hover {
            background-color: #5a6268;
        }

        .message-box {
            text-align: center;
            margin-top: 25px;
            font-weight: 600;
            border-radius: 8px;
            padding: 12px;
            display: none;
        }

        .message-success {
            background-color: #d1e7dd;
            color: #0f5132;
            display: block;
        }

        .message-error {
            background-color: #f8d7da;
            color: #842029;
            display: block;
        }

        @media (max-width: 768px) {
            .form-row {
                grid-template-columns: 1fr;
            }
            .form-body {
                padding: 25px;
            }
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
        <div class="form-container">
            <div class="form-header">
                <i class="fa fa-calendar-check"></i>
                <h2>Asignación de Turnos</h2>
                <p>Seleccione los datos necesarios para crear un turno</p>
            </div>

            <div class="form-body">
                <div class="form-row">
                    <div class="form-group">
                        <asp:Label ID="lblEspecialidad" runat="server" Text="Especialidad:" AssociatedControlID="ddlEspecialidad"></asp:Label>
                        <asp:DropDownList ID="ddlEspecialidad" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEspecialidad_SelectedIndexChanged">
                            <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblMedico" runat="server" Text="Médico:" AssociatedControlID="ddlMedico"></asp:Label>
                        <asp:DropDownList ID="ddlMedico" runat="server">
                            <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="form-row">
                    <div class="form-group">
                        <asp:Label ID="lblFecha" runat="server" Text="Fecha:" AssociatedControlID="txtFecha"></asp:Label>
                        <asp:TextBox ID="txtFecha" runat="server" TextMode="Date"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblHora" runat="server" Text="Hora:" AssociatedControlID="ddlHora"></asp:Label>
<asp:DropDownList ID="ddlHora" runat="server">
    <asp:ListItem Text="-- Seleccionar Hora --" Value=""></asp:ListItem>
</asp:DropDownList>                    

                    </div>
                </div>

                <div class="form-row full-width">
                    <div class="form-group">
                        <asp:Label ID="lblPaciente" runat="server" Text="Paciente:" AssociatedControlID="ddlPaciente"></asp:Label>
                        <asp:DropDownList ID="ddlPaciente" runat="server">
                            <asp:ListItem Text="-- Seleccionar Paciente --" Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                
                <asp:RequiredFieldValidator ID="reqEspecialidad" runat="server" ControlToValidate="ddlEspecialidad" ErrorMessage="Seleccione una especialidad" Display="Dynamic" ForeColor="Red" ValidationGroup="grupo1"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="reqMedico" runat="server" ControlToValidate="ddlMedico" ErrorMessage="Seleccione un médico" Display="Dynamic" ForeColor="Red" ValidationGroup="grupo1"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="reqFecha" runat="server" ControlToValidate="txtFecha" ErrorMessage="Seleccione una fecha" Display="Dynamic" ForeColor="Red" ValidationGroup="grupo1"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="reqHora" runat="server" ControlToValidate="ddlHora" ErrorMessage="Seleccione un horario" Display="Dynamic" ForeColor="Red" ValidationGroup="grupo1"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="reqPaciente" runat="server" ControlToValidate="ddlPaciente" ErrorMessage="Seleccione un paciente" Display="Dynamic" ForeColor="Red" ValidationGroup="grupo1"></asp:RequiredFieldValidator>

                
                <div class="form-actions">
                    <asp:Button ID="btnAsignar" runat="server" CssClass="btn btn-asignar" Text="Asignar Turno" OnClick="btnAsignar_Click" ValidationGroup="grupo1" />
                    <asp:Button ID="btnCancelar" runat="server" CssClass="btn btn-cancelar" Text="Cancelar" OnClick="btnCancelar_Click" />
                    <br />
                    <br />
                    <asp:Label ID="lblVal" runat="server"></asp:Label>
                </div>
            </div>
        </div>
    </form>
</body>
</html>