<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pagina_Modificar_Medico.aspx.cs" Inherits="proyecto_final.pagina_Modificar_Medico" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Modificar Médico - Clínica Médica</title>
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
            margin: 0;
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

        .container {
            max-width: 1250px;
            margin: 0 auto;
        }

        .card {
            background: white;
            border-radius: 8px;
            box-shadow: 0 4px 12px rgba(0,0,0,0.1);
            overflow: hidden;
            margin-bottom: 20px;
        }

        .card-header {
            background: linear-gradient(135deg, #153f8a 0%, #1f56b5 100%);
            color: white;
            padding: 30px;
            text-align: center;
        }

        .card-header-icon {
            font-size: 48px;
            margin-bottom: 10px;
        }

        .card-header h2 {
            font-size: 26px;
            margin-bottom: 5px;
            font-weight: 600;
        }

        .card-header p {
            font-size: 14px;
            opacity: 0.9;
        }

        .card-body {
            padding: 30px;
        }

        .grid-section {
            margin-bottom: 30px;
        }

        .grid-wrapper {
            overflow-x: auto;
            margin-bottom: 20px;
            border: 1px solid #ccc;
            border-radius: 8px;
            background: white;
        }

        .grid-wrapper table {
            width: 100%;
            border-collapse: collapse;
            background: #f8f9fa;
            border-radius: 6px;
            overflow: hidden;
        }

        .grid-wrapper table th {
            background-color: #2c5aa0;
            color: white;
            padding: 12px;
            text-align: left;
            font-weight: 600;
            font-size: 13px;
        }

        .grid-wrapper table td {
            padding: 12px;
            border-bottom: 1px solid #e9ecef;
            font-size: 14px;
        }

        .grid-wrapper table tr:last-child td {
            border-bottom: none;
        }

        .grid-wrapper table tr:hover {
            background-color: #e8f0fe;
        }

        .form-group {
            margin-bottom: 20px;
        }

        .form-label {
            display: block;
            margin-bottom: 8px;
            font-weight: 500;
            color: #333;
            font-size: 14px;
        }

        .form-control {
            width: 100%;
            padding: 10px 12px;
            border: 1px solid #ced4da;
            border-radius: 6px;
            font-size: 14px;
            transition: border-color 0.15s ease-in-out, box-shadow 0.15s ease-in-out;
        }

        .form-control:focus {
            outline: none;
            border-color: #2c5aa0;
            box-shadow: 0 0 0 3px rgba(44, 90, 160, 0.1);
        }

        .form-row {
            display: grid;
            grid-template-columns: 1fr 1fr;
            gap: 20px;
        }

        .buscar-dni {
            padding-top: 30px;
            padding-left: 40px;
            padding-right: 40px;
        }

        .checkbox-group {
            display: grid;
            grid-template-columns: repeat(2, 1fr);
            gap: 10px;
        }

        .checkbox-item {
            padding: 8px;
            background: #f8f9fa;
            border-radius: 4px;
        }

        .checkbox-item input[type="checkbox"] {
            margin-right: 8px;
        }

        .checkbox-item label {
            font-size: 14px;
            color: #495057;
            cursor: pointer;
        }

        .section-title {
            font-size: 16px;
            font-weight: 600;
            color: #2c5aa0;
            margin-bottom: 15px;
            padding-bottom: 10px;
            border-bottom: 2px solid #e9ecef;
        }

        .button-group {
            display: flex;
            gap: 10px;
            justify-content: center;
            margin-top: 30px;
            padding-top: 20px;
            border-top: 1px solid #e9ecef;
        }

        .btn {
            padding: 12px 30px;
            border: none;
            border-radius: 6px;
            font-size: 14px;
            font-weight: 600;
            cursor: pointer;
            transition: all 0.3s ease;
            min-width: 120px;
        }

        .btn-primary {
            background-color: #2c5aa0;
            color: white;
        }

        .btn-primary:hover {
            background-color: #1e4278;
            transform: translateY(-2px);
            box-shadow: 0 4px 12px rgba(44, 90, 160, 0.3);
        }

        .btn-secondary {
            background-color: #6c757d;
            color: white;
        }

        .btn-secondary:hover {
            background-color: #5a6268;
            transform: translateY(-2px);
            box-shadow: 0 4px 12px rgba(108, 117, 125, 0.3);
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

        @media (max-width: 768px) {
            .form-row {
                grid-template-columns: 1fr;
            }

            .checkbox-group {
                grid-template-columns: 1fr;
            }

            .card-body {
                padding: 20px;
            }

            .button-group {
                flex-direction: column;
            }

            .btn {
                width: 100%;
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

        <div class="container">
            <div class="card">
                <div class="card-header">
                    <i class="fa fa-user-md card-header-icon"></i>
                    <h2>Modificar Médico</h2>
                    <p>Seleccione el médico para modificar</p>
                </div>

                <div class="form-group buscar-dni">
                    <label class="form-label">
                        <asp:Label ID="lblBuscarDNI" runat="server" Text="Buscar por DNI"></asp:Label>
                    </label>
                    <div class="input-box">
                        <i class="fa fa-id-card"></i>
                        <asp:TextBox ID="txtBuscarDNI" runat="server"
                            placeholder="Ej: 12345678"
                            MaxLength="8"></asp:TextBox>
                    </div>
                </div>
                <div class="button-group">
                    <asp:Button ID="BtnFiltrar" runat="server" CssClass="btn btn-buscar" Text="Buscar" OnClick="btnBuscar_Click" />
                    <asp:Button ID="btnLimpiar" runat="server" CssClass="btn btn-limpiar" Text="Limpiar Filtro" OnClick="btnLimpiar_Click" />
                &nbsp;<asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </div>

                <div class="card-body">
                    <div class="grid-section">
                        <div class="grid-wrapper">
                            <asp:GridView ID="gvmedico" runat="server" 
                                CssClass="gridview-table"
                                AutoGenerateSelectButton="true"
                                DataKeyNames="Legajo"
                                OnSelectedIndexChanged="gvmedico_SelectedIndexChanged"
                                OnRowDataBound="gvmedico_RowDataBound"
                                EnableViewState="true" 
                                AllowPaging="True" 
                                OnPageIndexChanging="gvmedico_PageIndexChanging" 
                                PageSize="5">
                            </asp:GridView>
                        </div>
                    </div>

                    <div class="section-title">Información Personal</div>

                    <div class="form-group">
                        <asp:Label ID="lbldni" runat="server" Text="DNI:" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="txtdni" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="form-row">
                        <div class="form-group">
                            <asp:Label ID="lblnombre" runat="server" Text="Nombre:" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="txtnombre" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="form-group">
                            <asp:Label ID="lblapellido" runat="server" Text="Apellido:" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="txtapellido" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-row">
                        <div class="form-group">
                            <asp:Label ID="lblsexo" runat="server" Text="Sexo:" CssClass="form-label"></asp:Label>
                            <asp:DropDownList ID="ddlsexo" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>

                        <div class="form-group">
                            <asp:Label ID="lblnacionalidad" runat="server" Text="Nacionalidad:" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="txtnacionalidad" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:Label ID="lblFN" runat="server" Text="Fecha de Nacimiento:" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="txtfcn" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                    </div>

                    <div class="section-title">Información de Contacto y Ubicación</div>

                    <div class="form-group">
                        <asp:Label ID="lbldireccion" runat="server" Text="Dirección:" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="txtdireccion" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="form-row">
                        <div class="form-group">
                            <asp:Label ID="lblprovincia" runat="server" Text="Provincia:" CssClass="form-label"></asp:Label>
                            <asp:DropDownList ID="ddlprovincia" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>

                        <div class="form-group">
                            <asp:Label ID="lbllocalidad" runat="server" Text="Localidad:" CssClass="form-label"></asp:Label>
                            <asp:DropDownList ID="ddllocalidad" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:Label ID="lbltelefono" runat="server" Text="Teléfono:" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="txttelefono" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Labelmail" runat="server" Text="Mail:" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="TextBoxmail" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="section-title">Información Profesional</div>

                    <div class="form-group">
                        <asp:Label ID="lblespecialidad" runat="server" Text="Especialidad:" CssClass="form-label"></asp:Label>
                        <asp:DropDownList ID="ddlespecialidad" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>

                    <div class="form-group">
                        <asp:Label ID="lblDiasAtencion" runat="server" Text="Días de Atención:" CssClass="form-label"></asp:Label>
                        <div class="checkbox-group">
                            <div class="checkbox-item">
                                <asp:CheckBoxList ID="CheckBoxList1" runat="server"></asp:CheckBoxList>
                            </div>
                            <div class="checkbox-item">
                                <asp:CheckBoxList ID="CheckBoxList2" runat="server"></asp:CheckBoxList>
                            </div>
                            <div class="checkbox-item">
                                <asp:CheckBoxList ID="CheckBoxList3" runat="server"></asp:CheckBoxList>
                            </div>
                            <div class="checkbox-item">
                                <asp:CheckBoxList ID="CheckBoxList4" runat="server"></asp:CheckBoxList>
                            </div>
                            <div class="checkbox-item">
                                <asp:CheckBoxList ID="CheckBoxList5" runat="server"></asp:CheckBoxList>
                            </div>
                            <div class="checkbox-item">
                                <asp:CheckBoxList ID="CheckBoxList6" runat="server"></asp:CheckBoxList>
                            </div>
                        </div>
                    </div>

                    <div class="section-title">Horarios de Atención</div>

                    <div class="form-row">
                        <div class="form-group">
                            <asp:Label ID="lbliniciohorario" runat="server" Text="Horario de Inicio:" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="txthorainicio" runat="server" CssClass="form-control" TextMode="Time"></asp:TextBox>
                        </div>

                        <div class="form-group">
                            <asp:Label ID="lblbhorariotemina" runat="server" Text="Horario de Fin:" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="txthorafin" runat="server" CssClass="form-control" TextMode="Time"></asp:TextBox>
                        </div>
                    </div>

                    <div class="button-group">
                        <asp:Button ID="btnModificar" runat="server" Text="Modificar" CssClass="btn btn-primary" OnClick="btnModificar_Click" />
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnCancelar_Click" />
                        <asp:Button ID="Volver_Menu" runat="server"  Text="Volver al menu" CssClass="btn btn-secondary" OnClick="Volver_Menu_Click" />
                    </div>
                    <asp:Label ID="lblerror" runat="server" ForeColor="Red" Visible="false"></asp:Label>

                </div>
            </div>
        </div>
    </form>
</body>
</html>