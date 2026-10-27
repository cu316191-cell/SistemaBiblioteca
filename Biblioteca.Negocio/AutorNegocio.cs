using System.Collections.Generic;
using Biblioteca.Datos;
using Biblioteca.Entidades;

namespace Biblioteca.Negocio
{
    /// <summary>Reglas del módulo de autores.</summary>
    public class AutorNegocio
    {
        private readonly AutorDAO _dao = new AutorDAO();

        public List<Autor> Listar()
        {
            return _dao.Listar();
        }

        public List<Autor> Buscar(string texto)
        {
            return string.IsNullOrWhiteSpace(texto) ? _dao.Listar() : _dao.Buscar(texto.Trim());
        }

        public Autor ObtenerPorId(int id)
        {
            return _dao.ObtenerPorId(id);
        }

        /// <summary>Registra un autor nuevo o actualiza uno existente.</summary>
        public void Guardar(Autor autor)
        {
            Validar(autor);
            if (autor.IdAutor == 0)
            {
                _dao.Insertar(autor);
            }
            else
            {
                if (_dao.ObtenerPorId(autor.IdAutor) == null)
                {
                    throw new ValidacionException("El autor que intenta modificar ya no existe.");
                }
                _dao.Actualizar(autor);
            }
        }

        public void Eliminar(int idAutor)
        {
            if (idAutor <= 0)
            {
                throw new ValidacionException("Seleccione un autor de la lista.");
            }
            int libros = _dao.ContarLibros(idAutor);
            if (libros > 0)
            {
                throw new ValidacionException(
                    "No se puede eliminar el autor porque tiene " + libros +
                    " libro(s) registrado(s). Primero quite el autor de esos libros.");
            }
            _dao.Eliminar(idAutor);
        }

        private static void Validar(Autor autor)
        {
            if (autor == null) throw new ValidacionException("No hay datos del autor.");
            Validador.Nombre(autor.Nombre, 60, "Nombre");
            Validador.Nombre(autor.Apellido, 60, "Apellido");
            Validador.TextoOpcionalLetras(autor.Nacionalidad, 50, "Nacionalidad");
        }
    }
}
