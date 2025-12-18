using Negocio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace proyecto_final
{
    public partial class Pagina_informes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblAdmin.Text = Session["NombreAdmin"]?.ToString();

            if (!IsPostBack)
            {
                CargarEspecialidades();
                MostrarInforme(0); 
            }
        }

        private void CargarEspecialidades()
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT IdEspecialidad, Nombre FROM Especialidad ORDER BY Nombre",
                    conexion);

                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                ddlEspecialidad.Items.Clear();
                ddlEspecialidad.Items.Add(new ListItem("-- Todas --", "0"));

                while (dr.Read())
                {
                    ddlEspecialidad.Items.Add(new ListItem(dr["Nombre"].ToString(),
                                                           dr["IdEspecialidad"].ToString()));
                }
            }
        }

        private void MostrarInforme(int n)
        {
            pnlInforme1.Visible = (n == 1);
            pnlInforme2.Visible = (n == 2);
            pnlInforme3.Visible = (n == 3);

            pnlResultados.Visible = false;
            pnlResultadosFechas.Visible = false;
        }

        // ===== INFORME 1 =====
        protected void lnkInforme1_Click(object sender, EventArgs e)
        {
            MostrarInforme(1);
        }

        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            if (ddlEspecialidad.Items.Count == 0)
            {
                lblPresentes.Text = "0";
                lblAusentes.Text = "0";
                lblTotal.Text = "0";
                lblPorcPresentes.Text = "Sin datos";
                lblPorcAusentes.Text = "Sin datos";
                pnlResultados.Visible = true;
                return;
            }

            var inf = new Informes();
            int idEsp = int.Parse(ddlEspecialidad.SelectedValue);

            var r = inf.obtener_asistencia(idEsp);

            // Validación si no hay resultados
            if (r.cant_total == 0)
            {
                lblPresentes.Text = "0";
                lblAusentes.Text = "0";
                lblTotal.Text = "0";
                lblPorcPresentes.Text = "0%";
                lblPorcAusentes.Text = "0%";

                // Limpia el gráfico
                chartAsistencia.Series.Clear();
                pnlResultados.Visible = true;
                return;
            }

            lblPresentes.Text = r.cant_presentes.ToString();
            lblAusentes.Text = r.cant_ausentes.ToString();
            lblTotal.Text = r.cant_total.ToString();
            lblPorcPresentes.Text = r.porc_presentes.ToString("0.00") + "%";
            lblPorcAusentes.Text = r.porc_ausentes.ToString("0.00") + "%";

            CrearGrafico(r.cant_presentes, r.cant_ausentes);

            pnlResultados.Visible = true;
        }

        private void CrearGrafico(int pres, int aus)
        {
            chartAsistencia.Series.Clear();
            chartAsistencia.ChartAreas.Clear();

            ChartArea area = new ChartArea();
            chartAsistencia.ChartAreas.Add(area);

            Series serie = new Series();
            serie.ChartType = SeriesChartType.Pie;
            //// presentes
            int pPres = serie.Points.AddXY("Presentes", pres);
            serie.Points[pPres].Color =
                System.Drawing.ColorTranslator.FromHtml("#28A946");

            // texto presentes
            serie.Points[pPres].LabelForeColor = System.Drawing.Color.White;
            serie.Points[pPres].Font = new System.Drawing.Font("Segoe UI", 11, FontStyle.Bold);
            
            // ausentes
            int pAus = serie.Points.AddXY("Ausentes", aus);
            serie.Points[pAus].Color =
                System.Drawing.ColorTranslator.FromHtml("#eb5757");

            // texto ausentes
            serie.Points[pAus].LabelForeColor = System.Drawing.Color.White;
            serie.Points[pAus].Font = new System.Drawing.Font("Segoe UI", 11, FontStyle.Bold);

            serie.BorderColor = System.Drawing.Color.White;
            serie.BorderWidth = 2;

            serie.Label = "#VALX";

            chartAsistencia.Series.Add(serie);
        }

        protected void btnLimpiarFiltro2_Click(object sender, EventArgs e)
        {
            ddlEspecialidad.SelectedIndex = 0;

            lblPresentes.Text = "";
            lblAusentes.Text = "";
            lblTotal.Text = "";
            lblPorcPresentes.Text = "";
            lblPorcAusentes.Text = "";

            chartAsistencia.Series.Clear();
            chartAsistencia.ChartAreas.Clear();

            pnlResultados.Visible = false;
        }


        // ===== INFORME 2 =====
        protected void lnkInforme2_Click(object sender, EventArgs e)
        {
            MostrarInforme(2);
        }

        protected void btnFiltrarFechas_Click(object sender, EventArgs e)
        {
            // Validar fechas vacías
            if (string.IsNullOrEmpty(txtFechaDesde.Text) || string.IsNullOrEmpty(txtFechaHasta.Text))
            {
                lblCantidadTurnos.Text = "❌ Debe seleccionar ambas fechas.";
                pnlResultadosFechas.Visible = true;
                repTurnosFechas.DataSource = null;
                repTurnosFechas.DataBind();
                return;
            }

            DateTime desde = DateTime.Parse(txtFechaDesde.Text);
            DateTime hasta = DateTime.Parse(txtFechaHasta.Text);

            // Validar rango
            if (desde > hasta)
            {
                lblCantidadTurnos.Text = "❌ La fecha 'Desde' no puede ser mayor que 'Hasta'.";
                pnlResultadosFechas.Visible = true;
                repTurnosFechas.DataSource = null;
                repTurnosFechas.DataBind();
                return;
            }

            var inf = new Informes();
            var lista = inf.ObtenerTurnosEntreFechas(desde, hasta);

            if (lista.Count == 0)
            {
                lblCantidadTurnos.Text = "⚠️ No hay turnos en ese rango de fechas.";
                pnlResultadosFechas.Visible = true;

                repTurnosFechas.DataSource = null;
                repTurnosFechas.DataBind();
                return;
            }


            repTurnosFechas.DataSource = lista;
            repTurnosFechas.DataBind();

            int total = lista.Count;
            int presentes = lista.Count(t => t.Item4 == "Presente");
            int ausentes = lista.Count(t => t.Item4 == "Ausente");

            double porcPresentes = total > 0 ? (presentes * 100.0) / total : 0;
            double porcAusentes = total > 0 ? (ausentes * 100.0) / total : 0;

            lblTotalFechas.Text = total.ToString();
            lblPresentesFechas.Text = presentes.ToString();
            lblAusentesFechas.Text = ausentes.ToString();

            lblPorcPresentesFechas.Text = porcPresentes.ToString("0.00") + "%";
            lblPorcAusentesFechas.Text = porcAusentes.ToString("0.00") + "%";

            lblCantidadTurnos.Text = $"Informe de turnos desde {desde:dd/MM/yyyy} hasta {hasta:dd/MM/yyyy}";
            pnlResultadosFechas.Visible = true;
        }

        protected void btnLimpiarFiltro_Click(object sender, EventArgs e)
        {
            txtFechaDesde.Text = "";
            txtFechaHasta.Text = "";

            lblCantidadTurnos.Text = "";

            lblTotalFechas.Text = "";
            lblPresentesFechas.Text = "";
            lblAusentesFechas.Text = "";
            lblPorcPresentesFechas.Text = "";
            lblPorcAusentesFechas.Text = "";

            repTurnosFechas.DataSource = null;
            repTurnosFechas.DataBind();

            pnlResultadosFechas.Visible = false;
        }

        // ===== INFORME 3 =====
        protected void lnkInforme3_Click(object sender, EventArgs e)
        {
            MostrarInforme(3);

            var inf = new Informes();
            var datos = inf.ObtenerPromedioPorEspecialidad();

            pnlEspecialidad.Controls.Clear();

            if (datos.Count == 0)
            {
                pnlEspecialidad.Controls.Add(new Literal
                {
                    Text = "<p>No hay datos registrados.</p>"
                });

                return;
            }

            Table tabla = new Table();

            TableRow header = new TableRow();
            header.Cells.Add(new TableCell { Text = "Especialidad" });
            header.Cells.Add(new TableCell { Text = "Total" });
            header.Cells.Add(new TableCell { Text = "Presentes" });
            header.Cells.Add(new TableCell { Text = "Ausentes" });
            header.Cells.Add(new TableCell { Text = "% Asistencia" });
            tabla.Rows.Add(header);

            foreach (var d in datos)
            {
                TableRow row = new TableRow();
                row.Cells.Add(new TableCell { Text = d.especialidad });
                row.Cells.Add(new TableCell { Text = d.total.ToString() });
                row.Cells.Add(new TableCell { Text = d.presentes.ToString() });
                row.Cells.Add(new TableCell { Text = d.ausentes.ToString() });
                row.Cells.Add(new TableCell { Text = d.porcentaje.ToString("0.00") + "%" });

                tabla.Rows.Add(row);
            }

            pnlEspecialidad.Controls.Add(tabla);
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("pagina_administrador.aspx");
        }
    }
}
