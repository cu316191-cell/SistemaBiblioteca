using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Biblioteca.Entidades;

namespace Biblioteca.Datos
{
    /// <summary>Acceso a la tabla Prestamos.</summary>
    public class PrestamoDAO : IRepositorio<Prestamo>
    {
        private const string SelectBase =
            "SELECT p.IdPrestamo, p.IdUsuario, p.IdLibro, p.FechaPrestamo, p.FechaLimite, " +
            "p.FechaDevolucion, p.Estado, u.Documento + ' - ' + u.Nombre + N' ' + u.Apellido AS NombreUsuario, " +
            "l.Titulo AS TituloLibro " +
            "FROM Prestamos p " +
            "INNER JOIN Usuarios u ON u.IdUsuario = p.IdUsuario " +
            "INNER JOIN Libros l ON l.IdLibro = p.IdLibro ";

        private const string OrdenBase = "ORDER BY p.FechaPrestamo DESC, p.IdPrestamo DESC";

        public List<Prestamo> Listar()
        {
            return Listar(FiltroPrestamo.Todos);
        }

        public List<Prestamo> Listar(FiltroPrestamo filtro)
        {
            string where;
            switch (filtro)
            {
                case FiltroPrestamo.Prestados:
                    where = "WHERE p.Estado = 'Prestado' ";
                    break;
                case FiltroPrestamo.Devueltos:
                    where = "WHERE p.Estado = 'Devuelto' ";
                    break;
                case FiltroPrestamo.Vencidos:
                    where = "WHERE p.Estado = 'Prestado' AND p.FechaLimite < CAST(GETDATE() AS DATE) ";
                    break;
                default:
                    where = string.Empty;
                    break;
            }
            return Consultar(SelectBase + where + OrdenBase, null);
        }

        /// <summary>Préstamos de un usuario (historial).</summary>
        public List<Prestamo> ListarPorUsuario(int idUsuario)
        {
            return Consultar(SelectBase + "WHERE p.IdUsuario = @idUsuario " + OrdenBase,
                cmd => Util.AgregarParametro(cmd, "@idUsuario", SqlDbType.Int, idUsuario));
        }

        public Prestamo ObtenerPorId(int id)
        {
            List<Prestamo> lista = Consultar(SelectBase + "WHERE p.IdPrestamo = @id",
                cmd => Util.AgregarParametro(cmd, "@id", SqlDbType.Int, id));
            return lista.Count > 0 ? lista[0] : null;
        }

