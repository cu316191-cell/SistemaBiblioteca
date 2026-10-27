namespace Biblioteca.Entidades
{
    /// <summary>Empresa que publica los libros.</summary>
    public class Editorial
    {
        private string _nombre = string.Empty;
        private string _pais;
        private string _telefono;
        private string _correo;

        public Editorial()
        {
        }

        public Editorial(int idEditorial, string nombre, string pais, string telefono, string correo)
        {
            IdEditorial = idEditorial;
            Nombre = nombre;
            Pais = pais;
            Telefono = telefono;
            Correo = correo;
        }

        public int IdEditorial { get; set; }

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = (value ?? string.Empty).Trim(); }
        }

        public string Pais
        {
            get { return _pais; }
            set { _pais = string.IsNullOrWhiteSpace(value) ? null : value.Trim(); }
        }

        public string Telefono
        {
            get { return _telefono; }
            set { _telefono = string.IsNullOrWhiteSpace(value) ? null : value.Trim(); }
        }

        public string Correo
        {
            get { return _correo; }
            set { _correo = string.IsNullOrWhiteSpace(value) ? null : value.Trim().ToLowerInvariant(); }
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
