namespace P05.ChangeTownNamesCasing
{
    using Microsoft.Data.SqlClient;
    public class Startup
    {
        public static async Task  Main()
        {
            Console.Write("Enter a country name: ");
            string countryName = Console.ReadLine();

            if ( await TryToMakeTownNameToUppercase(countryName))
            {
                Console.WriteLine("No town names were affected.");
            }
        }

        private static async Task<bool> TryToMakeTownNameToUppercase(string countryName)
        {
            await using SqlConnection sqlConnection = new SqlConnection(Config.ConnectionString);
            
                await sqlConnection.OpenAsync();

                await using SqlCommand sqlCommand = new SqlCommand(SqlQueries.UpdateTownByCountry, sqlConnection);
                
                    sqlCommand.Parameters.AddWithValue("@countryName", countryName);
                    int numberOfAffectedRows = await sqlCommand.ExecuteNonQueryAsync();
                    if (numberOfAffectedRows == 0)
                    {
                        return true;
                    }
                    Console.WriteLine($"{numberOfAffectedRows} town names were affected.");
                    await PrintTownsByCountryName(countryName, sqlConnection);
            return false;
        }

        private static async Task PrintTownsByCountryName(string countryName, SqlConnection sqlConnection)
        {
            await using SqlCommand command = new SqlCommand(SqlQueries.SelectTownByCountry, sqlConnection);
            command.Parameters.AddWithValue("@countryName", countryName);
            await using SqlDataReader sqlReader = await command.ExecuteReaderAsync();
            IEnumerable<string> townsNames = ReaderToCollection(sqlReader, 0);

            Console.WriteLine($"[{string.Join(", ", townsNames)}]");
        }

        private static IEnumerable<string> ReaderToCollection(SqlDataReader sqlReader, int columnIndex)
        {
            while (sqlReader.Read())
            {
                yield return sqlReader.GetString(columnIndex);
            }
        }
    }

}