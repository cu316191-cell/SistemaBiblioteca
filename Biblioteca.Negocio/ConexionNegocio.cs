using Biblioteca.Datos;

namespace Biblioteca.Negocio
{
    /// <summary>Permite a la interfaz comprobar la conexión sin conocer la capa de datos.</summary>
    public static class ConexionNegocio
    {
        public static bool Probar(out string mensaje)
        {
            return Conexion.ProbarConexion(out mensaje);
        }
    }
}
