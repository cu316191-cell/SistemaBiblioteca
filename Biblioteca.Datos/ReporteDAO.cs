using System.Data;
using System.Data.SqlClient;

namespace Biblioteca.Datos
{
    /// <summary>
    /// Consultas de solo lectura para el módulo de reportes.
    /// Cada método devuelve una tabla lista para mostrarse en pantalla.
    /// </summary>
    public class ReporteDAO
    {
        public DataTable LibrosRegistrados()
        {
            return Ejecutar(
                "SELECT IdLibro AS [Código], ISBN, Titulo AS [Título], Autores, Editorial, " +
                "AnioPublicacion AS [Año], Existencias, Prestados, Disponibles " +
                "FROM vw_LibrosDisponibilidad ORDER BY Titulo", null);
        }

        public DataTable AutoresConLibros()
        {
            return Ejecutar(
                "SELECT a.IdAutor AS [Código], a.Nombre, a.Apellido, a.Nacionalidad, " +
                "COUNT(la.IdLibro) AS [Libros registrados] " +
                "FROM Autores a LEFT JOIN LibroAutor la ON la.IdAutor = a.IdAutor " +
                "GROUP BY a.IdAutor, a.Nombre, a.Apellido, a.Nacionalidad " +
                "ORDER BY a.Apellido, a.Nombre", null);
        }

        public DataTable EditorialesConLibros()
        {
            return Ejecutar(
                "SELECT e.IdEditorial AS [Código], e.Nombre, e.Pais AS [País], e.Telefono AS [Teléfono], " +
                "e.Correo, COUNT(l.IdLibro) AS [Libros registrados] " +
                "FROM Editoriales e LEFT JOIN Libros l ON l.IdEditorial = e.IdEditorial " +
                "GROUP BY e.IdEditorial, e.Nombre, e.Pais, e.Telefono, e.Correo " +
                "ORDER BY e.Nombre", null);
        }

        public DataTable UsuariosRegistrados()
        {
            return Ejecutar(
                "SELECT u.IdUsuario AS [Código], u.Documento, u.Nombre, u.Apellido, " +
                "u.Telefono AS [Teléfono], u.Correo, u.FechaRegistro AS [Fecha de registro], " +
                "SUM(CASE WHEN p.Estado = 'Prestado' THEN 1 ELSE 0 END) AS [Préstamos activos], " +
                "COUNT(p.IdPrestamo) AS [Préstamos totales] " +
                "FROM Usuarios u LEFT JOIN Prestamos p ON p.IdUsuario = u.IdUsuario " +
                "GROUP BY u.IdUsuario, u.Documento, u.Nombre, u.Apellido, u.Telefono, u.Correo, u.FechaRegistro " +
                "ORDER BY u.Apellido, u.Nombre", null);
        }

        /// <summary>Préstamos según su estado: "Prestado", "Devuelto" o null para todos.</summary>
        public DataTable Prestamos(string estado, bool soloVencidos)
        {
            string where = "WHERE 1 = 1 ";
            if (estado != null) where += "AND p.Estado = @estado ";
            if (soloVencidos) where += "AND p.Estado = 'Prestado' AND p.FechaLimite < CAST(GETDATE() AS DATE) ";

            return Ejecutar(ConsultaPrestamos(where), cmd =>
            {
                if (estado != null)
                {
                    cmd.Parameters.Add("@estado", SqlDbType.VarChar, 10).Value = estado;
                }
            });
        }

        public DataTable HistorialUsuario(int idUsuario)
        {
            return Ejecutar(ConsultaPrestamos("WHERE p.IdUsuario = @idUsuario "),
                cmd => cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario);
        }

        /// <summary>Totales generales de la biblioteca (información consolidada).</summary>
        public DataTable Resumen()
        {
            const string sql =
                "SELECT N'Libros (títulos) registrados' AS Indicador, COUNT(*) AS Valor FROM Libros " +
                "UNION ALL SELECT N'Ejemplares en total', ISNULL(SUM(Existencias), 0) FROM Libros " +
                "UNION ALL SELECT N'Ejemplares disponibles', ISNULL(SUM(Disponibles), 0) FROM vw_LibrosDisponibilidad " +
                "UNION ALL SELECT N'Autores registrados', COUNT(*) FROM Autores " +
                "UNION ALL SELECT N'Editoriales registradas', COUNT(*) FROM Editoriales " +
                "UNION ALL SELECT N'Usuarios registrados', COUNT(*) FROM Usuarios " +
                "UNION ALL SELECT N'Préstamos registrados', COUNT(*) FROM Prestamos " +
                "UNION ALL SELECT N'Préstamos activos', COUNT(*) FROM Prestamos WHERE Estado = 'Prestado' " +
                "UNION ALL SELECT N'Préstamos vencidos', COUNT(*) FROM Prestamos " +
                "    WHERE Estado = 'Prestado' AND FechaLimite < CAST(GETDATE() AS DATE) " +
                "UNION ALL SELECT N'Préstamos devueltos', COUNT(*) FROM Prestamos WHERE Estado = 'Devuelto'";
            return Ejecutar(sql, null);
        }

        private static string ConsultaPrestamos(string where)
        {
            return
                "SELECT p.IdPrestamo AS [Código], u.Documento, u.Nombre + N' ' + u.Apellido AS Usuario, " +
                "l.Titulo AS Libro, p.FechaPrestamo AS [Fecha préstamo], p.FechaLimite AS [Fecha límite], " +
                "p.FechaDevolucion AS [Fecha devolución], " +
                "CASE WHEN p.Estado = 'Prestado' AND p.FechaLimite < CAST(GETDATE() AS DATE) " +
                "     THEN 'Vencido' ELSE p.Estado END AS Estado, " +
                "CASE WHEN p.Estado = 'Prestado' AND p.FechaLimite < CAST(GETDATE() AS DATE) " +
                "     THEN DATEDIFF(DAY, p.FechaLimite, CAST(GETDATE() AS DATE)) " +
                "     WHEN p.Estado = 'Devuelto' AND p.FechaDevolucion > p.FechaLimite " +
                "     THEN DATEDIFF(DAY, p.FechaLimite, p.FechaDevolucion) " +
                "     ELSE 0 END AS [Días de retraso] " +
                "FROM Prestamos p " +
                "INNER JOIN Usuarios u ON u.IdUsuario = p.IdUsuario " +
                "INNER JOIN Libros l ON l.IdLibro = p.IdLibro " +
                where +
                "ORDER BY p.FechaPrestamo DESC, p.IdPrestamo DESC";
        }

        private static DataTable Ejecutar(string sql, System.Action<SqlCommand> parametros)
        {
            try
            {
                using (SqlConnection cn = Conexion.CrearConexion())
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    if (parametros != null) parametros(cmd);
                    return Util.LlenarTabla(cmd);
                }
            }
            catch (SqlException ex)
            {
                throw DatosException.DesdeSql(ex);
            }
        }
    }
}
