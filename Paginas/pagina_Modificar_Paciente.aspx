<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pagina_Modificar_Paciente.aspx.cs" Inherits="proyecto_final.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Clínica Médica - Modificar Paciente</title>
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
        max-width: 1350px;
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

    .form-group {
        margin-bottom: 20px;
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
    }

    .form-group input:focus,
    .form-group select:focus {
        border-color: #153f8a;
    }

    .form-actions {
        display: flex;
        justify-content: center;
        gap: 20px;
        margin-top: 30px;
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
        width: 100%;
        min-width: 900px;   
    }
    #GridViewPaciente {
    width: 100% ;
    max-width: 100%;
    display: block;
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

    .btn-Modificar {
        background-color: #153f8a;
        color: white;
    }

    .btn-Modificar:hover {
        background-color: #1f56b5;
    }

    .btn-cancelar {
        background-color: #6c757d;
        color: white;
    }

    .btn-cancelar:hover {
        background-color: #5a6268;
    }

    @media (max-width: 600px) {
        .form-body {
            padding: 25px;
        }
    }
    .mensaje-sistema {
        display: block;
        padding: 12px;
        margin-bottom: 20px;
        border-radius: 6px;
        font-weight: 600;
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
            <i class="fa fa-user-plus"></i>
            <h2>
                Modificar Paciente</h2>
            <p>Seleccione el paciente para modificar</p>
        </div>
        <asp:Label ID="lblMensajeModificacion" runat="server" CssClass="mensaje-sistema"></asp:Label>

        <div class="form-body">
            <div class="grid-container">
                <asp:GridView ID="GridViewPaciente" runat="server" CssClass="tabla-pacientes" CellPadding="4" ForeColor="#333333" GridLines="None" Width="903px" Height="155px" DataKeyNames="IdPaciente" OnSelectedIndexChanged="GridViewPaciente_SelectedIndexChanged"
                    AutoGenerateSelectButton="True" >
                    <AlternatingRowStyle BackColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
                <br />
            </div>
            <div class="form-group">
                <asp:Label ID="Label1" runat="server"></asp:Label>
                <asp:Label ID="nombre" runat="server" Text="Nombre:" AssociatedControlID="Text_Nombre_Paciente"></asp:Label>
                <asp:TextBox ID="Text_Nombre_Paciente" runat="server" Width="1170px"></asp:TextBox>
            </div>

            <div class="form-group">
                <asp:Label ID="apellido" runat="server" Text="Apellido:" AssociatedControlID="Text_Apellido_Paciente"></asp:Label>
                <asp:TextBox ID="Text_Apellido_Paciente" runat="server"></asp:TextBox>
            </div>

            <div class="form-group">
                <asp:Label ID="dni" runat="server" Text="DNI:" AssociatedControlID="Text_DNI_Paciente"></asp:Label>
                <asp:TextBox ID="Text_DNI_Paciente" runat="server"></asp:TextBox>
            </div>

            <div class="form-group">
                <asp:Label ID="sexo" runat="server" Text="Sexo:" AssociatedControlID="ddl_Sexo_Paciente"></asp:Label>
                <asp:DropDownList ID="ddl_Sexo_Paciente" runat="server" Width="1202px"></asp:DropDownList>
            </div>

            <div class="form-group">
                <asp:Label ID="Nacionalidad" runat="server" Text="Nacionalidad:" AssociatedControlID="Text_Nacionalidad_Paciente"></asp:Label>
                <asp:TextBox ID="Text_Nacionalidad_Paciente" runat="server"></asp:TextBox>
            </div>

            <div class="form-group">
                <asp:Label ID="fecha_Nacimiento" runat="server" Text="Fecha de Nacimiento:" AssociatedControlID="Text_FechaNacimiento_Paciente"></asp:Label>
                <asp:TextBox ID="Text_FechaNacimiento_Paciente" runat="server" TextMode="Date"></asp:TextBox>
            </div>

            <div class="form-group">
                <asp:Label ID="direccion" runat="server" Text="Dirección:" AssociatedControlID="Text_Direccion_Paciente"></asp:Label>
                <asp:TextBox ID="Text_Direccion_Paciente" runat="server"></asp:TextBox>
            </div>

            <div class="form-group">
                <asp:Label ID="provincia" runat="server" Text="Provincia:" AssociatedControlID="ddl_Provincia_Paciente" ></asp:Label>
                <asp:DropDownList ID="ddl_Provincia_Paciente" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_Provincia_Paciente_SelectedIndexChanged" Width="1202px" ></asp:DropDownList>
            </div>

            <div class="form-group">
                <asp:Label ID="localidad" runat="server" Text="Localidad:" AssociatedControlID="ddl_Localidad_Paciente"></asp:Label>
                <asp:DropDownList ID="ddl_Localidad_Paciente" runat="server" Width="1204px"></asp:DropDownList>
            </div>

            <div class="form-group">
                <asp:Label ID="mail" runat="server" Text="Mail:" AssociatedControlID="Text_Mail_Paciente"></asp:Label>
                <asp:TextBox ID="Text_Mail_Paciente" runat="server" TextMode="Email"></asp:TextBox>
            </div>

            <div class="form-group">
                <asp:Label ID="telefono" runat="server" Text="Teléfono:" AssociatedControlID="Text_Telefono_Paciente"></asp:Label>
                <asp:TextBox ID="Text_Telefono_Paciente" runat="server"></asp:TextBox>
            </div>

            <asp:DropDownList ID="ddl_Estado_Paciente" runat="server" class="form-group">
                <asp:ListItem Text="Activo" Value="1"></asp:ListItem>
                <asp:ListItem Text="Inactivo" Value="0"></asp:ListItem>
            </asp:DropDownList>

            <div class="form-actions">
                <asp:Button ID="Modificar_Paciente" runat="server" CssClass="btn btn-Modificar" Text="Modificar" OnClick="Modificar_Paciente_Click" />
                <asp:Button ID="Cancelar_Paciente" runat="server" CssClass="btn btn-cancelar" Text="Cancelar" />
                <asp:Button ID="Volver_Menu" runat="server" CssClass="btn btn-cancelar" Text="Volver al menu" OnClick="Volver_Menu_Click" />
            </div>
        </div>
    </div>
</form>

</body>
</html>

