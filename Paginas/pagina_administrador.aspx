<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pagina_administrador.aspx.cs" Inherits="proyecto_final.pagina_administrador" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Panel Administrador</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.5/font/bootstrap-icons.css" rel="stylesheet" />
<style>
    body {
        background-color: #f8f9fa;
        font-family: 'Segoe UI', sans-serif;
    }

    .main-content {
        padding: 40px 60px;
        max-width: 1200px;
        margin: 0 auto;
    }

    .topbar {
        display: flex;
        justify-content: flex-end;
        align-items: center;
        padding: 10px 20px;
        background-color: #ffffff;
        border-bottom: 1px solid #dee2e6;
        border-radius: 8px;
        box-shadow: 0 2px 5px rgba(0, 0, 0, 0.05);
    }

    .user-info {
        display: flex;
        align-items: center;
        gap: 10px;
        font-weight: 500;
    }

    .user-info i {
        font-size: 1.6rem;
        color: #153f8a; 
    }

    .page-header {
        text-align: center;
        margin: 50px 0 30px 0;
    }

    .card-link {
        display: block;
        text-decoration: none !important;
        color: inherit !important;
        background-color: #ffffff;
        border: none;
        border-radius: 15px;
        box-shadow: 0 3px 6px rgba(0,0,0,0.1);
        text-align: center;
        transition: transform 0.2s ease-in-out, box-shadow 0.2s;
        cursor: pointer;
        padding: 1.5rem;
    }

    .card-link:hover {
        transform: scale(1.05);
        box-shadow: 0 6px 12px rgba(0,0,0,0.15);
    }

    .card-link i {
        font-size: 3rem;
        margin-bottom: 10px;
        color: #153f8a; 
    }

    .card-link h5 {
        color: #212529;
        font-weight: 600;
    }
</style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="main-content">

            <div class="topbar">
                <div class="user-info">
                    <i class="bi bi-person-circle"></i>
                    <asp:Label ID="lblUsuario" runat="server" Text="Administrador"></asp:Label>
                </div>
            </div>

            <div class="page-header">
                <h2 class="fw-semibold">Panel Administrador</h2>
                <p class="text-muted">Seleccione una opción para gestionar del sistema</p>
            </div>

            <div class="container">
                <div class="row justify-content-center g-4">

                    <div class="col-sm-6 col-md-3">
                        <asp:LinkButton ID="btnPacientes" runat="server" CssClass="card-link" OnClick="btnPacientes_Click">
                            <i class="bi bi-person-lines-fill"></i>
                            <h5>Pacientes</h5>
                        </asp:LinkButton>
                    </div>

                    <div class="col-sm-6 col-md-3">
                        <asp:LinkButton ID="btnMedicos" runat="server" CssClass="card-link" OnClick="btnMedicos_Click">
                            <i class="bi bi-person-badge"></i>
                            <h5>Médicos</h5>
                        </asp:LinkButton>
                    </div>

                    <div class="col-sm-6 col-md-3">
                        <asp:LinkButton ID="btnTurnos" runat="server" CssClass="card-link" OnClick="btnTurnos_Click">
                            <i class="bi bi-calendar-check"></i>
                            <h5>Asignar Turnos</h5>
                        </asp:LinkButton>
                    </div>

                    <div class="col-sm-6 col-md-3">
                        <asp:LinkButton ID="btnInformes" runat="server" CssClass="card-link" OnClick="btnInformes_Click">
                            <i class="bi bi-bar-chart-line"></i>
                            <h5>Informes</h5>
                        </asp:LinkButton>
                    </div>

                </div>
            </div>
        </div>
    </form>
</body>
</html>
