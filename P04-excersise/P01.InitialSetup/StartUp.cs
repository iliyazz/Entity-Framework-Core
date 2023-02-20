namespace P01.InitialSetup
{
    using Microsoft.Data.SqlClient;

    static class StartUp
    {
        private const string DataBaseName = "MinionsDB";

        static async Task Main(string[] args)
        {
            await CreateDatabase();

            await using SqlConnection sqlConnection = new SqlConnection(Config.ConnectionString);
            await sqlConnection.OpenAsync();

            SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
            try
            {
                SqlCommand createAndFillTable = new SqlCommand(SqlQueries.inputTableData,   sqlConnection, sqlTransaction);
                await sqlTransaction.CommitAsync();
            }
            catch (Exception e)
            {
               await sqlTransaction.RollbackAsync();
            }
        }

        private static async Task CreateDatabase()
        {
            await using SqlConnection sqlConnection = new SqlConnection(Config.ConnectionStringCreateDatabase);
            await sqlConnection.OpenAsync();
            SqlCommand createDatabaseCmd = new SqlCommand($"CREATE DATABASE {DataBaseName};", sqlConnection);
            await createDatabaseCmd.ExecuteNonQueryAsync();
            await sqlConnection.CloseAsync();
        }
    }
}