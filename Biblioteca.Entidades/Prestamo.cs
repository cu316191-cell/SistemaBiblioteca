using System;

namespace Biblioteca.Entidades
{
    /// <summary>Valores permitidos para el estado de un préstamo.</summary>
    public static class EstadoPrestamo
    {
        public const string Prestado = "Prestado";
        public const string Devuelto = "Devuelto";
    }

    /// <summary>Préstamo de un ejemplar de un libro a un usuario.</summary>
    public class Prestamo
    {
        public Prestamo()
        {
            FechaPrestamo = DateTime.Today;
            FechaLimite = DateTime.Today.AddDays(14);
            Estado = EstadoPrestamo.Prestado;
        }

        public Prestamo(int idPrestamo, int idUsuario, int idLibro, DateTime fechaPrestamo,
                        DateTime fechaLimite, DateTime? fechaDevolucion, string estado)
        {
            IdPrestamo = idPrestamo;
            IdUsuario = idUsuario;
            IdLibro = idLibro;
            FechaPrestamo = fechaPrestamo;
            FechaLimite = fechaLimite;
            FechaDevolucion = fechaDevolucion;
            Estado = estado;
        }

        public int IdPrestamo { get; set; }
        public int IdUsuario { get; set; }
        public int IdLibro { get; set; }
        public DateTime FechaPrestamo { get; set; }

        /// <summary>Fecha en la que el usuario debe devolver el libro.</summary>
        public DateTime FechaLimite { get; set; }

        /// <summary>Fecha real de devolución. Es null mientras el libro siga prestado.</summary>
        public DateTime? FechaDevolucion { get; set; }

        public string Estado { get; set; }

        /// <summary>Nombre del usuario (solo para mostrar).</summary>
        public string NombreUsuario { get; set; }

        /// <summary>Título del libro (solo para mostrar).</summary>
        public string TituloLibro { get; set; }

        public bool EstaDevuelto
        {
            get { return Estado == EstadoPrestamo.Devuelto; }
        }

        /// <summary>Un préstamo está vencido si sigue prestado y ya pasó la fecha límite.</summary>
        public bool EstaVencido
        {
            get { return !EstaDevuelto && FechaLimite.Date < DateTime.Today; }
        }

        /// <summary>Texto que se muestra en la tabla: Prestado, Vencido o Devuelto.</summary>
        public string EstadoVisible
        {
            get { return EstaVencido ? "Vencido" : Estado; }
        }

        /// <summary>Marca el préstamo como devuelto en la fecha indicada.</summary>
        public void MarcarDevuelto(DateTime fecha)
        {
            FechaDevolucion = fecha.Date;
            Estado = EstadoPrestamo.Devuelto;
        }
    }
}
