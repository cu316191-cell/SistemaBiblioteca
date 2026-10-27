using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Biblioteca.Entidades;
using Biblioteca.Negocio;
using Biblioteca.Presentacion.Utilidades;

namespace Biblioteca.Presentacion.Formularios
{
    /// <summary>Registro, consulta, modificación y devolución de préstamos.</summary>
    public partial class FrmPrestamos : Form
    {
        private readonly PrestamoNegocio _negocio = new PrestamoNegocio();
        private readonly UsuarioNegocio _usuarioNegocio = new UsuarioNegocio();
        private readonly LibroNegocio _libroNegocio = new LibroNegocio();
        private int _idSeleccionado;
        private bool _cargando;

        public FrmPrestamos()
        {
            InitializeComponent();
            dgvPrestamos.CellFormatting += dgvPrestamos_CellFormatting;
        }

        private void FrmPrestamos_Load(object sender, EventArgs e)
        {
            Estilos.AplicarTabla(dgvPrestamos);
            _cargando = true;
            cboFiltro.Items.AddRange(new object[] { "Todos", "Prestados (activos)", "Devueltos", "Vencidos" });
            cboFiltro.SelectedIndex = 0;
            _cargando = false;
            Limpiar();
            CargarTabla();
        }

        private FiltroPrestamo FiltroSeleccionado()
        {
            switch (cboFiltro.SelectedIndex)
            {
                case 1: return FiltroPrestamo.Prestados;
                case 2: return FiltroPrestamo.Devueltos;
                case 3: return FiltroPrestamo.Vencidos;
                default: return FiltroPrestamo.Todos;
            }
        }

        private void CargarTabla()
        {
            try
            {
                List<Prestamo> prestamos = _negocio.Listar(FiltroSeleccionado());
                dgvPrestamos.DataSource = prestamos.Select(p => new
                {
                    Codigo = p.IdPrestamo,
                    Usuario = p.NombreUsuario,
                    Libro = p.TituloLibro,
                    Prestamo = p.FechaPrestamo.ToString("dd/MM/yyyy"),
                    Limite = p.FechaLimite.ToString("dd/MM/yyyy"),
                    Devolucion = p.FechaDevolucion.HasValue ? p.FechaDevolucion.Value.ToString("dd/MM/yyyy") : "",
                    Estado = p.EstadoVisible
                }).ToList();
                Estilos.Encabezados(dgvPrestamos, "Codigo", "Código", "Prestamo", "Préstamo",
                                    "Limite", "Límite", "Devolucion", "Devolución");
                Estilos.AnchoFijo(dgvPrestamos, "Codigo", 55);
                Estilos.AnchoFijo(dgvPrestamos, "Prestamo", 80);
                Estilos.AnchoFijo(dgvPrestamos, "Limite", 80);
                Estilos.AnchoFijo(dgvPrestamos, "Devolucion", 85);
                Estilos.AnchoFijo(dgvPrestamos, "Estado", 75);
                lblTotal.Text = "Registros: " + prestamos.Count;
            }
            catch (Exception ex)
            {
                Mensajes.MostrarExcepcion(ex);
            }
        }

        /// <summary>
        /// Llena las listas de usuarios y libros. Para un préstamo nuevo solo se
        /// muestran los libros con ejemplares disponibles.
        /// </summary>
        private void CargarListas(bool soloLibrosDisponibles)
        {
            try
            {
                cboUsuario.DataSource = _usuarioNegocio.Listar();
                cboUsuario.ValueMember = "IdUsuario";
                cboUsuario.SelectedIndex = -1;

                cboLibro.DataSource = soloLibrosDisponibles ? _libroNegocio.ListarDisponibles() : _libroNegocio.Listar();
                cboLibro.ValueMember = "IdLibro";
                cboLibro.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                Mensajes.MostrarExcepcion(ex);
            }
        }

        private void Limpiar()
        {
            _idSeleccionado = 0;
            CargarListas(true);
            _cargando = true;
            dtpFechaPrestamo.MaxDate = DateTime.Today;
            dtpFechaPrestamo.Value = DateTime.Today;
            dtpFechaLimite.Value = DateTime.Today.AddDays(PrestamoNegocio.DiasPorDefecto);
            dtpFechaDevolucion.MaxDate = DateTime.Today;
            dtpFechaDevolucion.Value = DateTime.Today;
            _cargando = false;

            HabilitarDatos(true);
            lblEstado.Text = "Estado: nuevo préstamo";
            btnGuardar.Text = "Registrar préstamo";
            btnGuardar.Enabled = true;
            btnEliminar.Enabled = false;
            grpDevolucion.Enabled = false;
            dgvPrestamos.ClearSelection();
        }

        private void HabilitarDatos(bool habilitar)
        {
            cboUsuario.Enabled = habilitar;
            cboLibro.Enabled = habilitar;
            dtpFechaPrestamo.Enabled = habilitar;
            dtpFechaLimite.Enabled = habilitar;
        }

