using System.Collections.Generic;
using Biblioteca.Datos;
using Biblioteca.Entidades;

namespace Biblioteca.Negocio
{
    /// <summary>Reglas del módulo de editoriales.</summary>
    public class EditorialNegocio
    {
        private readonly EditorialDAO _dao = new EditorialDAO();

        public List<Editorial> Listar()
        {
            return _dao.Listar();
        }

        public List<Editorial> Buscar(string texto)
        {
            return string.IsNullOrWhiteSpace(texto) ? _dao.Listar() : _dao.Buscar(texto.Trim());
        }

        public Editorial ObtenerPorId(int id)
        {
            return _dao.ObtenerPorId(id);
        }

        public void Guardar(Editorial editorial)
        {
            Validar(editorial);
            if (_dao.ExisteNombre(editorial.Nombre, editorial.IdEditorial))
            {
                throw new ValidacionException("Ya existe una editorial con el nombre \"" + editorial.Nombre + "\".");
            }

            if (editorial.IdEditorial == 0)
            {
                _dao.Insertar(editorial);
            }
            else
            {
                if (_dao.ObtenerPorId(editorial.IdEditorial) == null)
                {
                    throw new ValidacionException("La editorial que intenta modificar ya no existe.");
                }
                _dao.Actualizar(editorial);
            }
        }

        public void Eliminar(int idEditorial)
        {
            if (idEditorial <= 0)
            {
                throw new ValidacionException("Seleccione una editorial de la lista.");
            }
            int libros = _dao.ContarLibros(idEditorial);
            if (libros > 0)
            {
                throw new ValidacionException(
                    "No se puede eliminar la editorial porque tiene " + libros + " libro(s) registrado(s).");
            }
            _dao.Eliminar(idEditorial);
        }

        private static void Validar(Editorial editorial)
        {
            if (editorial == null) throw new ValidacionException("No hay datos de la editorial.");
            Validador.Requerido(editorial.Nombre, "Nombre");
            Validador.LongitudMaxima(editorial.Nombre, 100, "Nombre");
            Validador.TextoOpcionalLetras(editorial.Pais, 50, "País");
            Validador.Telefono(editorial.Telefono, "Teléfono");
            Validador.CorreoOpcional(editorial.Correo, "Correo");
        }
    }
}