        /// <summary>
        /// Registra un préstamo. Dentro de una transacción vuelve a comprobar que
        /// el libro tenga ejemplares disponibles, para que dos préstamos al mismo
        /// tiempo no se lleven el último ejemplar.
        /// </summary>
        public int Insertar(Prestamo prestamo)
        {
            const string sql =
                "INSERT INTO Prestamos (IdUsuario, IdLibro, FechaPrestamo, FechaLimite, FechaDevolucion, Estado) " +
                "VALUES (@idUsuario, @idLibro, @fechaPrestamo, @fechaLimite, NULL, 'Prestado'); " +
                "SELECT CAST(SCOPE_IDENTITY() AS INT);";
            try
            {
                using (SqlConnection cn = Conexion.CrearConexion())
                {
                    cn.Open();
                    using (SqlTransaction tx = cn.BeginTransaction())
                    {
                        try
                        {
                            if (DisponiblesConBloqueo(cn, tx, prestamo.IdLibro) <= 0)
                            {
                                throw new DatosException("El libro no tiene ejemplares disponibles para prestar.");
                            }
                            using (SqlCommand cmd = new SqlCommand(sql, cn, tx))
                            {
                                AgregarParametros(cmd, prestamo);
                                prestamo.IdPrestamo = (int)cmd.ExecuteScalar();
                            }
                            tx.Commit();
                            prestamo.Estado = EstadoPrestamo.Prestado;
                            prestamo.FechaDevolucion = null;
                            return prestamo.IdPrestamo;
                        }
                        catch
                        {
                            tx.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw DatosException.DesdeSql(ex);
            }
        }

        /// <summary>
        /// Modifica un préstamo que sigue activo (usuario, libro y fechas).
        /// Si se cambia el libro, comprueba que el nuevo tenga ejemplares disponibles.
        /// </summary>
        public void Actualizar(Prestamo prestamo)
        {
            const string sql =
                "UPDATE Prestamos SET IdUsuario = @idUsuario, IdLibro = @idLibro, " +
                "FechaPrestamo = @fechaPrestamo, FechaLimite = @fechaLimite " +
                "WHERE IdPrestamo = @id AND Estado = 'Prestado'";
            try
            {
                using (SqlConnection cn = Conexion.CrearConexion())
                {
                    cn.Open();
                    using (SqlTransaction tx = cn.BeginTransaction())
                    {
                        try
                        {
                            int idLibroActual;
                            using (SqlCommand cmd = new SqlCommand(
                                "SELECT IdLibro FROM Prestamos WHERE IdPrestamo = @id", cn, tx))
                            {
                                Util.AgregarParametro(cmd, "@id", SqlDbType.Int, prestamo.IdPrestamo);
                                object valor = cmd.ExecuteScalar();
                                if (valor == null)
                                {
                                    throw new DatosException("El préstamo ya no existe.");
                                }
                                idLibroActual = (int)valor;
                            }

                            if (idLibroActual != prestamo.IdLibro &&
                                DisponiblesConBloqueo(cn, tx, prestamo.IdLibro) <= 0)
                            {
                                throw new DatosException("El nuevo libro no tiene ejemplares disponibles.");
                            }

                            using (SqlCommand cmd = new SqlCommand(sql, cn, tx))
                            {
                                AgregarParametros(cmd, prestamo);
                                Util.AgregarParametro(cmd, "@id", SqlDbType.Int, prestamo.IdPrestamo);
                                if (cmd.ExecuteNonQuery() == 0)
                                {
                                    throw new DatosException("Solo se pueden modificar préstamos que siguen activos.");
                                }
                            }
                            tx.Commit();
                        }
                        catch
                        {
                            tx.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw DatosException.DesdeSql(ex);
            }
        }

        /// <summary>Marca el préstamo como devuelto y guarda la fecha de devolución.</summary>
        public void RegistrarDevolucion(int idPrestamo, DateTime fechaDevolucion)
        {
            const string sql =
                "UPDATE Prestamos SET FechaDevolucion = @fecha, Estado = 'Devuelto' " +
                "WHERE IdPrestamo = @id AND Estado = 'Prestado'";
            try
            {
                using (SqlConnection cn = Conexion.CrearConexion())
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    Util.AgregarParametro(cmd, "@fecha", SqlDbType.Date, fechaDevolucion.Date);
                    Util.AgregarParametro(cmd, "@id", SqlDbType.Int, idPrestamo);
                    cn.Open();
                    if (cmd.ExecuteNonQuery() == 0)
                    {
                        throw new DatosException("El préstamo no existe o ya fue devuelto.");
                    }
                }
            }
            catch (SqlException ex)
            {
                throw DatosException.DesdeSql(ex);
            }
        }

        public void Eliminar(int id)
        {
            try
            {
                using (SqlConnection cn = Conexion.CrearConexion())
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Prestamos WHERE IdPrestamo = @id", cn))
                {
                    Util.AgregarParametro(cmd, "@id", SqlDbType.Int, id);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw DatosException.DesdeSql(ex);
            }
        }

        /// <summary>
        /// Ejemplares disponibles de un libro. Bloquea la fila del libro hasta que
        /// termine la transacción para evitar préstamos simultáneos del último ejemplar.
        /// </summary>
        private static int DisponiblesConBloqueo(SqlConnection cn, SqlTransaction tx, int idLibro)
        {
            const string sql =
                "SELECT l.Existencias - (SELECT COUNT(*) FROM Prestamos p " +
                "WHERE p.IdLibro = l.IdLibro AND p.Estado = 'Prestado') " +
                "FROM Libros l WITH (UPDLOCK, HOLDLOCK) WHERE l.IdLibro = @idLibro";
            using (SqlCommand cmd = new SqlCommand(sql, cn, tx))
            {
                Util.AgregarParametro(cmd, "@idLibro", SqlDbType.Int, idLibro);
                object valor = cmd.ExecuteScalar();
                if (valor == null)
                {
                    throw new DatosException("El libro seleccionado no existe.");
                }
                return Convert.ToInt32(valor);
            }
        }

        private static void AgregarParametros(SqlCommand cmd, Prestamo prestamo)
        {
            Util.AgregarParametro(cmd, "@idUsuario", SqlDbType.Int, prestamo.IdUsuario);
            Util.AgregarParametro(cmd, "@idLibro", SqlDbType.Int, prestamo.IdLibro);
            Util.AgregarParametro(cmd, "@fechaPrestamo", SqlDbType.Date, prestamo.FechaPrestamo.Date);
            Util.AgregarParametro(cmd, "@fechaLimite", SqlDbType.Date, prestamo.FechaLimite.Date);
        }

        private static List<Prestamo> Consultar(string sql, Action<SqlCommand> parametros)
        {
            List<Prestamo> lista = new List<Prestamo>();
            try
            {
                using (SqlConnection cn = Conexion.CrearConexion())
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    if (parametros != null) parametros(cmd);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Prestamo p = new Prestamo(
                                Util.LeerEntero(dr, "IdPrestamo"),
                                Util.LeerEntero(dr, "IdUsuario"),
                                Util.LeerEntero(dr, "IdLibro"),
                                Util.LeerFecha(dr, "FechaPrestamo"),
                                Util.LeerFecha(dr, "FechaLimite"),
                                Util.LeerFechaNula(dr, "FechaDevolucion"),
                                Util.LeerTexto(dr, "Estado"));
                            p.NombreUsuario = Util.LeerTexto(dr, "NombreUsuario");
                            p.TituloLibro = Util.LeerTexto(dr, "TituloLibro");
                            lista.Add(p);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw DatosException.DesdeSql(ex);
            }
            return lista;
        }
    }
}
