using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Biblioteca.Entidades;
using Biblioteca.Negocio;
using Biblioteca.Presentacion.Utilidades;

namespace Biblioteca.Presentacion.Formularios
{
    /// <summary>CRUD de editoriales.</summary>
    public partial class FrmEditoriales : Form
    {
        private readonly EditorialNegocio _negocio = new EditorialNegocio();
        private int _idSeleccionado;

        public FrmEditoriales()
        {
            InitializeComponent();
        }

        private void FrmEditoriales_Load(object sender, EventArgs e)
        {
            Estilos.AplicarTabla(dgvEditoriales);
            Limpiar();
            CargarTabla(null);
        }

        private void CargarTabla(string texto)
        {
            try
            {
                List<Editorial> editoriales = _negocio.Buscar(texto);
                dgvEditoriales.DataSource = editoriales.Select(ed => new
                {
                    Codigo = ed.IdEditorial,
                    ed.Nombre,
                    Pais = ed.Pais,
                    Telefono = ed.Telefono,
                    ed.Correo
                }).ToList();
                Estilos.Encabezados(dgvEditoriales, "Codigo", "Código", "Pais", "País", "Telefono", "Teléfono");
                Estilos.AnchoFijo(dgvEditoriales, "Codigo", 70);
                lblTotal.Text = "Registros: " + editoriales.Count;
            }
            catch (Exception ex)
            {
                Mensajes.MostrarExcepcion(ex);
            }
        }

        private void Limpiar()
        {
            _idSeleccionado = 0;
            txtNombre.Clear();
            txtPais.Clear();
            txtTelefono.Clear();
            txtCorreo.Clear();
            lblModo.Text = "Modo: nueva editorial";
            btnEliminar.Enabled = false;
            dgvEditoriales.ClearSelection();
            txtNombre.Focus();
        }

        private void dgvEditoriales_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = Estilos.ValorEntero(dgvEditoriales, e.RowIndex, "Codigo");
            if (id == 0) return;
            try
            {
                Editorial editorial = _negocio.ObtenerPorId(id);
                if (editorial == null)
                {
                    Mensajes.Advertencia("La editorial ya no existe. Se actualizará la lista.");
                    CargarTabla(txtBuscar.Text);
                    return;
                }
                _idSeleccionado = editorial.IdEditorial;
                txtNombre.Text = editorial.Nombre;
                txtPais.Text = editorial.Pais;
                txtTelefono.Text = editorial.Telefono;
                txtCorreo.Text = editorial.Correo;
                lblModo.Text = "Modo: editando la editorial " + editorial.Nombre;
                btnEliminar.Enabled = true;
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
                Editorial editorial = new Editorial(_idSeleccionado, txtNombre.Text, txtPais.Text,
                                                    txtTelefono.Text, txtCorreo.Text);
                bool esNuevo = editorial.IdEditorial == 0;
                _negocio.Guardar(editorial);
                Mensajes.Exito(esNuevo ? "Editorial registrada correctamente." : "Editorial actualizada correctamente.");
                Limpiar();
                CargarTabla(txtBuscar.Text);
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
                Mensajes.Advertencia("Seleccione una editorial de la tabla.");
                return;
            }
            if (!Mensajes.Confirmar("¿Desea eliminar la editorial " + txtNombre.Text + "?"))
            {
                return;
            }
            try
            {
                _negocio.Eliminar(_idSeleccionado);
                Mensajes.Exito("Editorial eliminada correctamente.");
                Limpiar();
                CargarTabla(txtBuscar.Text);
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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarTabla(txtBuscar.Text);
        }

        private void btnVerTodos_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            CargarTabla(null);
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                CargarTabla(txtBuscar.Text);
            }
        }
    }
}
