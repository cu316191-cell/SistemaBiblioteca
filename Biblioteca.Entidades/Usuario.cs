using System;

namespace Biblioteca.Entidades
{
    /// <summary>Persona registrada en la biblioteca que puede pedir libros prestados.</summary>
    public class Usuario : Persona
    {
        private string _documento = string.Empty;
        private string _telefono;
        private string _correo;

        public Usuario()
        {
            FechaRegistro = DateTime.Today;
        }

        public Usuario(int idUsuario, string documento, string nombre, string apellido,
                       string telefono, string correo, DateTime fechaRegistro)
            : base(nombre, apellido)
        {
            IdUsuario = idUsuario;
            Documento = documento;
            Telefono = telefono;
            Correo = correo;
            FechaRegistro = fechaRegistro;
        }

        public int IdUsuario { get; set; }

        public string Documento
        {
            get { return _documento; }
            set { _documento = (value ?? string.Empty).Trim(); }
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

        public DateTime FechaRegistro { get; set; }

        public override string TipoPersona
        {
            get { return "Usuario"; }
        }

        public override string ObtenerDescripcion()
        {
            return "Usuario: " + NombreCompleto + " - Doc. " + Documento;
        }

        /// <summary>Texto que se muestra en las listas desplegables.</summary>
        public override string ToString()
        {
            return Documento + " - " + NombreCompleto;
        }
    }
}
