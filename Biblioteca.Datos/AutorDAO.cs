using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Biblioteca.Entidades;

namespace Biblioteca.Datos
{
    /// <summary>Acceso a la tabla Autores.</summary>
    public class AutorDAO : IRepositorio<Autor>
    {
        private const string SelectBase =
            "SELECT IdAutor, Nombre, Apellido, Nacionalidad FROM Autores ";

        public List<Autor> Listar()
        {
            return Consultar(SelectBase + "ORDER BY Apellido, Nombre", null);
        }

        /// <summary>Busca autores cuyo nombre, apellido o nacionalidad contenga el texto.</summary>
        public List<Autor> Buscar(string texto)
        {
            return Consultar(
                SelectBase +
                "WHERE Nombre LIKE @texto OR Apellido LIKE @texto OR Nacionalidad LIKE @texto " +
                "ORDER BY Apellido, Nombre",
                cmd => Util.AgregarParametro(cmd, "@texto", SqlDbType.NVarChar, 62, "%" + texto + "%"));
        }

        public Autor ObtenerPorId(int id)
        {
            List<Autor> lista = Consultar(SelectBase + "WHERE IdAutor = @id",
                cmd => Util.AgregarParametro(cmd, "@id", SqlDbType.Int, id));
            return lista.Count > 0 ? lista[0] : null;
        }

        public int Insertar(Autor autor)
        {
            const string sql =
                "INSERT INTO Autores (Nombre, Apellido, Nacionalidad) " +
                "VALUES (@nombre, @apellido, @nacionalidad); " +
                "SELECT CAST(SCOPE_IDENTITY() AS INT);";
            try
            {
                using (SqlConnection cn = Conexion.CrearConexion())
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    AgregarParametros(cmd, autor);
                    cn.Open();
                    autor.IdAutor = (int)cmd.ExecuteScalar();
                    return autor.IdAutor;
                }
            }
            catch (SqlException ex)
            {
                throw DatosException.DesdeSql(ex);
            }
        }

        public void Actualizar(Autor autor)
        {
            const string sql =
                "UPDATE Autores SET Nombre = @nombre, Apellido = @apellido, Nacionalidad = @nacionalidad " +
                "WHERE IdAutor = @id";
            try
            {
                using (SqlConnection cn = Conexion.CrearConexion())
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    AgregarParametros(cmd, autor);
                    Util.AgregarParametro(cmd, "@id", SqlDbType.Int, autor.IdAutor);
                    cn.Open();
                    cmd.ExecuteNonQuery();
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
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Autores WHERE IdAutor = @id", cn))
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

        /// <summary>Cuántos libros tiene registrados el autor.</summary>
        public int ContarLibros(int idAutor)
        {
            try
            {
                using (SqlConnection cn = Conexion.CrearConexion())
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT COUNT(*) FROM LibroAutor WHERE IdAutor = @id", cn))
                {
                    Util.AgregarParametro(cmd, "@id", SqlDbType.Int, idAutor);
                    cn.Open();
                    return (int)cmd.ExecuteScalar();
                }
            }
            catch (SqlException ex)
            {
                throw DatosException.DesdeSql(ex);
            }
        }

        private static void AgregarParametros(SqlCommand cmd, Autor autor)
        {
            Util.AgregarParametro(cmd, "@nombre", SqlDbType.NVarChar, 60, autor.Nombre);
            Util.AgregarParametro(cmd, "@apellido", SqlDbType.NVarChar, 60, autor.Apellido);
            Util.AgregarParametro(cmd, "@nacionalidad", SqlDbType.NVarChar, 50, autor.Nacionalidad);
        }

        private static List<Autor> Consultar(string sql, System.Action<SqlCommand> parametros)
        {
            List<Autor> lista = new List<Autor>();
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
                            lista.Add(new Autor(
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
            return lista;
        }
    }
}
