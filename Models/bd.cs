using Microsoft.Data.SqlClient;
using Dapper;
using Enerfit.Models;

namespace Enerfit
{
    public class BD
    {
        private static string _connectionString =
            @"Server=localhost;DataBase=Enerfit;Integrated Security=True;TrustServerCertificate=True;";

        // ===================== USUARIOS ======================

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

        public static void EliminarUsuario(int IDUsuario)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Usuario WHERE IdUsuario = @pId";
                connection.Execute(query, new { pId = IDUsuario });
            }
        }

        // ===================== PERFIL ======================

        public static Perfil ObtenerPerfilPorUsuario(int idUsuario)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Perfil WHERE IdUsuario = @pIdUsuario";
                return connection.QueryFirstOrDefault<Perfil>(query, new { pIdUsuario = idUsuario });
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

        public static void ActualizarPerfil(Perfil perfilActualizado)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"
                    UPDATE Perfil
                    SET Nombre = @pNombre,
                        Apellido = @pApellido,
                        Email = @pEmail,
                        Sexo = @pSexo
                    WHERE IdUsuario = @pIdUsuario";

                connection.Execute(query, new
                {
                    pNombre = perfilActualizado.Nombre,
                    pApellido = perfilActualizado.Apellido,
                    pEmail = perfilActualizado.Email,
                    pSexo = perfilActualizado.Sexo,
                    pIdUsuario = perfilActualizado.IDUsuario
                });
            }
        }

        public static bool ExisteEmail(string email)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT COUNT(*) FROM Perfil WHERE Email = @pEmail";
                int count = connection.ExecuteScalar<int>(query, new { pEmail = email });
                return count > 0;
            }
        }

        public static bool ExisteNombreUsuario(string nombreUsuario)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT COUNT(*) FROM Usuario WHERE Nombre = @pNombre";
                int count = connection.ExecuteScalar<int>(query, new { pNombre = nombreUsuario });
                return count > 0;
            }
        }

        // ===================== RECETAS ======================

        public static Recetas ObtenerReceta(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = @"
                    SELECT IDRecetas, Titulo, calorias, proteinas, carbohidratos, ingredientes
                    FROM Recetas
                    WHERE IDRecetas = @id";

                return connection.QueryFirstOrDefault<Recetas>(sql, new { id });
            }
        }

        public static List<Recetas> ObtenerRecetas()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = @"
                    SELECT IDRecetas, Titulo, calorias, proteinas, carbohidratos, ingredientes
                    FROM Recetas";

                return connection.Query<Recetas>(sql).ToList();
            }
        }

        public static int CrearReceta(Recetas receta)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string insertQuery = @"
                    INSERT INTO Recetas (Titulo, calorias, proteinas, carbohidratos, Ingredientes)
                    OUTPUT INSERTED.IDRecetas
                    VALUES (@pTitulo, @pCalorias, @pProteinas, @pCarbohidratos, @pIngredientes);";

                int nuevoID = connection.ExecuteScalar<int>(insertQuery, new
                {
                    pTitulo = receta.Titulo,
                    pCalorias = receta.calorias,
                    pProteinas = receta.proteinas,
                    pCarbohidratos = receta.carbohidratos,
                    pIngredientes = receta.Ingredientes
                });

                return nuevoID;
            }
        }

        public static void EditarReceta(Recetas receta)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"
                    UPDATE Recetas
                    SET Titulo = @pTitulo,
                        calorias = @pCalorias,
                        proteinas = @pProteinas,
                        carbohidratos = @pCarbohidratos,
                        Ingredientes = @pIngredientes
                    WHERE IDRecetas = @pID";

                connection.Execute(query, new
                {
                    pTitulo = receta.Titulo,
                    pCalorias = receta.calorias,
                    pProteinas = receta.proteinas,
                    pCarbohidratos = receta.carbohidratos,
                    pIngredientes = receta.Ingredientes,
                    pID = receta.IdRecetas
                });
            }
        }

        public static void BorrarReceta(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Recetas WHERE IDRecetas = @pId";
                connection.Execute(query, new { pId = id });
            }
        }

    }
}
