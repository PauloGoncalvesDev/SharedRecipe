using Dapper;
using MySqlConnector;

namespace SharedRecipe.Infrastructure.Migrations
{
    public static class Database
    {
        public static void CreateDatabase(string connectionString, string schemaName)
        {
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);

            mySqlConnection.Close();

            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("schemaName", schemaName);

            var registersSchema = mySqlConnection.Query("SELECT * FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = @schemaName", dynamicParameters);

            if (!registersSchema.Any())
                mySqlConnection.Execute($"CREATE DATABASE {schemaName}");
        }
    }
}
