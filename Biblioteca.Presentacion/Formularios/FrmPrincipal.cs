using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Biblioteca.Presentacion.Formularios
{
    /// <summary>
    /// Formulario principal. Tiene el menú de navegación a la izquierda y
    /// muestra cada módulo dentro del panel de contenido.
    /// </summary>
    public partial class FrmPrincipal : Form
    {
        private static readonly Color ColorMenu = Color.FromArgb(33, 47, 61);
        private static readonly Color ColorMenuActivo = Color.FromArgb(31, 78, 121);

        private Form _formularioActual;
        private Button _botonActivo;

        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            CultureInfo cultura = new CultureInfo("es-CO");
            string fecha = DateTime.Today.ToString("dddd, d 'de' MMMM 'de' yyyy", cultura);
            lblFecha.Text = char.ToUpper(fecha[0], cultura) + fecha.Substring(1);
            AbrirModulo(new FrmInicio(), btnInicio, "Inicio");
        }

        /// <summary>Muestra un módulo dentro del panel de contenido.</summary>
        private void AbrirModulo(Form formulario, Button boton, string titulo)
        {
            if (_formularioActual != null)
            {
                _formularioActual.Close();
                _formularioActual.Dispose();
            }

            _formularioActual = formulario;
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            pnlContenido.Controls.Add(formulario);
            formulario.Show();

            lblSeccion.Text = titulo;
            MarcarBoton(boton);
        }

        private void MarcarBoton(Button boton)
        {
            if (_botonActivo != null)
            {
                _botonActivo.BackColor = ColorMenu;
            }
            _botonActivo = boton;
            _botonActivo.BackColor = ColorMenuActivo;
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            AbrirModulo(new FrmInicio(), btnInicio, "Inicio");
        }

        private void btnLibros_Click(object sender, EventArgs e)
        {
            AbrirModulo(new FrmLibros(), btnLibros, "Libros");
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            AbrirModulo(new FrmUsuarios(), btnUsuarios, "Usuarios");
        }

        private void btnAutores_Click(object sender, EventArgs e)
        {
            AbrirModulo(new FrmAutores(), btnAutores, "Autores");
        }

        private void btnEditoriales_Click(object sender, EventArgs e)
        {
            AbrirModulo(new FrmEditoriales(), btnEditoriales, "Editoriales");
        }

        private void btnPrestamos_Click(object sender, EventArgs e)
        {
            AbrirModulo(new FrmPrestamos(), btnPrestamos, "Préstamos");
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            AbrirModulo(new FrmReportes(), btnReportes, "Reportes");
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
