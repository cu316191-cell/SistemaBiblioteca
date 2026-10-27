using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Biblioteca.Entidades;

namespace Biblioteca.Datos
{
    /// <summary>
    /// Acceso a las tablas Libros y LibroAutor.
    /// Las consultas usan la vista vw_LibrosDisponibilidad, que trae la
    /// editorial, los autores y los ejemplares disponibles de cada libro.
    /// </summary>
    public class LibroDAO : IRepositorio<Libro>
    {
        private const string SelectBase =
            "SELECT v.IdLibro, v.ISBN, v.Titulo, v.AnioPublicacion, v.IdEditorial, v.Editorial, " +
            "v.Autores, v.Existencias, v.Prestados FROM vw_LibrosDisponibilidad v ";

        public List<Libro> Listar()
        {
            return Consultar(SelectBase + "ORDER BY v.Titulo", null);
        }

        /// <summary>Libros que tienen al menos un ejemplar para prestar.</summary>
        public List<Libro> ListarDisponibles()
        {
            return Consultar(SelectBase + "WHERE v.Disponibles > 0 ORDER BY v.Titulo", null);
        }

        public List<Libro> Buscar(CriterioBusquedaLibro criterio, string texto)
        {
            string filtro;
            switch (criterio)
            {
                case CriterioBusquedaLibro.Codigo:
                    filtro = "WHERE v.ISBN LIKE @texto OR CAST(v.IdLibro AS VARCHAR(10)) = @exacto ";
                    break;
                case CriterioBusquedaLibro.Autor:
                    filtro = "WHERE EXISTS (SELECT 1 FROM LibroAutor la " +
                             "INNER JOIN Autores a ON a.IdAutor = la.IdAutor " +
                             "WHERE la.IdLibro = v.IdLibro AND (a.Nombre + N' ' + a.Apellido) LIKE @texto) ";
                    break;
                default:
                    filtro = "WHERE v.Titulo LIKE @texto ";
                    break;
            }

            return Consultar(SelectBase + filtro + "ORDER BY v.Titulo", cmd =>
            {
                Util.AgregarParametro(cmd, "@texto", SqlDbType.NVarChar, 152, "%" + texto + "%");
                Util.AgregarParametro(cmd, "@exacto", SqlDbType.VarChar, 150, texto);
            });
        }

