using System.Collections.Generic;
using Biblioteca.Datos;
using Biblioteca.Entidades;

namespace Biblioteca.Negocio
{
    /// <summary>Reglas del módulo de libros.</summary>
    public class LibroNegocio
    {
        public const int ExistenciasMaximas = 1000;

        private readonly LibroDAO _dao = new LibroDAO();
        private readonly EditorialDAO _editorialDao = new EditorialDAO();
        private readonly AutorDAO _autorDao = new AutorDAO();

        public List<Libro> Listar()
        {
            return _dao.Listar();
        }

        public List<Libro> ListarDisponibles()
        {
            return _dao.ListarDisponibles();
        }

        public List<Libro> Buscar(CriterioBusquedaLibro criterio, string texto)
        {
            return string.IsNullOrWhiteSpace(texto) ? _dao.Listar() : _dao.Buscar(criterio, texto.Trim());
        }

        public Libro ObtenerPorId(int id)
        {
            return _dao.ObtenerPorId(id);
        }

        public void Guardar(Libro libro)
        {
            Validar(libro);

            if (_dao.ExisteIsbn(libro.Isbn, libro.IdLibro))
            {
                throw new ValidacionException("Ya existe un libro con el ISBN " + libro.Isbn + ".");
            }
            if (_editorialDao.ObtenerPorId(libro.IdEditorial) == null)
            {
                throw new ValidacionException("La editorial seleccionada no existe.");
            }
            foreach (Autor autor in libro.Autores)
            {
                if (_autorDao.ObtenerPorId(autor.IdAutor) == null)
                {
                    throw new ValidacionException("El autor \"" + autor.NombreCompleto + "\" ya no existe.");
                }
            }

            if (libro.IdLibro == 0)
            {
                _dao.Insertar(libro);
            }
            else
            {
                if (_dao.ObtenerPorId(libro.IdLibro) == null)
                {
                    throw new ValidacionException("El libro que intenta modificar ya no existe.");
                }
                // No se pueden dejar menos ejemplares de los que están prestados.
                int prestados = _dao.ContarPrestamos(libro.IdLibro, true);
                if (libro.Existencias < prestados)
                {
                    throw new ValidacionException(
                        "Las existencias no pueden ser menores a los ejemplares prestados (" + prestados + ").");
                }
                _dao.Actualizar(libro);
            }
        }

        /// <summary>Un libro con préstamos registrados no se elimina para conservar el historial.</summary>
        public void Eliminar(int idLibro)
        {
            if (idLibro <= 0)
            {
                throw new ValidacionException("Seleccione un libro de la lista.");
            }
            int prestamos = _dao.ContarPrestamos(idLibro, false);
            if (prestamos > 0)
            {
                throw new ValidacionException(
                    "No se puede eliminar el libro porque tiene " + prestamos +
                    " préstamo(s) registrado(s) en el historial.");
            }
            _dao.Eliminar(idLibro);
        }

        private static void Validar(Libro libro)
        {
            if (libro == null) throw new ValidacionException("No hay datos del libro.");
            Validador.Isbn(libro.Isbn);
            Validador.Requerido(libro.Titulo, "Título");
            Validador.LongitudMaxima(libro.Titulo, 150, "Título");
            Validador.AnioPublicacion(libro.AnioPublicacion);

            if (libro.Existencias < 0 || libro.Existencias > ExistenciasMaximas)
            {
                throw new ValidacionException("Las existencias deben estar entre 0 y " + ExistenciasMaximas + ".");
            }
            if (libro.IdEditorial <= 0)
            {
                throw new ValidacionException("Seleccione la editorial del libro.");
            }
            if (libro.Autores.Count == 0)
            {
                throw new ValidacionException("Seleccione al menos un autor del libro.");
            }
        }
    }
}
