using System;
using System.Threading;
using System.Windows.Forms;
using Biblioteca.Presentacion.Formularios;
using Biblioteca.Presentacion.Utilidades;

namespace Biblioteca.Presentacion
{
    internal static class Program
    {
        /// <summary>Punto de entrada principal de la aplicación.</summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Cualquier error que no se haya controlado se muestra en un mensaje
            // en lugar de cerrar la aplicación.
            Application.ThreadException += AlOcurrirError;
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            Application.Run(new FrmPrincipal());
        }

        private static void AlOcurrirError(object sender, ThreadExceptionEventArgs e)
        {
            Mensajes.Error("Ocurrió un error inesperado:\n" + e.Exception.Message);
        }
    }
}
