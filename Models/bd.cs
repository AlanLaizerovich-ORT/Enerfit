using Microsoft.Data.SqlClient;
using Dapper;
using Enerfit.Models;

namespace Enerfit
{
    public class BD
    {
        private static string _connectionString = 
            @"Server=localhost;DataBase=Enerfit;Integrated Security=True;TrustServerCertificate=True;";

        public static Usuario ObtenerUsuario(string nombreUsuario, string contrasenia)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Usuario WHERE Nombre = @pNombre AND Contrasenia = @pContrasenia";
                return connection.QueryFirstOrDefault<Usuario>(query, new { pNombre = nombreUsuario, pContrasenia = contrasenia });
            }
        }

        public static int AgregarUsuario(Usuario nuevoUsuario)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"
                    INSERT INTO Usuario (Nombre, Contrasenia)
                    OUTPUT INSERTED.IdUsuario
                    VALUES (@pNombre, @pContrasenia)";
                
                return connection.ExecuteScalar<int>(query, new 
                { 
                    pNombre = nuevoUsuario.Nombre, 
                    pContrasenia = nuevoUsuario.Contrasenia 
                });
            }
        }

        public static void AgregarPerfil(Perfil nuevoPerfil)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"
                    INSERT INTO Perfil (Nombre, Apellido, Email, Sexo, IdUsuario)
                    VALUES (@pNombre, @pApellido, @pEmail, @pSexo, @pIdUsuario)";
                
                connection.Execute(query, new
                {
                    pNombre = nuevoPerfil.Nombre,
                    pApellido = nuevoPerfil.Apellido,
                    pEmail = nuevoPerfil.Email,
                    pSexo = nuevoPerfil.Sexo,
                    pIdUsuario = nuevoPerfil.IDUsuario
                });
            }
        }

        public static void EliminarUsuario(int IDUsuario)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Usuario WHERE IdUsuario = @pId";
                connection.Execute(query, new { pId = IDUsuario });
            }
        }
    }
}
