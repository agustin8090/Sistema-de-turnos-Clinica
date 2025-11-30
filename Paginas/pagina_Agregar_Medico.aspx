<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pagina_Agregar_Medico.aspx.cs" Inherits="proyecto_final.pagina_Agregar_Medico" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Clínica Médica - Agregar Médico</title>
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
        .form-group {
            margin-bottom: 0;
        }
        .form-group label {
            font-weight: 600;
            display: block;
            margin-bottom: 6px;
            color: #333;
        }
        .form-group input,
        .form-group select {
            width: 100%;
            padding: 10px 12px;
            border: 1px solid #ccc;
            border-radius: 8px;
            font-size: 14px;
            outline: none;
            transition: border-color 0.3s;
            box-sizing: border-box;
        }
        .form-group input:focus,
        .form-group select:focus {
            border-color: #153f8a;
        }
        .dias-atencion-container {
            margin-bottom: 20px;
        }
        .dias-atencion-container label.section-label {
            font-weight: 600;
            display: block;
            margin-bottom: 10px;
            color: #333;
        }
        .dias-checkboxes {
            display: flex;
            flex-wrap: wrap;
            gap: 15px;
            padding: 15px;
            background-color: #f8f9fa;
            border-radius: 8px;
            border: 1px solid #e0e0e0;
        }
        .checkbox-item {
            display: flex;
            align-items: center;
            gap: 6px;
        }
        .checkbox-item input[type="checkbox"] {
            width: auto;
            cursor: pointer;
        }
        .checkbox-item label {
            margin: 0;
            cursor: pointer;
            font-weight: normal;
        }
        .horario-row {
            display: grid;
            grid-template-columns: 1fr 1fr;
            gap: 20px;
        }
        .section-divider {
            border-top: 2px solid #e0e0e0;
            margin: 30px 0;
            position: relative;
        }
        .section-divider::before {
            content: attr(data-title);
            position: absolute;
            top: -12px;
            left: 20px;
            background-color: white;
            padding: 0 10px;
            color: #666;
            font-weight: 600;
            font-size: 14px;
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
        .btn-agregar {
            background-color: #153f8a;
            color: white;
        }
        .btn-agregar:hover {
            background-color: #1f56b5;
        }
        .btn-cancelar {
            background-color: #6c757d;
            color: white;
        }
        .btn-cancelar:hover {
            background-color: #5a6268;
        }
        @media (max-width: 768px) {
            .form-row,
            .horario-row {
                grid-template-columns: 1fr;
            }
            .form-body {
                padding: 25px;
            }
            .dias-checkboxes {
                flex-direction: column;
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
                <span>Administrador</span>
            </div>
        </div>

        <div class="form-container">
            <div class="form-header">
                <i class="fa fa-user-md"></i>
                <h2>Agregar Médico</h2>
                <p>Complete los datos del nuevo médico</p>
            </div>
            <asp:Label ID="lbl_Mensaje" runat="server"
                Visible="false"
                Font-Size="15px"
                ForeColor="White"
                Style="display: block; margin: 25px auto 0 auto; text-align: center; font-weight: 600; padding: 12px 20px">
            </asp:Label>


            <div class="form-body">
                <div class="form-row">
                    <div class="form-group">
                        <asp:Label ID="lbl_Legajo" runat="server" Text="Legajo:" AssociatedControlID="txt_Legajo_Medico"></asp:Label>
                        <asp:TextBox ID="txt_Legajo_Medico" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lbl_DNI" runat="server" Text="DNI:" AssociatedControlID="txt_DNI_Medico"></asp:Label>
                        <asp:TextBox ID="txt_DNI_Medico" runat="server"></asp:TextBox>
                    </div>
                </div>

                <div class="form-row">
                    <div class="form-group">
                        <asp:Label ID="lbl_Nombre" runat="server" Text="Nombre:" AssociatedControlID="txt_Nombre_Medico"></asp:Label>
                        <asp:TextBox ID="txt_Nombre_Medico" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lbl_Apellido" runat="server" Text="Apellido:" AssociatedControlID="txt_Apellido_Medico"></asp:Label>
                        <asp:TextBox ID="txt_Apellido_Medico" runat="server"></asp:TextBox>
                    </div>
                </div>

                <div class="form-row">
                    <div class="form-group">
                        <asp:Label ID="lbl_Sexo" runat="server" Text="Sexo:" AssociatedControlID="ddl_Sexo_Medico"></asp:Label>
                        <asp:DropDownList ID="ddl_Sexo_Medico" runat="server">
                            <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                            <asp:ListItem Text="Masculino" Value="M"></asp:ListItem>
                            <asp:ListItem Text="Femenino" Value="F"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lbl_Nacionalidad" runat="server" Text="Nacionalidad:" AssociatedControlID="txt_Nacionalidad_Medico"></asp:Label>
                        <asp:TextBox ID="txt_Nacionalidad_Medico" runat="server"></asp:TextBox>
                    </div>
                </div>

                <div class="form-row">
                    <div class="form-group">
                        <asp:Label ID="lbl_FechaNacimiento" runat="server" Text="Fecha de Nacimiento:" AssociatedControlID="txt_FechaNacimiento_Medico"></asp:Label>
                        <asp:TextBox ID="txt_FechaNacimiento_Medico" runat="server" TextMode="Date"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lbl_Telefono" runat="server" Text="Teléfono:" AssociatedControlID="txt_Telefono_Medico"></asp:Label>
                        <asp:TextBox ID="txt_Telefono_Medico" runat="server"></asp:TextBox>
                    </div>
                </div>

                <div class="form-row full-width">
                    <div class="form-group">
                        <asp:Label ID="lbl_Direccion" runat="server" Text="Dirección:" AssociatedControlID="txt_Direccion_Medico"></asp:Label>
                        <asp:TextBox ID="txt_Direccion_Medico" runat="server"></asp:TextBox>
                    </div>
                </div>

                <div class="form-row">
                    <div class="form-group">
                        <asp:Label ID="lbl_Provincia" runat="server" Text="Provincia:" AssociatedControlID="ddl_Provincia_Medico"></asp:Label>
                        <asp:DropDownList
                            ID="ddl_Provincia_Medico"
                            runat="server"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="ddl_Provincia_Medico_SelectedIndexChanged">
                            <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lbl_Localidad" runat="server" Text="Localidad:" AssociatedControlID="ddl_Localidad_Medico"></asp:Label>
                        <asp:DropDownList ID="ddl_Localidad_Medico" runat="server">
                            <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="form-row full-width">
                    <div class="form-group">
                        <asp:Label ID="lbl_Mail" runat="server" Text="Correo Electrónico:" AssociatedControlID="txt_Mail_Medico"></asp:Label>
                        <asp:TextBox ID="txt_Mail_Medico" runat="server" TextMode="Email"></asp:TextBox>
                    </div>
                </div>

                <div class="section-divider" data-title="DATOS PROFESIONALES"></div>

                <div class="form-row full-width">
                    <div class="form-group">
                        <asp:Label ID="lbl_Especialidad" runat="server" Text="Especialidad:" AssociatedControlID="ddl_Especialidad_Medico"></asp:Label>
                        <asp:DropDownList ID="ddl_Especialidad_Medico" runat="server">
                            <asp:ListItem Text="-- Seleccionar Especialidad --" Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="dias-atencion-container">
                    <label class="section-label">Días de Atención:</label>
                    <div class="dias-checkboxes">
                        <div class="checkbox-item">
                            <asp:CheckBox ID="chk_Lunes" runat="server" />
                            <asp:Label ID="lbl_Lunes" runat="server" Text="Lunes" AssociatedControlID="chk_Lunes"></asp:Label>
                        </div>
                        <div class="checkbox-item">
                            <asp:CheckBox ID="chk_Martes" runat="server" />
                            <asp:Label ID="lbl_Martes" runat="server" Text="Martes" AssociatedControlID="chk_Martes"></asp:Label>
                        </div>
                        <div class="checkbox-item">
                            <asp:CheckBox ID="chk_Miercoles" runat="server" />
                            <asp:Label ID="lbl_Miercoles" runat="server" Text="Miércoles" AssociatedControlID="chk_Miercoles"></asp:Label>
                        </div>
                        <div class="checkbox-item">
                            <asp:CheckBox ID="chk_Jueves" runat="server" />
                            <asp:Label ID="lbl_Jueves" runat="server" Text="Jueves" AssociatedControlID="chk_Jueves"></asp:Label>
                        </div>
                        <div class="checkbox-item">
                            <asp:CheckBox ID="chk_Viernes" runat="server" />
                            <asp:Label ID="lbl_Viernes" runat="server" Text="Viernes" AssociatedControlID="chk_Viernes"></asp:Label>
                        </div>
                        <div class="checkbox-item">
                            <asp:CheckBox ID="chk_Sabado" runat="server" />
                            <asp:Label ID="lbl_Sabado" runat="server" Text="Sábado" AssociatedControlID="chk_Sabado"></asp:Label>
                        </div>
                    </div>
                </div>

                <div class="horario-row">
                    <div class="form-group">
                        <asp:Label ID="lbl_HoraInicio" runat="server" Text="Hora Inicio:" AssociatedControlID="txt_HoraInicio_Medico"></asp:Label>
                        <asp:TextBox ID="txt_HoraInicio_Medico" runat="server" TextMode="Time"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lbl_HoraFin" runat="server" Text="Hora Fin:" AssociatedControlID="txt_HoraFin_Medico"></asp:Label>
                        <asp:TextBox ID="txt_HoraFin_Medico" runat="server" TextMode="Time"></asp:TextBox>
                    </div>
                </div>

                <div class="section-divider" data-title="ACCESO AL SISTEMA"></div>

                <div class="form-row">
                    <div class="form-group">
                        <asp:Label ID="lbl_Usuario" runat="server" Text="Usuario:" AssociatedControlID="txt_Usuario_Medico"></asp:Label>
                        <asp:TextBox ID="txt_Usuario_Medico" runat="server"></asp:TextBox>
                    </div>
                </div>

                <div class="form-row">
                    <div class="form-group">
                        <asp:Label ID="lbl_Contraseña" runat="server" Text="Contraseña:" AssociatedControlID="txt_Contraseña_Medico"></asp:Label>
                        <asp:TextBox ID="txt_Contraseña_Medico" runat="server" TextMode="Password"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lbl_RepetirContraseña" runat="server" Text="Repetir Contraseña:" AssociatedControlID="txt_RepetirContraseña_Medico"></asp:Label>
                        <asp:TextBox ID="txt_RepetirContraseña_Medico" runat="server" TextMode="Password"></asp:TextBox>
                    </div>
                </div>

                <div class="form-actions">
                    <asp:Button ID="btn_Agregar_Medico" runat="server" CssClass="btn btn-agregar" Text="Agregar Médico" OnClick="btn_Agregar_Medico_Click" />
                    <asp:Button ID="btn_Cancelar_Medico" runat="server" CssClass="btn btn-cancelar" Text="Cancelar" OnClick="btn_Cancelar_Medico_Click" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
