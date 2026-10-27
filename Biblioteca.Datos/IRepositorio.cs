using System.Collections.Generic;

namespace Biblioteca.Datos
{
    /// <summary>
    /// Operaciones CRUD que comparten las clases de acceso a datos.
    /// Cada clase la implementa con sus propias consultas SQL.
    /// </summary>
    public interface IRepositorio<T>
    {
        List<T> Listar();
        T ObtenerPorId(int id);
        int Insertar(T entidad);
        void Actualizar(T entidad);
        void Eliminar(int id);
    }
}
