using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Biblioteca.Entidades;

namespace Biblioteca.Datos
{
    /// <summary>Acceso a la tabla Usuarios.</summary>
    public class UsuarioDAO : IRepositorio<Usuario>
    {
        private const string SelectBase =
            "SELECT IdUsuario, Documento, Nombre, Apellido, Telefono, Correo, FechaRegistro FROM Usuarios ";

        public List<Usuario> Listar()
        {
            return Consultar(SelectBase + "ORDER BY Apellido, Nombre", null);
        }

        /// <summary>Busca por documento, nombre o apellido.</summary>
        public List<Usuario> Buscar(string texto)
        {
            return Consultar(
                SelectBase +
                "WHERE Documento LIKE @texto OR Nombre LIKE @texto OR Apellido LIKE @texto " +
                "ORDER BY Apellido, Nombre",
                cmd => Util.AgregarParametro(cmd, "@texto", SqlDbType.NVarChar, 62, "%" + texto + "%"));
        }

        public Usuario ObtenerPorId(int id)
        {
            List<Usuario> lista = Consultar(SelectBase + "WHERE IdUsuario = @id",
                cmd => Util.AgregarParametro(cmd, "@id", SqlDbType.Int, id));
            return lista.Count > 0 ? lista[0] : null;
        }

        public int Insertar(Usuario usuario)
        {
            const string sql =
                "INSERT INTO Usuarios (Documento, Nombre, Apellido, Telefono, Correo, FechaRegistro) " +
                "VALUES (@documento, @nombre, @apellido, @telefono, @correo, @fechaRegistro); " +
                "SELECT CAST(SCOPE_IDENTITY() AS INT);";
            try
            {
                using (SqlConnection cn = Conexion.CrearConexion())
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    AgregarParametros(cmd, usuario);
                    Util.AgregarParametro(cmd, "@fechaRegistro", SqlDbType.Date, usuario.FechaRegistro.Date);
                    cn.Open();
                    usuario.IdUsuario = (int)cmd.ExecuteScalar();
                    return usuario.IdUsuario;
                }
            }
            catch (SqlException ex)
            {
                throw DatosException.DesdeSql(ex);
            }
        }

        public void Actualizar(Usuario usuario)
        {
            const string sql =
                "UPDATE Usuarios SET Documento = @documento, Nombre = @nombre, Apellido = @apellido, " +
                "Telefono = @telefono, Correo = @correo WHERE IdUsuario = @id";
            try
            {
                using (SqlConnection cn = Conexion.CrearConexion())
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    AgregarParametros(cmd, usuario);
                    Util.AgregarParametro(cmd, "@id", SqlDbType.Int, usuario.IdUsuario);
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
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Usuarios WHERE IdUsuario = @id", cn))
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

        /// <summary>Indica si ya hay otro usuario con ese documento.</summary>
        public bool ExisteDocumento(string documento, int idExcluir)
        {
            try
            {
                using (SqlConnection cn = Conexion.CrearConexion())
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT COUNT(*) FROM Usuarios WHERE Documento = @documento AND IdUsuario <> @id", cn))
                {
                    Util.AgregarParametro(cmd, "@documento", SqlDbType.VarChar, 15, documento);
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

        /// <summary>Cuántos préstamos (activos o devueltos) tiene el usuario.</summary>
        public int ContarPrestamos(int idUsuario)
        {
            try
            {
                using (SqlConnection cn = Conexion.CrearConexion())
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT COUNT(*) FROM Prestamos WHERE IdUsuario = @id", cn))
                {
                    Util.AgregarParametro(cmd, "@id", SqlDbType.Int, idUsuario);
                    cn.Open();
                    return (int)cmd.ExecuteScalar();
                }
            }
            catch (SqlException ex)
            {
                throw DatosException.DesdeSql(ex);
            }
        }

        private static void AgregarParametros(SqlCommand cmd, Usuario usuario)
        {
            Util.AgregarParametro(cmd, "@documento", SqlDbType.VarChar, 15, usuario.Documento);
            Util.AgregarParametro(cmd, "@nombre", SqlDbType.NVarChar, 60, usuario.Nombre);
            Util.AgregarParametro(cmd, "@apellido", SqlDbType.NVarChar, 60, usuario.Apellido);
            Util.AgregarParametro(cmd, "@telefono", SqlDbType.VarChar, 15, usuario.Telefono);
            Util.AgregarParametro(cmd, "@correo", SqlDbType.VarChar, 100, usuario.Correo);
        }

        private static List<Usuario> Consultar(string sql, System.Action<SqlCommand> parametros)
        {
            List<Usuario> lista = new List<Usuario>();
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
                            lista.Add(new Usuario(
                                Util.LeerEntero(dr, "IdUsuario"),
                                Util.LeerTexto(dr, "Documento"),
                                Util.LeerTexto(dr, "Nombre"),
                                Util.LeerTexto(dr, "Apellido"),
                                Util.LeerTexto(dr, "Telefono"),
                                Util.LeerTexto(dr, "Correo"),
                                Util.LeerFecha(dr, "FechaRegistro")));
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
