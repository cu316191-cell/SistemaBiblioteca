using System;
using System.Windows.Forms;
using Biblioteca.Negocio;

namespace Biblioteca.Presentacion.Utilidades
{
    /// <summary>Mensajes estándar que muestra la aplicación.</summary>
    public static class Mensajes
    {
        private const string Titulo = "Sistema de Gestión de Biblioteca";

        public static void Exito(string mensaje)
        {
            MessageBox.Show(mensaje, Titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void Advertencia(string mensaje)
        {
            MessageBox.Show(mensaje, Titulo, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void Error(string mensaje)
        {
            MessageBox.Show(mensaje, Titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static bool Confirmar(string mensaje)
        {
            return MessageBox.Show(mensaje, Titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                   MessageBoxDefaultButton.Button2) == DialogResult.Yes;
        }

        /// <summary>
        /// Muestra una excepción: las de validación como advertencia y las
        /// demás (base de datos, conexión) como error.
        /// </summary>
        public static void MostrarExcepcion(Exception ex)
        {
            if (ex is ValidacionException)
            {
                Advertencia(ex.Message);
            }
            else
            {
                Error(ex.Message);
            }
        }
    }
}
