using System;
using System.Data;
using System.Data.SqlClient;

namespace Biblioteca.Datos
{
    /// <summary>Métodos de apoyo para los parámetros y la lectura de datos.</summary>
    internal static class Util
    {
        /// <summary>Convierte null en DBNull para enviarlo como parámetro.</summary>
        public static object ValorBD(object valor)
        {
            return valor ?? DBNull.Value;
        }

        public static void AgregarParametro(SqlCommand cmd, string nombre, SqlDbType tipo, object valor)
        {
            cmd.Parameters.Add(nombre, tipo).Value = ValorBD(valor);
        }

        public static void AgregarParametro(SqlCommand cmd, string nombre, SqlDbType tipo, int tamano, object valor)
        {
            cmd.Parameters.Add(nombre, tipo, tamano).Value = ValorBD(valor);
        }

        public static string LeerTexto(IDataRecord dr, string columna)
        {
            int i = dr.GetOrdinal(columna);
            return dr.IsDBNull(i) ? null : Convert.ToString(dr.GetValue(i));
        }

        public static int LeerEntero(IDataRecord dr, string columna)
        {
            return Convert.ToInt32(dr[columna]);
        }

        public static int? LeerEnteroNulo(IDataRecord dr, string columna)
        {
            int i = dr.GetOrdinal(columna);
            return dr.IsDBNull(i) ? (int?)null : Convert.ToInt32(dr.GetValue(i));
        }

        public static DateTime LeerFecha(IDataRecord dr, string columna)
        {
            return Convert.ToDateTime(dr[columna]);
        }

        public static DateTime? LeerFechaNula(IDataRecord dr, string columna)
        {
            int i = dr.GetOrdinal(columna);
            return dr.IsDBNull(i) ? (DateTime?)null : Convert.ToDateTime(dr.GetValue(i));
        }

        /// <summary>Ejecuta una consulta y devuelve el resultado en una tabla.</summary>
        public static DataTable LlenarTabla(SqlCommand cmd)
        {
            DataTable tabla = new DataTable();
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.Fill(tabla);
            }
            return tabla;
        }
    }
}
