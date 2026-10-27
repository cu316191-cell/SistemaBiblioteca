namespace Biblioteca.Entidades
{
    /// <summary>
    /// Clase base para las personas del sistema (autores y usuarios).
    /// Reúne los datos que ambos comparten: nombre y apellido.
    /// Es abstracta porque en el sistema no existe una "persona" suelta,
    /// siempre es un autor o un usuario.
    /// </summary>
    public abstract class Persona
    {
        private string _nombre = string.Empty;
        private string _apellido = string.Empty;

        protected Persona()
        {
        }

        protected Persona(string nombre, string apellido)
        {
            Nombre = nombre;
            Apellido = apellido;
        }

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = (value ?? string.Empty).Trim(); }
        }

        public string Apellido
        {
            get { return _apellido; }
            set { _apellido = (value ?? string.Empty).Trim(); }
        }

        public string NombreCompleto
        {
            get { return (Nombre + " " + Apellido).Trim(); }
        }

        /// <summary>Tipo de persona (Autor o Usuario). Cada clase hija lo define.</summary>
        public abstract string TipoPersona { get; }

        /// <summary>
        /// Descripción corta de la persona. Cada clase hija la sobrescribe
        /// para mostrar sus propios datos (polimorfismo).
        /// </summary>
        public abstract string ObtenerDescripcion();

        public override string ToString()
        {
            return NombreCompleto;
        }
    }
}
