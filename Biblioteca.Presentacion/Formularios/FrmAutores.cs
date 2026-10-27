using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Biblioteca.Entidades;
using Biblioteca.Negocio;
using Biblioteca.Presentacion.Utilidades;

namespace Biblioteca.Presentacion.Formularios
{
    /// <summary>CRUD de autores.</summary>
    public partial class FrmAutores : Form
    {
        private readonly AutorNegocio _negocio = new AutorNegocio();
        private int _idSeleccionado;

        public FrmAutores()
        {
            InitializeComponent();
        }

        private void FrmAutores_Load(object sender, EventArgs e)
        {
            Estilos.AplicarTabla(dgvAutores);
            Limpiar();
            CargarTabla(null);
        }

        private void CargarTabla(string texto)
        {
            try
            {
                List<Autor> autores = _negocio.Buscar(texto);
                dgvAutores.DataSource = autores.Select(a => new
                {
                    Codigo = a.IdAutor,
                    a.Nombre,
                    a.Apellido,
                    a.Nacionalidad
                }).ToList();
                Estilos.Encabezados(dgvAutores, "Codigo", "Código");
                Estilos.AnchoFijo(dgvAutores, "Codigo", 70);
                lblTotal.Text = "Registros: " + autores.Count;
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
            txtApellido.Clear();
            txtNacionalidad.Clear();
            lblModo.Text = "Modo: nuevo autor";
            btnEliminar.Enabled = false;
            dgvAutores.ClearSelection();
            txtNombre.Focus();
        }

        private void dgvAutores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = Estilos.ValorEntero(dgvAutores, e.RowIndex, "Codigo");
            if (id == 0) return;
            try
            {
                Autor autor = _negocio.ObtenerPorId(id);
                if (autor == null)
                {
                    Mensajes.Advertencia("El autor ya no existe. Se actualizará la lista.");
                    CargarTabla(txtBuscar.Text);
                    return;
                }
                _idSeleccionado = autor.IdAutor;
                txtNombre.Text = autor.Nombre;
                txtApellido.Text = autor.Apellido;
                txtNacionalidad.Text = autor.Nacionalidad;
                lblModo.Text = "Modo: editando el autor " + autor.NombreCompleto;
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
                Autor autor = new Autor(_idSeleccionado, txtNombre.Text, txtApellido.Text, txtNacionalidad.Text);
                bool esNuevo = autor.IdAutor == 0;
                _negocio.Guardar(autor);
                Mensajes.Exito(esNuevo ? "Autor registrado correctamente." : "Autor actualizado correctamente.");
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
                Mensajes.Advertencia("Seleccione un autor de la tabla.");
                return;
            }
            if (!Mensajes.Confirmar("¿Desea eliminar el autor " + txtNombre.Text + " " + txtApellido.Text + "?"))
            {
                return;
            }
            try
            {
                _negocio.Eliminar(_idSeleccionado);
                Mensajes.Exito("Autor eliminado correctamente.");
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