        private void dgvPrestamos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = Estilos.ValorEntero(dgvPrestamos, e.RowIndex, "Codigo");
            if (id == 0) return;
            try
            {
                Prestamo prestamo = _negocio.ObtenerPorId(id);
                if (prestamo == null)
                {
                    Mensajes.Advertencia("El préstamo ya no existe. Se actualizará la lista.");
                    CargarTabla();
                    return;
                }

                _idSeleccionado = prestamo.IdPrestamo;
                CargarListas(false);
                _cargando = true;
                cboUsuario.SelectedValue = prestamo.IdUsuario;
                cboLibro.SelectedValue = prestamo.IdLibro;
                dtpFechaPrestamo.MaxDate = DateTimePicker.MaximumDateTime;
                dtpFechaPrestamo.Value = prestamo.FechaPrestamo;
                dtpFechaLimite.Value = prestamo.FechaLimite;
                _cargando = false;

                btnEliminar.Enabled = true;
                if (prestamo.EstaDevuelto)
                {
                    lblEstado.Text = "Estado: devuelto el " + prestamo.FechaDevolucion.Value.ToString("dd/MM/yyyy");
                    HabilitarDatos(false);
                    btnGuardar.Enabled = false;
                    grpDevolucion.Enabled = false;
                }
                else
                {
                    lblEstado.Text = prestamo.EstaVencido
                        ? "Estado: vencido (" + (DateTime.Today - prestamo.FechaLimite.Date).Days + " días de retraso)"
                        : "Estado: prestado";
                    HabilitarDatos(true);
                    btnGuardar.Text = "Guardar cambios";
                    btnGuardar.Enabled = true;
                    grpDevolucion.Enabled = true;
                    dtpFechaDevolucion.MinDate = DateTimePicker.MinimumDateTime;
                    dtpFechaDevolucion.Value = DateTime.Today;
                }
            }
            catch (Exception ex)
            {
                Mensajes.MostrarExcepcion(ex);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Prestamo prestamo = new Prestamo
                {
                    IdPrestamo = _idSeleccionado,
                    IdUsuario = cboUsuario.SelectedValue == null ? 0 : (int)cboUsuario.SelectedValue,
                    IdLibro = cboLibro.SelectedValue == null ? 0 : (int)cboLibro.SelectedValue,
                    FechaPrestamo = dtpFechaPrestamo.Value.Date,
                    FechaLimite = dtpFechaLimite.Value.Date
                };

                if (prestamo.IdPrestamo == 0)
                {
                    _negocio.Registrar(prestamo);
                    Mensajes.Exito("Préstamo registrado correctamente.\nDebe devolverse a más tardar el "
                                   + prestamo.FechaLimite.ToString("dd/MM/yyyy") + ".");
                }
                else
                {
                    _negocio.Actualizar(prestamo);
                    Mensajes.Exito("Préstamo actualizado correctamente.");
                }
                Limpiar();
                CargarTabla();
            }
            catch (Exception ex)
            {
                Mensajes.MostrarExcepcion(ex);
            }
        }

        private void btnDevolver_Click(object sender, EventArgs e)
        {
            if (_idSeleccionado == 0)
            {
                Mensajes.Advertencia("Seleccione en la tabla el préstamo que se va a devolver.");
                return;
            }
            try
            {
                _negocio.RegistrarDevolucion(_idSeleccionado, dtpFechaDevolucion.Value.Date);
                Mensajes.Exito("Devolución registrada. El préstamo quedó en estado \"Devuelto\" "
                               + "y el ejemplar vuelve a estar disponible.");
                Limpiar();
                CargarTabla();
            }
            catch (Exception ex)
            {
                Mensajes.MostrarExcepcion(ex);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_idSeleccionado == 0)
            {
                Mensajes.Advertencia("Seleccione un préstamo de la tabla.");
                return;
            }
            if (!Mensajes.Confirmar("¿Desea eliminar el préstamo número " + _idSeleccionado + "?\n"
                                    + "Si el libro sigue prestado, el ejemplar volverá a quedar disponible."))
            {
                return;
            }
            try
            {
                _negocio.Eliminar(_idSeleccionado);
                Mensajes.Exito("Préstamo eliminado correctamente.");
                Limpiar();
                CargarTabla();
            }
            catch (Exception ex)
            {
                Mensajes.MostrarExcepcion(ex);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void cboFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_cargando) CargarTabla();
        }

        /// <summary>Al cambiar la fecha de un préstamo nuevo, propone la fecha límite (14 días después).</summary>
        private void dtpFechaPrestamo_ValueChanged(object sender, EventArgs e)
        {
            if (_cargando || _idSeleccionado != 0) return;
            dtpFechaLimite.Value = dtpFechaPrestamo.Value.Date.AddDays(PrestamoNegocio.DiasPorDefecto);
        }

        /// <summary>Pinta de rojo claro las filas de préstamos vencidos.</summary>
        private void dgvPrestamos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || !dgvPrestamos.Columns.Contains("Estado")) return;
            object estado = dgvPrestamos.Rows[e.RowIndex].Cells["Estado"].Value;
            if (estado != null && estado.ToString() == "Vencido")
            {
                e.CellStyle.BackColor = Estilos.ColorVencido;
            }
        }
    }
}
