using Microsoft.Data.SqlClient;
using Dapper;
using Enerfit.Models;

namespace TP0_INTROBD
{
    public class BD
    {
        private static string _connectionString = @"Server=localhost;DataBase=EnerfitDB;Integrated Security=True;TrustServerCertificate=True;";

        // Verifica si el usuario existe en la BD
        public static Usuario ObtenerUsuario(string nombreUsuario, string contrasenia)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Usuarios WHERE Nombre = @pNombre AND Contrasenia = @pContrasenia";
                return connection.QueryFirstOrDefault<Usuario>(query, new { pNombre = nombreUsuario, pContrasenia = contrasenia });
            }
        }

        // Guarda un nuevo usuario
        public static void AgregarUsuario(Usuario nuevoUsuario)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Usuarios (Nombre, Contrasenia) VALUES (@pNombre, @pContrasenia)";
                connection.Execute(query, new { pNombre = nuevoUsuario.Nombre, pContrasenia = nuevoUsuario.Contrasenia });
            }
        }
        public static void EliminarUsuario(int IDUsuario)
        {
             using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                 string query = "DELETE FROM Usuarios WHERE IdUsuario = @pId";
                connection.Execute(query, new { pId = IDUsuario });
            }
        }


    
       
    }
}
