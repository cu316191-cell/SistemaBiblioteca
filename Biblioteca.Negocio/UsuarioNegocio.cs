using System.Collections.Generic;
using Biblioteca.Datos;
using Biblioteca.Entidades;

namespace Biblioteca.Negocio
{
    /// <summary>Reglas del módulo de usuarios.</summary>
    public class UsuarioNegocio
    {
        private readonly UsuarioDAO _dao = new UsuarioDAO();

        public List<Usuario> Listar()
        {
            return _dao.Listar();
        }

        public List<Usuario> Buscar(string texto)
        {
            return string.IsNullOrWhiteSpace(texto) ? _dao.Listar() : _dao.Buscar(texto.Trim());
        }

        public Usuario ObtenerPorId(int id)
        {
            return _dao.ObtenerPorId(id);
        }

        public void Guardar(Usuario usuario)
        {
            Validar(usuario);
            if (_dao.ExisteDocumento(usuario.Documento, usuario.IdUsuario))
            {
                throw new ValidacionException("Ya existe un usuario con el documento " + usuario.Documento + ".");
            }

            if (usuario.IdUsuario == 0)
            {
                _dao.Insertar(usuario);
            }
            else
            {
                if (_dao.ObtenerPorId(usuario.IdUsuario) == null)
                {
                    throw new ValidacionException("El usuario que intenta modificar ya no existe.");
                }
                _dao.Actualizar(usuario);
            }
        }

        /// <summary>
        /// Un usuario con préstamos (activos o en el historial) no se elimina,
        /// para no perder el historial de la biblioteca.
        /// </summary>
        public void Eliminar(int idUsuario)
        {
            if (idUsuario <= 0)
            {
                throw new ValidacionException("Seleccione un usuario de la lista.");
            }
            int prestamos = _dao.ContarPrestamos(idUsuario);
            if (prestamos > 0)
            {
                throw new ValidacionException(
                    "No se puede eliminar el usuario porque tiene " + prestamos +
                    " préstamo(s) registrado(s) en el historial.");
            }
            _dao.Eliminar(idUsuario);
        }

        private static void Validar(Usuario usuario)
        {
            if (usuario == null) throw new ValidacionException("No hay datos del usuario.");
            Validador.Documento(usuario.Documento);
            Validador.Nombre(usuario.Nombre, 60, "Nombre");
            Validador.Nombre(usuario.Apellido, 60, "Apellido");
            Validador.Telefono(usuario.Telefono, "Teléfono");
            Validador.CorreoOpcional(usuario.Correo, "Correo");
        }
    }
}