        /// <summary>Trae un libro con la lista de sus autores.</summary>
        public Libro ObtenerPorId(int id)
        {
            List<Libro> lista = Consultar(SelectBase + "WHERE v.IdLibro = @id",
                cmd => Util.AgregarParametro(cmd, "@id", SqlDbType.Int, id));
            if (lista.Count == 0) return null;

            Libro libro = lista[0];
            const string sqlAutores =
                "SELECT a.IdAutor, a.Nombre, a.Apellido, a.Nacionalidad FROM LibroAutor la " +
                "INNER JOIN Autores a ON a.IdAutor = la.IdAutor WHERE la.IdLibro = @id " +
                "ORDER BY a.Apellido, a.Nombre";
            try
            {
                using (SqlConnection cn = Conexion.CrearConexion())
                using (SqlCommand cmd = new SqlCommand(sqlAutores, cn))
                {
                    Util.AgregarParametro(cmd, "@id", SqlDbType.Int, id);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            libro.AgregarAutor(new Autor(
                                Util.LeerEntero(dr, "IdAutor"),
                                Util.LeerTexto(dr, "Nombre"),
                                Util.LeerTexto(dr, "Apellido"),
                                Util.LeerTexto(dr, "Nacionalidad")));
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw DatosException.DesdeSql(ex);
            }
            return libro;
        }

        /// <summary>
        /// Guarda el libro y sus autores en una sola transacción:
        /// si algo falla, no queda guardado nada.
        /// </summary>
        public int Insertar(Libro libro)
        {
            const string sql =
                "INSERT INTO Libros (ISBN, Titulo, AnioPublicacion, Existencias, IdEditorial) " +
                "VALUES (@isbn, @titulo, @anio, @existencias, @idEditorial); " +
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
                            using (SqlCommand cmd = new SqlCommand(sql, cn, tx))
                            {
                                AgregarParametros(cmd, libro);
                                libro.IdLibro = (int)cmd.ExecuteScalar();
                            }
                            GuardarAutores(cn, tx, libro);
                            tx.Commit();
                            return libro.IdLibro;
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

        /// <summary>Actualiza el libro y reemplaza su lista de autores (en una transacción).</summary>
        public void Actualizar(Libro libro)
        {
            const string sql =
                "UPDATE Libros SET ISBN = @isbn, Titulo = @titulo, AnioPublicacion = @anio, " +
                "Existencias = @existencias, IdEditorial = @idEditorial WHERE IdLibro = @id";
            try
            {
                using (SqlConnection cn = Conexion.CrearConexion())
                {
                    cn.Open();
                    using (SqlTransaction tx = cn.BeginTransaction())
                    {
                        try
                        {
                            using (SqlCommand cmd = new SqlCommand(sql, cn, tx))
                            {
                                AgregarParametros(cmd, libro);
                                Util.AgregarParametro(cmd, "@id", SqlDbType.Int, libro.IdLibro);
                                cmd.ExecuteNonQuery();
                            }
                            using (SqlCommand cmd = new SqlCommand(
                                "DELETE FROM LibroAutor WHERE IdLibro = @id", cn, tx))
                            {
                                Util.AgregarParametro(cmd, "@id", SqlDbType.Int, libro.IdLibro);
                                cmd.ExecuteNonQuery();
                            }
                            GuardarAutores(cn, tx, libro);
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

        /// <summary>
        /// Elimina el libro. Sus filas en LibroAutor se borran solas
        /// (ON DELETE CASCADE). Si tiene préstamos, SQL Server lo impide.
        /// </summary>
        public void Eliminar(int id)
        {
            try
            {
                using (SqlConnection cn = Conexion.CrearConexion())
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Libros WHERE IdLibro = @id", cn))
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

        /// <summary>Indica si ya hay otro libro con ese ISBN.</summary>
        public bool ExisteIsbn(string isbn, int idExcluir)
        {
            try
            {
                using (SqlConnection cn = Conexion.CrearConexion())
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT COUNT(*) FROM Libros WHERE ISBN = @isbn AND IdLibro <> @id", cn))
                {
                    Util.AgregarParametro(cmd, "@isbn", SqlDbType.VarChar, 17, isbn);
                    Util.AgregarParametro(cmd, "@id", SqlDbType.Int, idExcluir);
                    cn.Open();
                    return (int)cmd.ExecuteScalar() > 0;
                }
            }
            catch (SqlException ex)
            {
                throw DatosException.DesdeSql(ex);
            }
        }

        /// <summary>Cuántos préstamos tiene el libro (si soloActivos es true, solo los que siguen prestados).</summary>
        public int ContarPrestamos(int idLibro, bool soloActivos)
        {
            string sql = "SELECT COUNT(*) FROM Prestamos WHERE IdLibro = @id" +
                         (soloActivos ? " AND Estado = 'Prestado'" : "");
            try
            {
                using (SqlConnection cn = Conexion.CrearConexion())
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    Util.AgregarParametro(cmd, "@id", SqlDbType.Int, idLibro);
                    cn.Open();
                    return (int)cmd.ExecuteScalar();
                }
            }
            catch (SqlException ex)
            {
                throw DatosException.DesdeSql(ex);
            }
        }

        private static void GuardarAutores(SqlConnection cn, SqlTransaction tx, Libro libro)
        {
            foreach (Autor autor in libro.Autores)
            {
                using (SqlCommand cmd = new SqlCommand(
                    "INSERT INTO LibroAutor (IdLibro, IdAutor) VALUES (@idLibro, @idAutor)", cn, tx))
                {
                    Util.AgregarParametro(cmd, "@idLibro", SqlDbType.Int, libro.IdLibro);
                    Util.AgregarParametro(cmd, "@idAutor", SqlDbType.Int, autor.IdAutor);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private static void AgregarParametros(SqlCommand cmd, Libro libro)
        {
            Util.AgregarParametro(cmd, "@isbn", SqlDbType.VarChar, 17, libro.Isbn);
            Util.AgregarParametro(cmd, "@titulo", SqlDbType.NVarChar, 150, libro.Titulo);
            Util.AgregarParametro(cmd, "@anio", SqlDbType.SmallInt,
                libro.AnioPublicacion.HasValue ? (object)(short)libro.AnioPublicacion.Value : null);
            Util.AgregarParametro(cmd, "@existencias", SqlDbType.Int, libro.Existencias);
            Util.AgregarParametro(cmd, "@idEditorial", SqlDbType.Int, libro.IdEditorial);
        }

        private static List<Libro> Consultar(string sql, System.Action<SqlCommand> parametros)
        {
            List<Libro> lista = new List<Libro>();
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
                            Libro libro = new Libro(
                                Util.LeerEntero(dr, "IdLibro"),
                                Util.LeerTexto(dr, "ISBN"),
                                Util.LeerTexto(dr, "Titulo"),
                                Util.LeerEnteroNulo(dr, "AnioPublicacion"),
                                Util.LeerEntero(dr, "Existencias"),
                                Util.LeerEntero(dr, "IdEditorial"));
                            libro.NombreEditorial = Util.LeerTexto(dr, "Editorial");
                            libro.NombresAutores = Util.LeerTexto(dr, "Autores");
                            libro.Prestados = Util.LeerEntero(dr, "Prestados");
                            lista.Add(libro);
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
