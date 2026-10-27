using System;
using System.Collections.Generic;
using System.Linq;
using Biblioteca.Datos;
using Biblioteca.Entidades;

namespace Biblioteca.Negocio
{
    /// <summary>
    /// Reglas de préstamos y devoluciones:
    /// - El usuario y el libro deben existir.
    /// - El libro debe tener ejemplares disponibles.
    /// - Un usuario puede tener máximo 3 préstamos activos.
    /// - Un usuario con préstamos vencidos no puede pedir más libros.
    /// - Un usuario no puede tener dos préstamos activos del mismo libro.
    /// - El plazo del préstamo es de máximo 30 días.
    /// </summary>
    public class PrestamoNegocio
    {
        public const int MaximoPrestamosActivos = 3;
        public const int DiasMaximosPrestamo = 30;
        public const int DiasPorDefecto = 14;

        private readonly PrestamoDAO _dao = new PrestamoDAO();
        private readonly UsuarioDAO _usuarioDao = new UsuarioDAO();
        private readonly LibroDAO _libroDao = new LibroDAO();

        public List<Prestamo> Listar(FiltroPrestamo filtro)
        {
            return _dao.Listar(filtro);
        }

        public Prestamo ObtenerPorId(int id)
        {
            return _dao.ObtenerPorId(id);
        }

        public void Registrar(Prestamo prestamo)
        {
            ValidarDatos(prestamo);

            Libro libro = _libroDao.ObtenerPorId(prestamo.IdLibro);
            if (!libro.TieneDisponibles())
            {
                throw new ValidacionException("El libro \"" + libro.Titulo + "\" no tiene ejemplares disponibles.");
            }

            List<Prestamo> activos = PrestamosActivosDe(prestamo.IdUsuario);
            if (activos.Any(p => p.EstaVencido))
            {
                throw new ValidacionException(
                    "El usuario tiene préstamos vencidos. Debe devolverlos antes de pedir otro libro.");
            }
            ValidarLimites(activos, prestamo);

            _dao.Insertar(prestamo);
        }

        /// <summary>Modifica un préstamo que todavía no ha sido devuelto.</summary>
        public void Actualizar(Prestamo prestamo)
        {
            if (prestamo == null || prestamo.IdPrestamo <= 0)
            {
                throw new ValidacionException("Seleccione un préstamo de la lista.");
            }
            Prestamo actual = _dao.ObtenerPorId(prestamo.IdPrestamo);
            if (actual == null)
            {
                throw new ValidacionException("El préstamo ya no existe.");
            }
            if (actual.EstaDevuelto)
            {
                throw new ValidacionException("Un préstamo devuelto no se puede modificar.");
            }

            ValidarDatos(prestamo);

            if (actual.IdLibro != prestamo.IdLibro && !_libroDao.ObtenerPorId(prestamo.IdLibro).TieneDisponibles())
            {
                throw new ValidacionException("El libro seleccionado no tiene ejemplares disponibles.");
            }

            List<Prestamo> activos = PrestamosActivosDe(prestamo.IdUsuario)
                .Where(p => p.IdPrestamo != prestamo.IdPrestamo).ToList();
            ValidarLimites(activos, prestamo);

            _dao.Actualizar(prestamo);
        }

        public void RegistrarDevolucion(int idPrestamo, DateTime fechaDevolucion)
        {
            if (idPrestamo <= 0)
            {
                throw new ValidacionException("Seleccione un préstamo de la lista.");
            }
            Prestamo prestamo = _dao.ObtenerPorId(idPrestamo);
            if (prestamo == null)
            {
                throw new ValidacionException("El préstamo ya no existe.");
            }
            if (prestamo.EstaDevuelto)
            {
                throw new ValidacionException("Este préstamo ya fue devuelto.");
            }
            if (fechaDevolucion.Date < prestamo.FechaPrestamo.Date)
            {
                throw new ValidacionException("La fecha de devolución no puede ser anterior a la fecha del préstamo.");
            }
            if (fechaDevolucion.Date > DateTime.Today)
            {
                throw new ValidacionException("La fecha de devolución no puede ser una fecha futura.");
            }
            _dao.RegistrarDevolucion(idPrestamo, fechaDevolucion);
        }

        public void Eliminar(int idPrestamo)
        {
            if (idPrestamo <= 0)
            {
                throw new ValidacionException("Seleccione un préstamo de la lista.");
            }
            if (_dao.ObtenerPorId(idPrestamo) == null)
            {
                throw new ValidacionException("El préstamo ya no existe.");
            }
            _dao.Eliminar(idPrestamo);
        }

        private List<Prestamo> PrestamosActivosDe(int idUsuario)
        {
            return _dao.ListarPorUsuario(idUsuario).Where(p => !p.EstaDevuelto).ToList();
        }

        private static void ValidarLimites(List<Prestamo> activos, Prestamo prestamo)
        {
            if (activos.Count >= MaximoPrestamosActivos)
            {
                throw new ValidacionException(
                    "El usuario ya tiene " + MaximoPrestamosActivos + " préstamos activos, que es el máximo permitido.");
            }
            if (activos.Any(p => p.IdLibro == prestamo.IdLibro))
            {
                throw new ValidacionException("El usuario ya tiene prestado un ejemplar de este libro.");
            }
        }

        private void ValidarDatos(Prestamo prestamo)
        {
            if (prestamo == null) throw new ValidacionException("No hay datos del préstamo.");
            if (prestamo.IdUsuario <= 0) throw new ValidacionException("Seleccione el usuario.");
            if (prestamo.IdLibro <= 0) throw new ValidacionException("Seleccione el libro.");

            if (_usuarioDao.ObtenerPorId(prestamo.IdUsuario) == null)
            {
                throw new ValidacionException("El usuario seleccionado no existe.");
            }
            if (_libroDao.ObtenerPorId(prestamo.IdLibro) == null)
            {
                throw new ValidacionException("El libro seleccionado no existe.");
            }
            if (prestamo.FechaPrestamo.Date > DateTime.Today)
            {
                throw new ValidacionException("La fecha del préstamo no puede ser una fecha futura.");
            }
            if (prestamo.FechaLimite.Date < prestamo.FechaPrestamo.Date)
            {
                throw new ValidacionException("La fecha límite no puede ser anterior a la fecha del préstamo.");
            }
            if ((prestamo.FechaLimite.Date - prestamo.FechaPrestamo.Date).TotalDays > DiasMaximosPrestamo)
            {
                throw new ValidacionException(
                    "El plazo del préstamo no puede superar " + DiasMaximosPrestamo + " días.");
            }
        }
    }
}
