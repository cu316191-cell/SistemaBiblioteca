namespace Biblioteca.Entidades
{
    /// <summary>Autor de uno o varios libros.</summary>
    public class Autor : Persona
    {
        private string _nacionalidad;

        public Autor()
        {
        }

        public Autor(int idAutor, string nombre, string apellido, string nacionalidad)
            : base(nombre, apellido)
        {
            IdAutor = idAutor;
            Nacionalidad = nacionalidad;
        }

        public int IdAutor { get; set; }

        /// <summary>Campo opcional. Si llega vacío se guarda como null.</summary>
        public string Nacionalidad
        {
            get { return _nacionalidad; }
            set { _nacionalidad = string.IsNullOrWhiteSpace(value) ? null : value.Trim(); }
        }

        public override string TipoPersona
        {
            get { return "Autor"; }
        }

        public override string ObtenerDescripcion()
        {
            return string.IsNullOrEmpty(Nacionalidad)
                ? "Autor: " + NombreCompleto
                : "Autor: " + NombreCompleto + " (" + Nacionalidad + ")";
        }
    }
}
