using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Biblioteca.Negocio;
using Biblioteca.Presentacion.Utilidades;

namespace Biblioteca.Presentacion.Formularios
{
    /// <summary>Consulta de reportes de la biblioteca.</summary>
    public partial class FrmReportes : Form
    {
        private readonly ReporteNegocio _negocio = new ReporteNegocio();
        private readonly UsuarioNegocio _usuarioNegocio = new UsuarioNegocio();

        /// <summary>Elemento de la lista de reportes (texto visible + tipo).</summary>
        private class OpcionReporte
        {
            public OpcionReporte(TipoReporte tipo)
            {
                Tipo = tipo;
            }

            public TipoReporte Tipo { get; private set; }

            public override string ToString()
            {
                return ReporteNegocio.Titulo(Tipo);
            }
        }

        public FrmReportes()
        {
            InitializeComponent();
        }

        private void FrmReportes_Load(object sender, EventArgs e)
        {
            Estilos.AplicarTabla(dgvReporte);
            foreach (TipoReporte tipo in Enum.GetValues(typeof(TipoReporte)).Cast<TipoReporte>())
            {
                cboReporte.Items.Add(new OpcionReporte(tipo));
            }
            try
            {
                cboUsuario.DataSource = _usuarioNegocio.Listar();
                cboUsuario.DisplayMember = "TextoLista";
                cboUsuario.ValueMember = "IdUsuario";
                cboUsuario.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                Mensajes.MostrarExcepcion(ex);
            }
            cboReporte.SelectedIndex = 0;
            Generar();
        }

        private TipoReporte TipoSeleccionado()
        {
            OpcionReporte opcion = cboReporte.SelectedItem as OpcionReporte;
            return opcion == null ? TipoReporte.ResumenGeneral : opcion.Tipo;
        }

        private void cboReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool porUsuario = TipoSeleccionado() == TipoReporte.HistorialPorUsuario;
            lblUsuario.Visible = porUsuario;
            cboUsuario.Visible = porUsuario;
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            Generar();
        }

        private void Generar()
        {
            try
            {
                int idUsuario = cboUsuario.SelectedValue == null ? 0 : (int)cboUsuario.SelectedValue;
                DataTable tabla = _negocio.Generar(TipoSeleccionado(), idUsuario);
                dgvReporte.DataSource = tabla;

                // Las fechas se muestran sin hora
                foreach (DataGridViewColumn columna in dgvReporte.Columns)
                {
                    if (columna.ValueType == typeof(DateTime))
                    {
                        columna.DefaultCellStyle.Format = "dd/MM/yyyy";
                    }
                }
                lblTotal.Text = ReporteNegocio.Titulo(TipoSeleccionado()) + " - Registros: " + tabla.Rows.Count
                                + " - Generado el " + DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            }
            catch (Exception ex)
            {
                Mensajes.MostrarExcepcion(ex);
            }
        }
    }
}
