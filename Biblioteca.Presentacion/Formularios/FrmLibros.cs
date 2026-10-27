using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Biblioteca.Entidades;
using Biblioteca.Negocio;
using Biblioteca.Presentacion.Utilidades;

namespace Biblioteca.Presentacion.Formularios
{
    /// <summary>CRUD de libros, con su editorial y sus autores.</summary>
    public partial class FrmLibros : Form
    {
        private readonly LibroNegocio _negocio = new LibroNegocio();
        private readonly EditorialNegocio _editorialNegocio = new EditorialNegocio();
        private readonly AutorNegocio _autorNegocio = new AutorNegocio();
        private int _idSeleccionado;

        public FrmLibros()
        {
            InitializeComponent();
        }

        private void FrmLibros_Load(object sender, EventArgs e)
        {
            Estilos.AplicarTabla(dgvLibros);
            cboCriterio.Items.AddRange(new object[] { "Código / ISBN", "Título", "Autor" });
            cboCriterio.SelectedIndex = 1;
            CargarListas();
            Limpiar();
            CargarTabla(null);
        }

        /// <summary>Llena la lista de editoriales y la lista de autores.</summary>
        private void CargarListas()
        {
            try
            {
                cboEditorial.DataSource = _editorialNegocio.Listar();
                cboEditorial.DisplayMember = "Nombre";
                cboEditorial.ValueMember = "IdEditorial";
                cboEditorial.SelectedIndex = -1;

                clbAutores.Items.Clear();
                foreach (Autor autor in _autorNegocio.Listar())
                {
                    clbAutores.Items.Add(autor);
                }
            }
            catch (Exception ex)
            {
                Mensajes.MostrarExcepcion(ex);
            }
        }

        private CriterioBusquedaLibro CriterioSeleccionado()
        {
            switch (cboCriterio.SelectedIndex)
            {
                case 0: return CriterioBusquedaLibro.Codigo;
                case 2: return CriterioBusquedaLibro.Autor;
                default: return CriterioBusquedaLibro.Titulo;
            }
        }

        private void CargarTabla(string texto)
        {
            try
            {
                List<Libro> libros = _negocio.Buscar(CriterioSeleccionado(), texto);
                dgvLibros.DataSource = libros.Select(l => new
                {
                    Codigo = l.IdLibro,
                    ISBN = l.Isbn,
                    Titulo = l.Titulo,
                    Autores = l.NombresAutores,
                    Editorial = l.NombreEditorial,
                    Anio = l.AnioPublicacion,
                    l.Existencias,
                    l.Disponibles
                }).ToList();
                Estilos.Encabezados(dgvLibros, "Codigo", "Código", "Titulo", "Título", "Anio", "Año");
                Estilos.AnchoFijo(dgvLibros, "Codigo", 60);
                Estilos.AnchoFijo(dgvLibros, "ISBN", 125);
                Estilos.AnchoFijo(dgvLibros, "Disponibles", 85);
                Estilos.Ocultar(dgvLibros, "Anio", "Existencias");
                lblTotal.Text = "Registros: " + libros.Count;
            }
            catch (Exception ex)
            {
                Mensajes.MostrarExcepcion(ex);
            }
        }

        private void Limpiar()
        {
            _idSeleccionado = 0;
            txtIsbn.Clear();
            txtTitulo.Clear();
            txtAnio.Clear();
            nudExistencias.Value = 1;
            cboEditorial.SelectedIndex = -1;
            for (int i = 0; i < clbAutores.Items.Count; i++)
            {
                clbAutores.SetItemChecked(i, false);
            }
            lblDisponibles.Text = "Disponibles: -";
            lblModo.Text = "Modo: nuevo libro";
            btnEliminar.Enabled = false;
            dgvLibros.ClearSelection();
            txtIsbn.Focus();
        }

        private void dgvLibros_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = Estilos.ValorEntero(dgvLibros, e.RowIndex, "Codigo");
            if (id == 0) return;
            try
            {
                Libro libro = _negocio.ObtenerPorId(id);
                if (libro == null)
                {
                    Mensajes.Advertencia("El libro ya no existe. Se actualizará la lista.");
                    CargarTabla(txtBuscar.Text);
                    return;
                }
                _idSeleccionado = libro.IdLibro;
                txtIsbn.Text = libro.Isbn;
                txtTitulo.Text = libro.Titulo;
                txtAnio.Text = libro.AnioPublicacion.HasValue ? libro.AnioPublicacion.Value.ToString() : string.Empty;
                nudExistencias.Value = Math.Min(libro.Existencias, (int)nudExistencias.Maximum);
                cboEditorial.SelectedValue = libro.IdEditorial;

                for (int i = 0; i < clbAutores.Items.Count; i++)
                {
                    Autor autor = (Autor)clbAutores.Items[i];
                    clbAutores.SetItemChecked(i, libro.Autores.Any(a => a.IdAutor == autor.IdAutor));
                }

                lblDisponibles.Text = "Disponibles: " + libro.Disponibles + " de " + libro.Existencias;
                lblModo.Text = "Modo: editando el libro \"" + libro.Titulo + "\"";
                btnEliminar.Enabled = true;
            }
            catch (Exception ex)
            {
                Mensajes.MostrarExcepcion(ex);
            }
        }

        /// <summary>Arma el objeto Libro con lo que el usuario escribió en el formulario.</summary>
        private Libro LeerFormulario()
        {
            int? anio = null;
            if (!string.IsNullOrWhiteSpace(txtAnio.Text))
            {
                int valor;
                if (!int.TryParse(txtAnio.Text.Trim(), out valor))
                {
                    throw new ValidacionException("El año de publicación debe ser un número (ejemplo: 1967).");
                }
                anio = valor;
            }

            Libro libro = new Libro(_idSeleccionado, txtIsbn.Text, txtTitulo.Text, anio,
                                    (int)nudExistencias.Value,
                                    cboEditorial.SelectedValue == null ? 0 : (int)cboEditorial.SelectedValue);
            foreach (object item in clbAutores.CheckedItems)
            {
                libro.AgregarAutor((Autor)item);
            }
            return libro;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Libro libro = LeerFormulario();
                bool esNuevo = libro.IdLibro == 0;
                _negocio.Guardar(libro);
                Mensajes.Exito(esNuevo ? "Libro registrado correctamente." : "Libro actualizado correctamente.");
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
                Mensajes.Advertencia("Seleccione un libro de la tabla.");
                return;
            }
            if (!Mensajes.Confirmar("¿Desea eliminar el libro \"" + txtTitulo.Text + "\"?"))
            {
                return;
            }
            try
            {
                _negocio.Eliminar(_idSeleccionado);
                Mensajes.Exito("Libro eliminado correctamente.");
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
            // También recarga editoriales y autores por si se agregaron en otro módulo.
            CargarListas();
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
