using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Biblioteca.Entidades;
using Biblioteca.Negocio;
using Biblioteca.Presentacion.Utilidades;

namespace Biblioteca.Presentacion.Formularios
{
    /// <summary>CRUD de usuarios de la biblioteca.</summary>
    public partial class FrmUsuarios : Form
    {
        private readonly UsuarioNegocio _negocio = new UsuarioNegocio();
        private int _idSeleccionado;
        private DateTime _fechaRegistro = DateTime.Today;

        public FrmUsuarios()
        {
            InitializeComponent();
        }

        private void FrmUsuarios_Load(object sender, EventArgs e)
        {
            Estilos.AplicarTabla(dgvUsuarios);
            Limpiar();
            CargarTabla(null);
        }

        private void CargarTabla(string texto)
        {
            try
            {
                List<Usuario> usuarios = _negocio.Buscar(texto);
                dgvUsuarios.DataSource = usuarios.Select(u => new
                {
                    Codigo = u.IdUsuario,
                    u.Documento,
                    u.Nombre,
                    u.Apellido,
                    Telefono = u.Telefono,
                    u.Correo
                }).ToList();
                Estilos.Encabezados(dgvUsuarios, "Codigo", "Código", "Telefono", "Teléfono");
                Estilos.AnchoFijo(dgvUsuarios, "Codigo", 60);
                lblTotal.Text = "Registros: " + usuarios.Count;
            }
            catch (Exception ex)
            {
                Mensajes.MostrarExcepcion(ex);
            }
        }

        private void Limpiar()
        {
            _idSeleccionado = 0;
            _fechaRegistro = DateTime.Today;
            txtDocumento.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtTelefono.Clear();
            txtCorreo.Clear();
            lblFechaRegistro.Text = _fechaRegistro.ToString("dd/MM/yyyy");
            lblModo.Text = "Modo: nuevo usuario";
            btnEliminar.Enabled = false;
            dgvUsuarios.ClearSelection();
            txtDocumento.Focus();
        }

        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = Estilos.ValorEntero(dgvUsuarios, e.RowIndex, "Codigo");
            if (id == 0) return;
            try
            {
                Usuario usuario = _negocio.ObtenerPorId(id);
                if (usuario == null)
                {
                    Mensajes.Advertencia("El usuario ya no existe. Se actualizará la lista.");
                    CargarTabla(txtBuscar.Text);
                    return;
                }
                _idSeleccionado = usuario.IdUsuario;
                _fechaRegistro = usuario.FechaRegistro;
                txtDocumento.Text = usuario.Documento;
                txtNombre.Text = usuario.Nombre;
                txtApellido.Text = usuario.Apellido;
                txtTelefono.Text = usuario.Telefono;
                txtCorreo.Text = usuario.Correo;
                lblFechaRegistro.Text = usuario.FechaRegistro.ToString("dd/MM/yyyy");
                lblModo.Text = "Modo: editando el usuario " + usuario.NombreCompleto;
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
                Usuario usuario = new Usuario(_idSeleccionado, txtDocumento.Text, txtNombre.Text, txtApellido.Text,
                                              txtTelefono.Text, txtCorreo.Text, _fechaRegistro);
                bool esNuevo = usuario.IdUsuario == 0;
                _negocio.Guardar(usuario);
                Mensajes.Exito(esNuevo ? "Usuario registrado correctamente." : "Usuario actualizado correctamente.");
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
                Mensajes.Advertencia("Seleccione un usuario de la tabla.");
                return;
            }
            if (!Mensajes.Confirmar("¿Desea eliminar el usuario con documento " + txtDocumento.Text + "?"))
            {
                return;
            }
            try
            {
                _negocio.Eliminar(_idSeleccionado);
                Mensajes.Exito("Usuario eliminado correctamente.");
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
