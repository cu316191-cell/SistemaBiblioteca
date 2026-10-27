using System;
using System.Text.RegularExpressions;

namespace Biblioteca.Negocio
{
    /// <summary>Reglas de validación que usan varias clases de negocio.</summary>
    public static class Validador
    {
        private static readonly Regex SoloLetras = new Regex(@"^[\p{L} .'\-]+$");
        private static readonly Regex SoloDigitos = new Regex(@"^\d+$");
        private static readonly Regex Correo = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]{2,}$");
        private static readonly Regex FormatoIsbn = new Regex(@"^[0-9\-]+[0-9Xx]$");

        public static void Requerido(string valor, string campo)
        {
            if (string.IsNullOrWhiteSpace(valor))
            {
                throw new ValidacionException("El campo " + campo + " es obligatorio.");
            }
        }

        public static void LongitudMaxima(string valor, int maximo, string campo)
        {
            if (valor != null && valor.Length > maximo)
            {
                throw new ValidacionException(
                    "El campo " + campo + " admite máximo " + maximo + " caracteres.");
            }
        }

        /// <summary>Obligatorio, con longitud máxima y solo letras (para nombres y apellidos).</summary>
        public static void Nombre(string valor, int maximo, string campo)
        {
            Requerido(valor, campo);
            LongitudMaxima(valor, maximo, campo);
            if (!SoloLetras.IsMatch(valor))
            {
                throw new ValidacionException("El campo " + campo + " solo admite letras.");
            }
        }

        /// <summary>Texto opcional que, si se escribe, solo puede tener letras.</summary>
        public static void TextoOpcionalLetras(string valor, int maximo, string campo)
        {
            if (string.IsNullOrEmpty(valor)) return;
            LongitudMaxima(valor, maximo, campo);
            if (!SoloLetras.IsMatch(valor))
            {
                throw new ValidacionException("El campo " + campo + " solo admite letras.");
            }
        }

        public static void Telefono(string valor, string campo)
        {
            if (string.IsNullOrEmpty(valor)) return;
            if (!SoloDigitos.IsMatch(valor) || valor.Length < 7 || valor.Length > 15)
            {
                throw new ValidacionException(
                    "El campo " + campo + " debe tener solo números, entre 7 y 15 dígitos.");
            }
        }

        public static void CorreoOpcional(string valor, string campo)
        {
            if (string.IsNullOrEmpty(valor)) return;
            LongitudMaxima(valor, 100, campo);
            if (!Correo.IsMatch(valor))
            {
                throw new ValidacionException(
                    "El campo " + campo + " no tiene un formato válido (ejemplo: nombre@correo.com).");
            }
        }

        public static void Documento(string valor)
        {
            Requerido(valor, "Documento");
            if (!SoloDigitos.IsMatch(valor) || valor.Length < 6 || valor.Length > 15)
            {
                throw new ValidacionException("El documento debe tener solo números, entre 6 y 15 dígitos.");
            }
        }

        /// <summary>El ISBN puede llevar guiones y debe tener 10 o 13 dígitos.</summary>
        public static void Isbn(string valor)
        {
            Requerido(valor, "ISBN");
            LongitudMaxima(valor, 17, "ISBN");
            if (!FormatoIsbn.IsMatch(valor))
            {
                throw new ValidacionException("El ISBN solo admite números y guiones.");
            }
            int digitos = valor.Replace("-", string.Empty).Length;
            if (digitos != 10 && digitos != 13)
            {
                throw new ValidacionException("El ISBN debe tener 10 o 13 dígitos (sin contar los guiones).");
            }
        }

        public static void AnioPublicacion(int? anio)
        {
            if (!anio.HasValue) return;
            int maximo = DateTime.Today.Year + 1;
            if (anio.Value < 1450 || anio.Value > maximo)
            {
                throw new ValidacionException("El año de publicación debe estar entre 1450 y " + maximo + ".");
            }
        }
    }
}
