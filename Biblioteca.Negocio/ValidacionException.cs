using System;

namespace Biblioteca.Negocio
{
    /// <summary>
    /// Se lanza cuando los datos ingresados no cumplen una regla del sistema
    /// (campo vacío, formato incorrecto, dato repetido, etc.).
    /// La interfaz la muestra como advertencia, no como error grave.
    /// </summary>
    public class ValidacionException : Exception
    {
        public ValidacionException(string mensaje) : base(mensaje)
        {
        }
    }
}
