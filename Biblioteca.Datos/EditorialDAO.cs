using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Biblioteca.Entidades;

namespace Biblioteca.Datos
{
    /// <summary>Acceso a la tabla Editoriales.</summary>
    public class EditorialDAO : IRepositorio<Editorial>
    {
        private const string SelectBase =
            "SELECT IdEditorial, Nombre, Pais, Telefono, Correo FROM Editoriales ";

        public List<Editorial> Listar()
        {
            return Consultar(SelectBase + "ORDER BY Nombre", null);
        }

        public List<Editorial> Buscar(string texto)
        {
            return Consultar(
                SelectBase + "WHERE Nombre LIKE @texto OR Pais LIKE @texto ORDER BY Nombre",
                cmd => Util.AgregarParametro(cmd, "@texto", SqlDbType.NVarChar, 102, "%" + texto + "%"));
        }

        public Editorial ObtenerPorId(int id)
        {
            List<Editorial> lista = Consultar(SelectBase + "WHERE IdEditorial = @id",
                cmd => Util.AgregarParametro(cmd, "@id", SqlDbType.Int, id));
            return lista.Count > 0 ? lista[0] : null;
        }

        public int Insertar(Editorial editorial)
        {
            const string sql =
                "INSERT INTO Editoriales (Nombre, Pais, Telefono, Correo) " +
                "VALUES (@nombre, @pais, @telefono, @correo); " +
                "SELECT CAST(SCOPE_IDENTITY() AS INT);";
            try
            {
                using (SqlConnection cn = Conexion.CrearConexion())
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    AgregarParametros(cmd, editorial);
                    cn.Open();
                    editorial.IdEditorial = (int)cmd.ExecuteScalar();
                    return editorial.IdEditorial;
                }
            }
            catch (SqlException ex)
            {
                throw DatosException.DesdeSql(ex);
            }
        }

        public void Actualizar(Editorial editorial)
        {
            const string sql =
                "UPDATE Editoriales SET Nombre = @nombre, Pais = @pais, Telefono = @telefono, Correo = @correo " +
                "WHERE IdEditorial = @id";
            try
            {
                using (SqlConnection cn = Conexion.CrearConexion())
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    AgregarParametros(cmd, editorial);
                    Util.AgregarParametro(cmd, "@id", SqlDbType.Int, editorial.IdEditorial);
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
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Editoriales WHERE IdEditorial = @id", cn))
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

        /// <summary>Indica si ya hay otra editorial con ese nombre.</summary>
        public bool ExisteNombre(string nombre, int idExcluir)
        {
            try
            {
                using (SqlConnection cn = Conexion.CrearConexion())
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT COUNT(*) FROM Editoriales WHERE Nombre = @nombre AND IdEditorial <> @id", cn))
                {
                    Util.AgregarParametro(cmd, "@nombre", SqlDbType.NVarChar, 100, nombre);
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

        public int ContarLibros(int idEditorial)
        {
            try
            {
                using (SqlConnection cn = Conexion.CrearConexion())
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT COUNT(*) FROM Libros WHERE IdEditorial = @id", cn))
                {
                    Util.AgregarParametro(cmd, "@id", SqlDbType.Int, idEditorial);
                    cn.Open();
                    return (int)cmd.ExecuteScalar();
                }
            }
            catch (SqlException ex)
            {
                throw DatosException.DesdeSql(ex);
            }
        }

        private static void AgregarParametros(SqlCommand cmd, Editorial editorial)
        {
            Util.AgregarParametro(cmd, "@nombre", SqlDbType.NVarChar, 100, editorial.Nombre);
            Util.AgregarParametro(cmd, "@pais", SqlDbType.NVarChar, 50, editorial.Pais);
            Util.AgregarParametro(cmd, "@telefono", SqlDbType.VarChar, 15, editorial.Telefono);
            Util.AgregarParametro(cmd, "@correo", SqlDbType.VarChar, 100, editorial.Correo);
        }

        private static List<Editorial> Consultar(string sql, System.Action<SqlCommand> parametros)
        {
            List<Editorial> lista = new List<Editorial>();
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
                            lista.Add(new Editorial(
                                Util.LeerEntero(dr, "IdEditorial"),
                                Util.LeerTexto(dr, "Nombre"),
                                Util.LeerTexto(dr, "Pais"),
                                Util.LeerTexto(dr, "Telefono"),
                                Util.LeerTexto(dr, "Correo")));
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
