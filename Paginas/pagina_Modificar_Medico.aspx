<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pagina_Modificar_Medico.aspx.cs" Inherits="proyecto_final.pagina_Modificar_Medico" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Modificar Médico - Clínica Médica</title>
    <style type="text/css">
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }

        body {
            font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Oxygen, Ubuntu, Cantarell, sans-serif;
            background-color: #f0f2f5;
            padding: 20px;
        }

        .header {
            background-color: white;
            padding: 15px 30px;
            margin-bottom: 20px;
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
            border-radius: 8px;
        }

        .header-title {
            font-size: 18px;
            font-weight: 600;
            color: #333;
            margin-bottom: 5px;
        }

        .header-title::before {
            content: "♥ ";
            color: #e74c3c;
        }

        .header-subtitle {
            font-size: 14px;
            color: #666;
        }

        .header-subtitle::before {
            content: "👤 ";
        }

        .container {
            max-width: 800px;
            margin: 0 auto;
        }

        .card {
            background: white;
            border-radius: 8px;
            box-shadow: 0 2px 8px rgba(0,0,0,0.1);
            overflow: hidden;
            margin-bottom: 20px;
        }

        .card-header {
            background: linear-gradient(135deg, #2c5aa0 0%, #1e4278 100%);
            color: white;
            padding: 30px;
            text-align: center;
        }

        .card-header-icon {
            font-size: 48px;
            margin-bottom: 10px;
        }

        .card-header h2 {
            font-size: 24px;
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
        <div class="header">
            <div class="header-title">Clínica Médica</div>
            <div class="header-subtitle">Administrador</div>
        </div>

        <div class="container">
            <div class="card">
                <div class="card-header">
                    <div class="card-header-icon">👨‍⚕️</div>
                    <h2>Modificar Médico</h2>
                    <p>Seleccione el médico para modificar</p>
                </div>

                <div class="card-body">
                    <!-- GridView Section -->
                    <div class="grid-section">
                        <div class="grid-wrapper">
                            <asp:GridView ID="gvmedico" runat="server" CssClass="gridview-table">
                            </asp:GridView>
                        </div>
                    </div>

                    <!-- Personal Information -->
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

                    <!-- Contact & Location Information -->
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

                    <!-- Professional Information -->
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

                    <!-- Action Buttons -->
                    <div class="button-group">
                        <asp:Button ID="btnModificar" runat="server" Text="Modificar" CssClass="btn btn-primary" />
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" />
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>