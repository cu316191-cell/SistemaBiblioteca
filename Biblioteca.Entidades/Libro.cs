using System.Collections.Generic;
using System.Linq;

namespace Biblioteca.Entidades
{
    /// <summary>Libro de la biblioteca con su editorial y sus autores.</summary>
    public class Libro
    {
        private string _isbn = string.Empty;
        private string _titulo = string.Empty;
        private readonly List<Autor> _autores = new List<Autor>();

        public Libro()
        {
            Existencias = 1;
        }

        public Libro(int idLibro, string isbn, string titulo, int? anioPublicacion,
                     int existencias, int idEditorial)
        {
            IdLibro = idLibro;
            Isbn = isbn;
            Titulo = titulo;
            AnioPublicacion = anioPublicacion;
            Existencias = existencias;
            IdEditorial = idEditorial;
        }

        public int IdLibro { get; set; }

        public string Isbn
        {
            get { return _isbn; }
            set { _isbn = (value ?? string.Empty).Trim(); }
        }

        public string Titulo
        {
            get { return _titulo; }
            set { _titulo = (value ?? string.Empty).Trim(); }
        }

        /// <summary>Año de publicación. Es opcional, por eso admite null.</summary>
        public int? AnioPublicacion { get; set; }

        /// <summary>Cantidad total de ejemplares que tiene la biblioteca.</summary>
        public int Existencias { get; set; }

        public int IdEditorial { get; set; }

        /// <summary>Nombre de la editorial (solo para mostrar).</summary>
        public string NombreEditorial { get; set; }

        /// <summary>Ejemplares que están prestados en este momento.</summary>
        public int Prestados { get; set; }

        /// <summary>Ejemplares que se pueden prestar.</summary>
        public int Disponibles
        {
            get { return Existencias - Prestados; }
        }

        /// <summary>Autores del libro (colección).</summary>
        public List<Autor> Autores
        {
            get { return _autores; }
        }

        /// <summary>Nombres de los autores separados por coma (solo para mostrar).</summary>
        public string NombresAutores { get; set; }

        public void AgregarAutor(Autor autor)
        {
            if (autor != null && !_autores.Any(a => a.IdAutor == autor.IdAutor))
            {
                _autores.Add(autor);
            }
        }

        public void LimpiarAutores()
        {
            _autores.Clear();
        }

        public bool TieneDisponibles()
        {
            return Disponibles > 0;
        }

        /// <summary>Texto que se muestra en las listas desplegables.</summary>
        public string TextoLista
        {
            get { return Titulo + " (disponibles: " + Disponibles + ")"; }
        }

        public override string ToString()
        {
            return TextoLista;
        }
    }
}
