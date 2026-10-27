using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Biblioteca.Negocio;
using Biblioteca.Presentacion.Utilidades;

namespace Biblioteca.Presentacion.Formularios
{
    /// <summary>Pantalla de inicio: estado de la conexión y resumen de la biblioteca.</summary>
    public partial class FrmInicio : Form
    {
        private readonly ReporteNegocio _reporteNegocio = new ReporteNegocio();

        public FrmInicio()
        {
            InitializeComponent();
        }

        private void FrmInicio_Load(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void Actualizar()
        {
            string mensaje;
            bool conectado = ConexionNegocio.Probar(out mensaje);
            lblConexion.Text = (conectado ? "Conexión correcta. " : "Sin conexión. ") + mensaje;
            lblConexion.ForeColor = conectado ? Color.FromArgb(25, 110, 60) : Color.FromArgb(170, 30, 30);

            if (!conectado)
            {
                lblValorLibros.Text = lblValorUsuarios.Text = lblValorActivos.Text = lblValorVencidos.Text = "-";
                return;
            }

            try
            {
                DataTable resumen = _reporteNegocio.Generar(TipoReporte.ResumenGeneral, 0);
                lblValorLibros.Text = Buscar(resumen, "Libros (títulos) registrados");
                lblValorUsuarios.Text = Buscar(resumen, "Usuarios registrados");
                lblValorActivos.Text = Buscar(resumen, "Préstamos activos");
                lblValorVencidos.Text = Buscar(resumen, "Préstamos vencidos");
            }
            catch (Exception ex)
            {
                Mensajes.MostrarExcepcion(ex);
            }
        }

        private static string Buscar(DataTable resumen, string indicador)
        {
            foreach (DataRow fila in resumen.Rows)
            {
                if (Convert.ToString(fila["Indicador"]) == indicador)
                {
                    return Convert.ToString(fila["Valor"]);
                }
            }
            return "-";
        }
    }
}
